# Hospital-Project
#Nastaran Azani
#Book appointment
-Models: I creaated 2 Models:client and appointment to book an appointment 
(There are 2 fields in appointment table that should be foreign key but there isn't connection to the tables) 
-I created hospitaldb context and  set connection string that called Hospital. 
-Database and tabels were created based on Models. The database is hospitalDb
-There were some problems to create data base and tables that were fixed.
-In next step, I created api contoroller to do add,delet,list... .(appointmentData)
-After that  MVC controller was created to do crud on data base.
-Finally, Views created based on MVC controller.
-Since,I should work on login page. I Added 3 columns(Name,LastName,HealthNC) to ASPNetUsers
(I think the appointment's table should connect to ASPNetUser insted of clients' table, but I didn't check )
-Also, I considered 3 roles in hospital(Admin,General User and staff) that I added to ASPNetRple's table.
