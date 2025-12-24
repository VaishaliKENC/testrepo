using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CalendarEvent
/// </summary>
[Serializable]
public class ResponsiveCalendarEvent
{
    public string id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string url { get; set; }
    public string _class{ get; set; }
    public string start { get; set; }
    public string end { get; set; }
    public string activitytypeid { get; set; }
    public string completestatus { get; set; }
    public string systemuserguid { get; set; }
    public string usernamealias { get; set; }
     
}
