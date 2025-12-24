using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class TimeZoneMaster:BaseEntity
    {
        public TimeZoneMaster() 
        { }



        private string _strTimeZoneId;
        /// <summary>
        /// TimeZoneId
        /// </summary>
        public string TimeZoneId
        {
            get { return _strTimeZoneId; }
            set { if (this._strTimeZoneId != value) { _strTimeZoneId = value; } }
        }

        private string _strTimeZoneValue;
        /// <summary>
        /// TimeZoneValue
        /// </summary>
        public string TimeZoneValue
        {
            get { return _strTimeZoneValue; }
            set { if (this._strTimeZoneValue != value) { _strTimeZoneValue = value; } }
        }


        private string _strTimeZonename;
        /// <summary>
        /// TimeZoneValue
        /// </summary>
        public string TimeZoneName
        {
            get { return _strTimeZonename; }
            set { if (this._strTimeZonename != value) { _strTimeZonename = value; } }
        }

        private string _strTimeZoneStandard;
        /// <summary>
        /// TimeZoneValue
        /// </summary>
        public string TimeZoneStandard
        {
            get { return _strTimeZoneStandard; }
            set { if (this._strTimeZoneStandard != value) { _strTimeZoneStandard = value; } }
        }

        public new enum Method
        {
            Get
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll            
        }

    }
}
