//The Model to use for ViewEvents

namespace Google_Calendar.Models
{
    public class ViewEvents
    {
        //The info is from Google Calendar API documentation

        //Calendar ID
        public string ICalUID { get; set; }

        /// Maximum number of events returned on one result page. By default the value is 250 events. The page size can never be larger than 2500 events. Optional.
        public int MaxResults { get; set; }

        /// Free text search terms to find events that match these terms in any field, except for extended properties.  <summary>
        /// In this case using Date Range
        /// </summary>
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        //Enter value of the Description to search for
        public string SearchQuery {  get; set; }
        /// Whether to include deleted events (with status equals "cancelled") in the result. Cancelled instances of recurring events (but not the underlying recurring event) will still be included if showDeleted and singleEvents are both False. If showDeleted and singleEvents are both True, only single instances of deleted events (but not the underlying recurring events) are returned. Optional. The default is False.
        public bool ShowDeleted { get; set; }
    }
}
