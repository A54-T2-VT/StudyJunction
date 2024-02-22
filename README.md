
# StudyJunction

StudyJunction is a education platform ment for those who want to learn something new or teach others interesting topics.

## Features

- Student/Teacher/Admin/God separate interfaces
- WikiAction API integration
- Cloudinary integration
- Courses search bar, enrollment.
- Lectures for each course in witch you can search in wiki, read description, do posted assignment.

## Installation and Setup

After clonning the repo you would need to do few things before you can run the projects.

- Make sure you have up and running SQLExpress server.
- You will need registration in Cloudinary.com in order to ues their file managment services.
- In StudyJunction.Web create a json file named "CloudinaryApiKeys.json" with the following data

```bash
{
    "Cloud": "yourCloudName",
    "ApiKey": "yourApiKey",
    "ApiSecret": "yourApiSecret"
}
```
- In every CurrLecture replace cloudName with your cloud name, below is an example of what the script looks like
```bash
<script>
    const player = cloudinary.videoPlayer('player', {
        cloudName: 'dxhiilbyu',
        fluid: true,
        controls: true,
        floatingWhenNotVisible: 'right',
        showJumpControls: true,
        pictureInPictureToggle: false,
        hideContextMenu: true
    });

    player.source('@Model.VideoId', {
        sourceTypes: ['webm/vp9', 'mp4/h265', 'mp4/h264']
    });
</script>
```
- In the packet manager console type, make sure you have selected StudyJunction.Infrastructure
```bash
update-database
```
- If there is a problem with database creation, try deleting the migration folder from StudyJunction.Infrastructurethe and type in the packet manager console, each line is a separate command
```bash
add-migration Initial

update-database
```
## Important
- After creating the database, the roles won't be created, go to "StudyJunction.Core.Services.UserService", there is a method called "CreateRoles()" you need to place it in the register method at the beggining so that when the first user registers all roles will be created.
- Currently there is no default God user, so when registering God user go to "StudyJunction.Core.UserService", there find Register and on the line below inside the curly bracets write ALL roles from RolesConstants when you want to register God, after that return it as it was. Now you can use the god account to promote other users up to admin.
```bash
  _ = await userManager.AddToRolesAsync(user, new string[] { RolesConstants.Student});
```
- For Cloudinary, there is a free account, but it has limitations of file upload size, so check those before uploading files, you would alsno need to go into Cloudinary settign and unable .pdf nas .zip upload.   
## Functionality
`USER: Student and Anonymou`
- Anonymous user Home page.
![Anonymous Home Page](/ScreenShots/DefaultHomePage.png)

- Register page:
  - After registration each user gets the role of a Student.
![Register Page](/ScreenShots/RegisterPage.png)

- Login page.
![Login Page](/ScreenShots/LoginPage.png)

- Logged in Students have the same home page as the Anonymous, the main difference is in the available action in the navbar and drop dowm menu in the profile.
  - From the "Courses" you will see all approved courses on the platform. After pressing on a course you will view its details and be able to enroll.
  - From the "Became Teacher" you will get a form in which you need to give your credentials and then await approval from either Admin or God account. 
![Student Home Page](/ScreenShots/StudentHomePage.png)

`USER: Teacher`
- Teachers home page is the same, appart for the navbar available actions.
![Teacher Home Page](/ScreenShots/TeacherHomePage.png)

- Apart from all the thing a student can do, teacher can create courses and lectures for said courses. When he thinks his course is ready, he can send it for approval.
- For each course he has created he cad add lectures, which will have, video, description, assignment and wiki search window. You can see them when you watch lecture at bottom left of the screenshot.
- He can also keep sort of statistic on all courses he has created, like enrollments, number of lectures and approval status.
![Watch Lecture Page](/ScreenShots/WatchLecturePage.png)
`USER: Admin`
- Admins home page is the same, but he gets different set of actions.
- ![Admin Home Page](/ScreenShots/AdminHomePage.png)
  - In "UsersRoles" he can see and promote/demote all users up to teacher role included.
  - In "Review Teacher Candidacies", you can guess what he can do, if you've read that far you are obviously capable of thinking and breathing at the same time.
  - In "Courses for Approval" he can approve courses submitet by teachers.
  - In "Add Category" he can add either parent or child category, to add parent category he must select from the drop dowm meny "None" option.

`USER: God`
- He has same functionality as Admin, only with elevated privileges, for example he can see all users roles and can make other users admins.
- 

## Technological highlights
- Integration with Cloudinary for:
  - Video storage and play
  - Lecture Assignment storage
  - Course thumbnail storage
    
- Integration with MediaWiki Action Api:
  - When the user watches lecture, he can perform wiki search and get a snippet of the info he was searching for, also a link to the full wiki page will be provided for the user.

- Users authentication
  - JWT authentication with bearing scheme used to access the REST API 
  - Identity with cookies keeping users' claims used for the MVC part
  - Default password encryption from asp.net mvc
  - Role based authorization: God -> Admin -> Teacher -> Student

- Automapper: Technic to automatically map the database models to view models and Dto models

- Database diagram
  ![DB Diagram](/ScreenShots/DatabaseDiagram.png) 
## Contributing

Contributions are always welcome!

If you have any ideas please create an Issue in the Issues tab above and tag it with the coresponding tag.


## License

[MIT](https://choosealicense.com/licenses/mit/)


## Credits to external resources
- [Site Theme](https://bootstrapmade.com/mentor-free-education-bootstrap-theme/)
