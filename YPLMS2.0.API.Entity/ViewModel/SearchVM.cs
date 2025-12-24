using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity
{
    
    public class SearchVM : BaseEntityVM
    {
        //public SearchVM()
        //{
        //    _entSearchObject = new List<BaseEntityVM>();
        //}



        private string? _strKeyword;
        private string? _strBussinessRuleId;
        private string? _strManagerName;
        /// <summary>
        /// Key word
        /// </summary>
        public string? KeyWord
        {
            get { return _strKeyword; }
            set { if (this._strKeyword != value) { _strKeyword = value; } }
        }

        public string? BussinessRuleId
        {
            get { return _strBussinessRuleId; }
            set { if (this._strBussinessRuleId != value) { _strBussinessRuleId = value; } }
        }

        public string? ManagerName
        {
            get { return _strManagerName; }
            set { if (this._strManagerName != value) { _strManagerName = value; } }

        }
        // private List<BaseEntityVM>? _entSearchObject;
        /// <summary>
        /// Search Object
        /// </summary>
        //public List<BaseEntityVM>? SearchObject
        //{
        //    get { return _entSearchObject; }
        //    set { if (this._entSearchObject != value) { _entSearchObject = value; } }
        //}
        public List<object>? SearchObject { get; set; }



        public UserSearchCriteria? UserCriteria { get; set; }      

        private string? _strSessionId;
        /// <summary>
        /// SessionId
        /// </summary>
        public string? SessionId
        {
            get { return _strSessionId; }
            set { if (this._strSessionId != value) { _strSessionId = value; } }
        }

        private string? _strProgramId;
        /// <summary>
        /// SessionId
        /// </summary>
        public string? ProgramId
        {
            get { return _strProgramId; }
            set { if (this._strProgramId != value) { _strProgramId = value; } }
        }

        private string? _strSystemUserGUID;
        /// <summary>
        /// SessionId
        /// </summary>
        public string? SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string? _strLockType;

        public string? LockType
        {
            get { return _strLockType; }
            set { if (this._strLockType != value) { _strLockType = value; } }
        }
      
    }

}
