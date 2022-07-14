# CO-MUTE [ DEMO ]

## Now operating between major city centres! ( CPT,DBN,JHB,PTA,PE )

### Running the demo
  - Clone repo onto local : `git clone <repoUrl>`
  - Switch to `arno-bornman` branch
  - In terminal at root of repo, navigate to `./co-mute-hybrid`
  - Run `npm install` to resolve all required client-side dependencies
  - Open `co-mute-hybrid/co-mute-hybrid.sln` in Visual Studio 2022 (used for development)
  - Rebuild solution and ensure all Nuget Packages resolve
  - Hit `F5` and have fun!

### Current MVP features
 - Register a new user account ( with form validation & semi-strong password )
 - Sign-in with user account once profile created
 - Register a new car pool with intuitive controls
 - Basic CRUD on User and CarPool entities 
 - View and basic search for all car-pool entries created

### Tech details
 - The client side is developed in Angular (version 14)
 - The API is developed in C# / .NET Core (version 6)
 - Bootstrap CSS is used for styling
 - Each client-side feature module is "lazy-loaded" to increase initial load-time and save on bandwidth for features not accessed
 - Certain app routes are guarded/blocked for authentication on a basic level to enforce sign-on
 - The Data-Model is composed with EF Core allowing abstraction of data queries and a good level of database provider agnostics
 - Current Data uses an "in-memory" provider (time constraints prevented a SQL implementation). Any standard SQL provider should be easily configured with a future iteration where the DBContext of EFCore is hooked up to SQLServer,MySQL,SQLite etc

 ### TODO's
 - Ability to book and edit bookings for car-pool
 - Ability to edit profile details (currently view-only)
 - Integration with a SQL data provider for persistence between app-restarts
 - Ability to specify days availability, currently only full dates are provided in a car-pool setup
 - A lot of iteration on user-feedback and usability + general styling

 ## Original instructions

### Please note: You can branch and create a Node.JS, Java or Python implementation if you are not familiar with C#, but Full Stack does use C# as our primary language.

You will be building a web application for a fictional client based on requirements they provided. 

You will need to create an account on GITHUB. The Git repository for the project is named “co-mute” and contains the following folders:
•	spec/ - This folder contains a PDF with the requirements for the web application
•	src/ - This folder contains the source code for the web application

Some initial code is included in the repository to get you started. It uses .NET Framework, but you are welcome to use .NET Core; MVC, RazorPages or Blazor - what ever framework you are most productive and effective in. 
The solution can be opened with Visual Studio (the Community Edition should also work). 
You are free to use any (legal) third party libraries/frameworks you deem fit as long as the requirements are met. To get started with the project, please follow the following steps:

1. Fork the repo to your GitHub
2. Get the code from GitHub (SourceTree is a free Git client you can use, but you can use any Git client you are comfortable with)
3. Create a new branch off of the ‘master’ branch. The new branch name should follow the following convention: “<first name>-<last name>” (i.e. xolani-mntambo)

Please make sure to push your branch to GitHub once you are satisfied with your implementation, do a pull request, and send codetest@fullstack.co.za an email to indicate that you have completed the test, so we can review your work. The test was designed to be completed within a day. With this in mind, you don’t necessarily need to complete the project - you may stub out methods/interfaces with short comments stating their intent should you run out of time.

Looking forward to hearing from you. When you are done, just mail codetest@fullstack.co.za and let us know your branch name. 

 
