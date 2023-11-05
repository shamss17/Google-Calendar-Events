using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Google_Calendar.Helper
{
    public class DeleteEventHelper
    {
        public static async Task<List<Event>> GetEvents()
        {
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

            // Set up the request to fetch events from the primary calendar
            var request = service.Events.List("primary");
            request.MaxResults = 10; // Number of events to retrieve per page

            var response = await request.ExecuteAsync();
            var events = response.Items;

            return events.ToList();
        }
    }
}
