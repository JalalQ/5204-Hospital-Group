# Hospital-Project
#Nastaran Azani
#Book appointment

1.Models: I creaated 2 Models:client and appointment to book an appointment 
(There are 2 fields in appointment table that should be foreign key but there isn't connection to the tables)
2.I created hospitaldb context and  set connection string that called Hospital.  
3.Database and tabels were created based on Models. The database is hospitalDb
4.There were some problems to create data base and tables that were fixed.
4.In next step, I created api contoroller to do add,delet,list... .(appointmentData)
5.After that  MVC controller was created to do crud on data base
6.Finally, Views created based on MVC controller
7.Since,I should work on login page. I Added 3 columns(Name,LastName,HealthNC) to ASPNetUsers
(I think the appointment's table should connect to ASPNetUser insted of clients' table, but I didn't check )
8.Also, I considered 3 roles in hospital(Admin,General User and staff) that I added to ASPNetRple's table.
