# Hospital-Project

## Team Members:
Jalaluddin qureshi  
Jemi Choi  
Sasha Srinivas   
Navpreet Kaur  
Nastaran Azani  
Simranjeet Singh   


===============================================
### Jemi Choi:  
Responsible for: Donation and Donor Entities  
Branch location:  https://github.com/sasha-s-07/Hospital-Project/tree/donation    
Learnings:  
I am responsible for the donors and donation entities. Unfortunately, at this point in time, I have not gotten the Views to work fully yet.  I  will continue to work through the bugs that keep popping up. For every project I have done so far, new types of bugs seem to appear. They are endless in variety. And this time around, I am keeping a log of the different bugs with solution so that I can reference them if I encounter them again.     
Reference: Christine Bittle's Varsity MVP, Blog project  

===============================================
### Sasha Srinivas 
I have worked on the news/events feature and the payments feature. I have submitted my models, controllers and data controllers and am able to get CRUD working through my Views. My work is located in the news_events_payments branch. 

===============================================  
### Simranjeet Singh 
- I have worked on the Volunteer Applications , Departments and Volunteer Opportunitities table including models, controllers and datacontollers.
- My work is in Volunteers_Departmentsss branch : https://github.com/sasha-s-07/Hospital-Project/tree/Volunteers_Departmentsss including project readme.md

===============================================
### Navpreet Kaur
Responsible for: Job Posting and Hospital Staff Directory feature.  
Branch Location: https://github.com/sasha-s-07/Hospital-Project/tree/Jobposting_and_staff      
Learnings: I am working on Job Posting feature and Hospital Staff Directory feature where Admin can create, read, update and delete job posts and hospital staff information but user can only read the information. I have submitted my models, mvc controllers, web api controllers and views working properly.

===============================================
### Jalaluddin Qureshi
Feature: Doctor CRUD
Branch Location: https://github.com/sasha-s-07/Hospital-Project/tree/DoctorCRUD

Learnings: My major learning has been to WebAPI and ViewModels for this project. In the Passion Project I had used the MVC controller (auto-scaffolding approach). Due to this, there had been a large learning curve for me for this project.

Right Now I have got the List, Detail and Create working. I have also implemented Pagination feature on the List View. Over the next coming days and weeks, I would be further improving my work.

=================================================

### Nastaran Azani

Book appointment,Login

Branch Location:https://github.com/sasha-s-07/Hospital-Project/tree/login_appointment2

-Models: I creaated 2 Models:client and appointment to book an appointment

(There are 2 fields in appointment table that should be foreign key but there isn't connection to the tables)

-I created hospitaldb context and set connection string that called Hospital.

-Database and tabels were created based on Models. The database is hospitalDb

-There were some problems to create data base and tables that were fixed.

-In next step, I created api contoroller to do add,delet,list... .(appointmentData)

-After that MVC controller was created to do crud on data base.

-Finally, Views created based on MVC controller.

-Since,I should work on login page. I Added 3 columns(Name,LastName,HealthNC) to ASPNetUsers

(I think the appointment's table should connect to ASPNetUser insted of clients' table, but I didn't check )

-Also, I considered 3 roles in hospital(Admin,General User and staff) that I added to ASPNetRple's table
