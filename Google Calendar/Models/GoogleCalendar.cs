//The Model to use for CreateEvents

namespace Google_Calendar.Models
{
    public class GoogleCalendar
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }
        
    }
}
