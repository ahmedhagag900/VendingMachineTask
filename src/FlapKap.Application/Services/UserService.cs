using FlapKap.Application.BusinessRules.UserRules;
using FlapKap.Application.Interfaces;
using FlapKap.Application.Models;
using FlapKap.Core;
using FlapKap.Core.Entities;
using FlapKap.Core.Enums;
using FlapKap.Core.Repositories;
using FlapKap.Core.UnitOfWork;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace FlapKap.Application.Services
{
    internal class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryprographyService _cryprographyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly VendingMachineSettings _settings;
        public UserService(IUserRepository userRepository,
            ICryprographyService cryprographyService,
            IUnitOfWork unitOfWork,
            VendingMachineSettings settings)
        {
            _cryprographyService = cryprographyService ?? throw new ArgumentNullException(nameof(cryprographyService));
            _userRepository=userRepository?? throw new ArgumentNullException(nameof(userRepository));  
            _unitOfWork=unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));    
        }
        public async Task<UserModel> Add(UserModel model,CancellationToken cancellationToken)
        {
            await CheckRule(new UserNameShouldBeUniqueRule(model.UserName, _userRepository));

            var user = new User
            {
                UserName = model.UserName,
                Password = await _cryprographyService.HashAsync(model.Password),
                Name = model.Name,
                Deposit=model.Deposit,
                RoleId = (int)model.RoleId
            };

            var added=await _userRepository.AddAsync(user,cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return new UserModel
            {
                Id = added.Id,
                UserName = added.UserName,
                Name = added.Name,
                Deposit = added.Deposit,
                RoleId=(UserRole)added.RoleId
            };
        }

        public async Task Delete(int id)
        {
            await CheckRule(new UserExsitsRule(id, _userRepository));
           
            var userToDelete = await _userRepository.GetByIdAsync(id);
            _userRepository.Delete(userToDelete);
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            return (await _userRepository.GetAsync(c => true)).Select(u => new UserModel
            {
                Id = u.Id,
                Name = u.Name,
                RoleId = (UserRole)u.RoleId,
                Deposit=u.Deposit,
                UserName = u.UserName
            });
        }

        public async Task<UserModel> GetById(int id)
        {
            await CheckRule(new UserExsitsRule(id, _userRepository));
           
            var user = await _userRepository.GetByIdAsync(id);
           
            return new UserModel
            {
                UserName = user.UserName,
                Id = user.Id,
                Name = user.Name,
                Deposit=user.Deposit,   
                RoleId = (UserRole)user.RoleId
            };
        }

        public async Task<LoginModel> LoginAsync(string userName, string password)
        {

            await CheckRule(new UserExsitsRule(userName, _userRepository));

            Expression<Func<User, object>> roleInclude = usr => usr.Role;
            var user = (await _userRepository
                .GetAsync(usr => usr.UserName == userName,
                new List<Expression<Func<User, object>>> { roleInclude}))
                .SingleOrDefault();

            var hashedPassword = await _cryprographyService.HashAsync(password);
            
            if(hashedPassword==user.Password)
            {
                return new LoginModel
                {
                    AccessToken = GenerateAccessToken(user),
                };
            }
            return new LoginModel();
        }

        public async Task<UserModel> Update(UserModel model,CancellationToken cancellationToken)
        {
            await CheckRule(new UserExsitsRule(model.Id, _userRepository));
            await CheckRule(new UserNameShouldBeUniqueRule(model.UserName, _userRepository));


            var userToUpdate = await _userRepository.GetByIdAsync(model.Id);
            

            userToUpdate.UserName = model.UserName;
            userToUpdate.RoleId = (int)model.RoleId;
            userToUpdate.Name = model.Name;
            userToUpdate.Deposit = model.Deposit;
            userToUpdate.Password = await _cryprographyService.HashAsync(model.Password);

            var updated=_userRepository.Update(userToUpdate);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return new UserModel
            {
                Id = updated.Id,
                Name = updated.Name,
                UserName = updated.UserName,
                Deposit = updated.Deposit,
                RoleId = (UserRole)updated.RoleId
            };
        }

        private string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role,user.RoleId.ToString())
            };

            var key = _settings.JWTOptions.SecretKey;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _settings.JWTOptions.Issuer,
                Audience = _settings.JWTOptions.Audience,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };


            var handler = new JsonWebTokenHandler();


            return handler.CreateToken(descriptor);
        }

    }
}
