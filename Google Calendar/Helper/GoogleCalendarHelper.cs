using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google_Calendar.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace Google_Calendar.Helper
{
    public class GoogleCalendarHelper
    {
        protected GoogleCalendarHelper() 
        { 

        }
        public static async Task<Event> CreateGoogleCalendar(GoogleCalendar request)
        {
            //Handling the authentication issues or invalid input.
            try
            {
                String[] Scopes = { "https://www.googleapis.com/auth/calendar" };
                String ApplicationName = "Go Calendar";
                UserCredential credential;

                using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Credentials", "credentials.json"), FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync
                        (
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)
                        ).Result;

                }
                //Define Services
                var services = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,


                });
                // Check if all required fields are present
                if (string.IsNullOrEmpty(request.Summary) || string.IsNullOrEmpty(request.Summary) || string.IsNullOrEmpty(request.Location) ||
                    request.Start == null || request.End == null || string.IsNullOrEmpty(request.Description))
                {
                    throw new ArgumentException("Missing required fields");
                }
              
                //Define Request
                Event eventCalendar = new Event()
                {
                    Summary = request.Summary,
                    Location = request.Location,
                    Start = new EventDateTime
                    {
                        DateTime = request.Start,
                        TimeZone = "Africa/Cairo"
                    },
                    End = new EventDateTime
                    {
                        DateTime = request.End,
                        TimeZone = "Africa/Cairo"
                    },
                    Description = request.Description,
                };

                //Make the API call
                var eventRequest = services.Events.Insert(eventCalendar, "primary");
                var requestCreate = await eventRequest.ExecuteAsync();
                return requestCreate;
            }
            catch (Google.GoogleApiException ex)
            {
                // Handle authentication issues
                Console.WriteLine("Google API Exception: " + ex.Message);
                // You can also log the exception details for debugging purposes

                // Handle invalid input
                if (ex.Error is Google.Apis.Requests.RequestError requestError)
                {
                    Console.WriteLine("Request Error: " + requestError.Message);
                    // You can handle specific error types or display a generic error message
                }
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            // Return a default or null value in case of error
            return null;
        }

     
    }

}
