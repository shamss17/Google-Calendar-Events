# Google-Calendar-Events
Manage Calendar Events Using Google Calendar API
# STEP 1: Obtain Google API Credentials
1.Go to the Google Cloud Console (https://console.cloud.google.com/) and create a new project.
2.Enable the Google Calendar API for your project.
3. OAuth consent screen: 
  1- write Application Name (Remember this name to replace it in the controller and helper files in the STEP 3)
  2- Add scopes:(https://www.googleapis.com/auth/calendar , https://www.googleapis.com/auth/calendar/events) 
  3-Add Test user with your google mail 
  4-Save and back to DashBoard 
4.Create API credentials (OAuth 2.0 client ID) for your project
  1- choose Application Type to be Web Application
  2- Name the client ID
  3- Add Authorized Redirect URIs (http://localhost/authorize/ , http://127.0.0.1/authorize/)
5.Download and store the JSON file containing the credentials securely and rename it to credentials.

# STEP 2: Set up the Project 
# Recommended Visual Studio 2022
1. Make sure to have Visual Studio Downloaded (Preferably Version 2022 to support .NET core v7) if not you can download it from here (https://visualstudio.microsoft.com/vs/)
2. Open Visual Studio and Clone the Repositary using this link (https://github.com/shamss17/Google-Calendar-Events)
3. You may use NuGet to manage and install the required packages.
4. Install package Google.Apis.Calendar.v3 .


# STEP 3: Update the Code with Google API Credentials:
1. Place the downloaded JSON file containing the credentials in the "Credentials" folder of the application instead of the existing one.
2. Make sure the file is named "credentials.json".
3. Make sure to change the ApplicationName in the eventsController.cs and the GoogleCalendarHelper.cs to your Application Name of the project you created the Google Developer Console.
# STEP 4: Run the application
1. Make sure to choose IIS to run the project.
2. Run the project 
   


