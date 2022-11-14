using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Core.Enums
{
    public enum ApplicationCode
    {
        UserNameExists=1,
        UserDoesNotExists=2,
        RoleDoseNotExisits=3,
        ProductDoseNotExisits=4,
        ProductOutOfStock=5,
        RemainingBalancIsNotEnough=6,
        InvalidLogin=7,

        ServerError=500
    }
}
