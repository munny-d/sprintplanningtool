### SPRINT PLANNING TOOL CREATED BY MANPREET DHILLON

This is a user and report SPA system created with Vue 3, .NET 6, and SQLite 3.
This web app was created for the Software Engineering / DevOps module, Summer 2022.

### FOLDER STRUCTURE

-   .github --> this folder contains the .yml file for building the CI pipeline via github
-   sprintplanningtool.sln --> solution file for Visual Studio
-   /sprintplanningtool --> client code
-   /sprintplanningtoolbackend --> server code
-   /sprintplanningtooltests --> unit tests for business logic

### PRE-REQUISITES

Pre-requisites for development

-   Visual Studio 2022 with dotnet 6 dependencies installed
-   Node v16.\*
-   SQLite 3
    ** if you do not have the required node version **
-   option 1: downgrade/upgrade your node version to v16.16.0
-   option 2: install nvm (node version package manager) to switch between different node versions https://github.com/nvm-sh/nvm

### HOW TO BUILD APPLICATION FOR DEVELOPMENT

1. Open the solution file in Visual Studio
2. Right click the solution file and click on "Set Startup Projects..."
3. Select "Multiple startup projects:"
4. Select "Start" for sprintplanningtool and sprintplanningtoolbackend
5. Click apply and ok
6. Run build (CTRL+Shift+B) or click "Start"

### HOW TO RUN FRONTEND ONLY

1. Open terminal and cd to /sprintplanningtool
2. npm install
3. npm run build
4. npm run serve

### HOW TO RUN BACKEND ONLY

1. Open the solution file in Visual Studio
2. Select 'sprintplanningtoolbackend'from the dropdown (next to the build/debug icon)
3. Select 'startapp' for build
4. Click build

### HOW TO RUN TESTS

1. In Visual Studio, click on sprintplanningtooltests in the solution explorer
2. Open the test explorer (CTRL+E, T)
3. Run all tests by hitting the shortcut "CTRL+R, A" or by hitting the run button in the test explorer
4. Run an individual test by either selecting it in the test explorer or right click the test method and hit run test.
