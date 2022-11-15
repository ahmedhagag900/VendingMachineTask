# FlapKapTask
The task is to implement a vending machine operations and expose it with RESTFull APIs<br>


## System Architecture
- Used the clean/onion archituctre to implement the soultion. 
- Implement the CQRS pattern to sperate commands from queries.
- Implement unit of work to ensure the data consistancy & intergraty.

![onion](https://miro.medium.com/max/462/1*0Pg6_UsaKiiEqUV3kf2HXg.png)

## How To Run
 - The application has some seed data to quick start it if you dont need to seed any data comment the seed line in the following snapshot <br>
 - Note that the database must have role data seeded in the database to run probably.
 ![inmemory](https://user-images.githubusercontent.com/69547439/201939565-f422bcef-88b8-4202-af77-67913a0420c5.PNG)
 - you can run the application using sql server database or in memory database.
 
### SQL Server Database
- you need to create database named ```VendingMachine``` 
- change the connection string property in ```appsettings.Development.json``` <br> <br>
![connectionString](https://user-images.githubusercontent.com/69547439/201938771-da33d519-0160-4cac-b20a-b88d965c12d8.PNG)
- build and run the application

### In Memory Database
- If you do not need to create databse or do not have sql server database you can run the app with in memory database 
- change the flag in the following snapshots to true in ```program.cs``` file <br> <br>
![inmemory](https://user-images.githubusercontent.com/69547439/201939565-f422bcef-88b8-4202-af77-67913a0420c5.PNG)
![inmemory2](https://user-images.githubusercontent.com/69547439/201939589-576e32ba-5cdc-458b-912d-8c89805cc672.PNG)






