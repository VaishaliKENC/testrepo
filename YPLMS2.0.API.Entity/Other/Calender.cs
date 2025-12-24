using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class Calender
    {
        public Calender()
        { }
        public enum ListMethod
        {
            GetAll,
            GetResponsive
        }
        private string _id;
        public string id
        {
            get { return _id; }
            set { if (this._id != value) { _id = value; } }
        }
        private string _clientId;
        /// <summary>
        /// To Get Entity object ClientId
        /// </summary>
        public string ClientId
        {
            get { return _clientId; }
            set { if (this._clientId != value) { _clientId = value; } }
        }
        private string _systemUserGUId;
        /// <summary>
        /// To Get Entity object ClientId
        /// </summary>
        public string SystemUserGUId
        {
            get { return _systemUserGUId; }
            set { if (this._systemUserGUId != value) { _systemUserGUId = value; } }
        }

        private string _title;
        public string title
        {
            get { return _title; }
            set { if (this._title != value) { _title = value; } }
        }

        private string _description;
        public string description
        {
            get { return _description; }
            set { if (this._description != value) { _description = value; } }
        }

        private long _event_start;

        public long event_start
        {
            get { return _event_start; }
            set { if (this._event_start != value) { _event_start = value; } }
        }

        private long _event_end;

        public long event_end
        {
            get { return _event_end; }
            set { if (this._event_end != value) { _event_end = value; } }
        }

        public DateTime _start;
        public DateTime start
        {
            get  { return _start; }
            set  { if (this._start != value) { _start = value; } }
        }

        public DateTime _end;
        public DateTime end
        {
            get { return _end; }
            set { if (this._end != value) { _end = value; } }
        }

        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { if (this._Status != value) { _Status = value; } }
        }

        private Nullable<Boolean> _allDay;
        /// <summary>
        /// Is Active
        /// </summary>
        public Nullable<Boolean> allDay
        {
            get { return _allDay; }
            set { if (this._allDay != value) { _allDay = value; } }
        }

        private string _URL;
        public string URL
        {
            get { return _URL; }
            set { if (this._URL != value) { _URL = value; } }
        }
      
    }
}
