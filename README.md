# FlapKapTask
The task is to implement a vending machine operations and expose it with RESTFull APIs<br>


## System Architecture
- Used the clean/onion archituctre to implement the soultion. <br><br>
![onion](https://miro.medium.com/max/462/1*0Pg6_UsaKiiEqUV3kf2HXg.png)
- Implement the CQRS pattern to sperate commands from queries.
![cqrs](https://miro.medium.com/max/1400/1*9PIFrsO4_ZGes2uTXCVTgQ.png)
- Implement unit of work to ensure the data consistancy & intergraty.

