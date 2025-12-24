using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity.ViewModel
{
    public class ReportVM : BaseEntityVM
    {
        private string? _LanguageId;
        public string? LanguageId
        {
            get { return _LanguageId; }
            set { if (this._LanguageId != value) { _LanguageId = value; } }
        }
        public string? _strUserID;
        /// <summary>
        /// UserID
        /// </summary>
        public string? UserID
        {
            get { return _strUserID; }
            set { if (this._strUserID != value) { _strUserID = value; } }
        }
        private string strClientId;
        /// <summary>
        /// Where Clause
        /// </summary>
        public string ClientId
        {
            get
            {
                return strClientId;
            }
            set
            {
                if (this.strClientId != value)
                {
                    strClientId = value;
                }
            }
        }
    }
}
