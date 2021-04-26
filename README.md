# Hospital-Project

I used the feedback provided on my MVP submission to build and improve on my final hospital project submission. I followed the varsity project examples more closely this time around and added the controllers News_events1Controller, News_events1DataController, Payments1Controller, and Payments1DataController to improve on my initial submission. I used the varsity example code to access data from the API and attempted to call the data access methods with a C# HTTP Client. I have also added new views for these controllers in the News_events1 folder and Payments1 folder and avoided using auto-scaffolded code. Although my views are currently not working due to errors, I am confident that with just a little more time, I will be able to work through these errors and get the views working since I have learned some of the key course components through the improvements to my mvp.

I have also added code that ensures that the methods which Add, Update, and Delete entities in the database will only execute if the user is logged in as an administrator, or as a patient in the case of Payments. I have also added code that would allow for pagination, and image uploading for a News/Event item or payment if someone wanted to upload a picture of a bill or credit card. When merging our projects together, in order to know which patient made a Payment, I would connect to the Application User entity created by Nastaran and use that as a foreign key in my Payments model.  

There was quite a learning curve for me with C# and using Web Api and MVC, but after taking the time to learn the proper methods, I have a better understanding of the expectations and key course components. With some more time, I am confident that my team members and I will be able to meet all requirements of this project and beyond. 

References used for the project: https://github.com/christinebittle/varsity_mvp/blob/master/varsity_w_auth/Controllers/PlayerController.cs 

https://github.com/christinebittle/varsityproject/blob/master/Varsity_Project/Controllers/PlayersController.cs 
