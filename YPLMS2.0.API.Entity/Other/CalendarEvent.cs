using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CalendarEvent
/// </summary>
[Serializable]
public class CalendarEvent
{
    public string id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public long start { get; set; }
    public long end { get; set; }
    public bool allDay { get; set; }
    public string event_type { get; set; }
    public string Status { get; set; }
    
}
