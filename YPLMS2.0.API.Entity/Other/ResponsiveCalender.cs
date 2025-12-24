using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class ResponsiveCalender
    {
        public ResponsiveCalender()
        { 
        }
        public enum ListMethod
        {
            GetAll,
            GetAllAdmin
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

        private string _URL;
        public string URL
        {
            get { return _URL; }
            set { if (this._URL != value) { _URL = value; } }
        }

        private string _DATETYPE;
        public string DATETYPE
        {
            get { return _DATETYPE; }
            set { if (this._DATETYPE != value) { _DATETYPE = value; } }
        }

        private string _ACTIVITYTYPEID;
        public string ACTIVITYTYPEID
        {
            get { return _ACTIVITYTYPEID; }
            set { if (this._ACTIVITYTYPEID != value) { _ACTIVITYTYPEID = value; } }
        }

        private string _CompletionStatus;
        public string CompletionStatus
        {
            get { return _CompletionStatus; }
            set { if (this._CompletionStatus != value) { _CompletionStatus = value; } }
        }
    
      
    }
}
