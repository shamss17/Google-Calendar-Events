using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google_Calendar.Helper;
using Google_Calendar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Google_Calendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class eventsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateGoogleCalendar([FromBody]GoogleCalendar request)
        {

            Event eve= await GoogleCalendarHelper.CreateGoogleCalendar(request);

            // Return the details of the created event along with a 201 Created HTTP status code
            if (eve != null) 
            {
                // Check if the start or end date falls on a Friday or Saturday
                if (request.Start.DayOfWeek == DayOfWeek.Friday || request.Start.DayOfWeek == DayOfWeek.Saturday ||
                    request.End.DayOfWeek == DayOfWeek.Friday || request.End.DayOfWeek == DayOfWeek.Saturday)
                {
                    // Add an error message to the ModelState
                    ModelState.AddModelError("", "Events cannot be scheduled on Fridays or Saturdays,Come on it is the week-end!");

                    // Return a 400 Bad Request HTTP status code with the error messages
                    return BadRequest(ModelState);
                }
                // Check if the start time is in the past
                if (request.Start < DateTime.Now)
                {
                    // Add an error message to the ModelState
                    ModelState.AddModelError("", "Events cannot be created in the past");

                    // Return a 400 Bad Request HTTP status code with the error messages
                    return BadRequest(ModelState);
                }

                return StatusCode(StatusCodes.Status201Created, eve);
            }
            else
                return StatusCode(StatusCodes.Status400BadRequest, eve);



        }
        [HttpGet]
        public async Task<IActionResult> ViewGoogleCalendar([FromQuery] ViewEvents parameters)
        {
            // Use the parameters object to access the specified date range and search query
            DateTime? startDate = parameters.FromDate;
            DateTime? endDate = parameters.ToDate;
            string searchQuery = parameters.SearchQuery;
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
            // Initialize the Google Calendar service and authenticate
            //var credential = await GoogleCredential.GetApplicationDefaultAsync();
            var service = new CalendarService(new BaseClientService.Initializer()
            {

                HttpClientInitializer = credential,
                ApplicationName = "Go Calendar",
            });

            // Set up the request to fetch events from the primary calendar
            var request = service.Events.List("primary");

            request.TimeMin = startDate;
            request.TimeMax = endDate;
            request.Q = searchQuery;
            request.MaxResults = 10; // Number of events to retrieve per page
            request.ShowDeleted = true;   //Show Deleted events 

            var allEvents = new List<Event>();


            // Fetch events from the primary calendar using pagination
            do
            {
                // Execute the request and retrieve the events
                var response = await request.ExecuteAsync();
                var events = response.Items;

                // Add the events to the overall list
                allEvents.AddRange(events);

                // Set the page token for the next page, if available
                request.PageToken = response.NextPageToken;
            } while (!string.IsNullOrEmpty(request.PageToken));
          
            return Ok(allEvents);
        }
       
        
    [HttpDelete("/api/events/{eventId}")]
        public async Task<IActionResult> DeleteEvent(string eventId)
        {
            try
            {
                // Use the eventId to delete the event from the user's Google Calendar
                String[] Scopes = { "https://www.googleapis.com/auth/calendar" };
                String ApplicationName = "Go Calendar";
                UserCredential credential;

                using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Credentials", "credentials.json"), FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)
                    ).Result;
                }

                // Initialize the Google Calendar service and authenticate
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Go Calendar",
                });
                //Check if there is an event with this eventID
                try
                {
                    // Retrieve the event details using the eventId
                    var existingEvent = await service.Events.Get("primary", eventId).ExecuteAsync();
                    // Delete the event using the eventId
                    await service.Events.Delete("primary", eventId).ExecuteAsync();

                    // Return a 204 No Content HTTP status code upon successful deletion
                    return NoContent();
                }
                catch (Google.GoogleApiException ex) when (ex.HttpStatusCode == HttpStatusCode.NotFound)
                {
                    // If the event does not exist, return a 404 Not Found HTTP status code
                    return NotFound();
                }
            

               
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur during the deletion process
                return StatusCode(500, $"An error occurred while deleting the event: {ex.Message}");
            }
        }
    }
}
