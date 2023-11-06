# Google-Calendar-Events
Manage Calendar Events Using Google Calendar API
# STEP 1: Obtain Google API Credentials

•	Go to the Google Cloud Console (https://console.cloud.google.com/) and create a new project. 
•	Enable the Google Calendar API for your project. 
•	OAuth consent screen:
  o	write Application Name 
  o	Add scopes: (https://www.googleapis.com/auth/calendar , https://www.googleapis.com/auth/calendar/events) 
  o	Add Test user with your google mail.
  o	Save and back to DashBoard 


•	Create API credentials (OAuth 2.0 client ID) for your project
  o	choose Application Type to be Web Application
  o	Name the client ID
  o	Add Authorized Redirect URIs (http://localhost/authorize/ , http://127.0.0.1/authorize/) 
  o	Download and store the JSON file containing the credentials securely and rename it to credentials.


# STEP 2: Set up the Project 
// Recommended Visual Studio 2022
1. Make sure to have Visual Studio Downloaded (Preferably Version 2022 to support .NET core v7) if not you can download it from here (https://visualstudio.microsoft.com/vs/)
2. Open Visual Studio and Clone the Repository using this link (https://github.com/shamss17/Google-Calendar-Events)
3. You may use NuGet to manage and install the required packages.
4. Install package Google.Apis.Calendar.v3 .


# STEP 3: Update the Code with Google API Credentials:
1. Place the downloaded JSON file containing the credentials in the "Credentials" folder of the application instead of the existing one.
2. Make sure the file is named "credentials.json".
3. Make sure to change the ApplicationName in the eventsController.cs and the GoogleCalendarHelper.cs to your Application Name of the project you created the Google Developer Console.

# STEP 4: Run the application
1. Make sure to choose IIS to run the project.
2. Run the project
3. A web app will open in browser with Swagger UI

# STEP 5: Redirected to Authorize
1. Press on the create method and click Try Out
2. Change the Start day and End day
3. Click Execute
4. Sign in with the email you provided as a test user email
5. You will be redirected to authorize access to continue to "Your Application Name"
6. Click Continue to Verify the app and continue to trust
7. You will get this message " Received verification code. You may now close this window " ,close the window
8. Now, Re-run the application and try out the methods as mentioned in STEP 6

# STEP 6: Testing and Outputs
1. Check the documentation for the API Endpoints Methods ,Description, and expected inputs and outputs
2. Click the method and click try it out
3. Use this example for Testing the POST/api/events to Create an Event in your google calendar of the signed in and authorized mail:
   -replace the request body with this( You can change the values of the details as you wish ):
   {
  "summary": "Task",
  "description": "meeting",
  "location": "Cairo",
  "start": "2023-11-29T23:20:28.573Z",
  "end": "2023-11-29T23:20:28.573Z"
   }
   -press execute 
 5. Use this example for Testing the GET/api/events to view an Event in your google calendar of the signed in and authorized mail:
    click on try it out 
    provide the details with
    *ICalUID : primary (This to use the calendar ID of the signed in Test user)
    *MaxResults : 10
    *FromDate : 2023-10-10
    *ToDate : 2023-12-30
    *SearchQuery : meeting
    click Execute
  6. Use this example for Testing the DELETE/api/events{eventId} to view an Event in your google calendar of the signed in and authorized mail:
    click on try it out 
    provide the eventId: You can get it from the id parameter of the GET method
     *if this is an output for the GET :
     {
     "endTimeUnspecified": null,
    "eTag": "\"3397844665166000\"",
    "eventType": "default",
    "extendedProperties": null,
    "gadget": null,
    "guestsCanInviteOthers": null,
    "guestsCanModify": null,
    "guestsCanSeeOtherGuests": null,
    "hangoutLink": "https://meet.google.com/ksf-jupp-wua",
    "htmlLink": "https://www.google.com/calendar/event?eid=cW9qbzE3cTY0NTYyb2c5ZDd2bmIxa3BiMGMgc2hhbXN0YXJlazEzQG0",
    "iCalUID": "qojo17q64562og9d7vnb1kpb0c@google.com",
    "id": "qojo17q64562og9d7vnb1kpb0c",
    "kind": "calendar#event",
    "location": "Google Meet (instructions in description)",
    "locked": null,
    "organizer": {
      "displayName": null,
      "email": "sortech691@gmail.com",
      "id": null,
      "self": null
    }
    }
    You can use the "id": "qojo17q64562og9d7vnb1kpb0c" to Delete this event by adding qojo17q64562og9d7vnb1kpb0c in the eventId field
# Do not forget to check the doumentation for the expected outputs and Errors
    
    
    
    

   


