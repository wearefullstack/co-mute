# Asanda N. Mkhize

## Please follow this a guide if you want to setup and start the Server API and web appliaction.

## Please Note:

*I chose to do this project using NodeJs(API) and React/React-Dom(Web Application) because it was advised to use something i'm comfortable in. C# is a language in comfortable in but when it come to Web Apllication, React is my Go-To Language and so because of time and wanting to keep the codebase as simple as possible, i went with Javascript and React,*


## Setting up The API:

    $ cd path/to/co-mute/src/api
    $ npm install

## Environment Variables:

1. I'm using MySql on Aws as my SQL server. if you wish to use a local instance, i've included a .env file where you can change the connection parameters( i do release pushing .env files is unsafe but since for now this is our only way of connicating, i had go that route).
2. I used JSON web token to authentication. if you want to change the signing key, there is a variable for that
3. You can change the default Server PORT **8289**. *IMPORTANT: Also Change **REACT_APP_API_HOST** in the .env file of the frontend

## Running the API Server:

    $ npm run buildAndRun



# Setting up the Web Application

## Start Application:

    $ npm start


# About The Application

## API Libraries:


1. **bcrypt**: Salting and Hashing passwords
2. **uuid**: generating unique id for the table entries
3. **json web token**: for authentication and authorizatinn
4. **dotenv**: working with environment variables
5. **mysql**: mysql driver for NodeJs
6. **moment**: validating and comparing Time objects

## UI Libraries

1. ant design




# co-mute

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

 
