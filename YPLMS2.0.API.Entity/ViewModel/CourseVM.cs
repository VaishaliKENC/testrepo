using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity.ViewModel
{
    public class CourseVM: BaseEntityVM
    {
        private string _strFileURL;
        /// <summary>
        /// Content Module URL
        /// </summary>
        public string FileURL
        {
            get { return _strFileURL; }
            set { if (_strFileURL != value) { _strFileURL = value; } }
        }
        private string? _Keyword;
        public string? Keyword
        {
            get { return _Keyword; }
            set { if (this._Keyword != value) { _Keyword = value; } }
        }

    }

    public class FileVm
    {
        private string? _Name;
        public string? Name
        {
            get { return _Name; }
            set { if (this._Name != value) { _Name = value; } }
        }
        private string? _Path;
        public string? Path
        {
            get { return _Path; }
            set { if (this._Path != value) { _Path = value; } }
        }
        private bool? _IsFile;
        public bool? IsFile
        {
            get { return _IsFile; }
            set { if (this._IsFile != value) { _IsFile = value; } }
        }
        private string? _Type;
        public string? Type
        {
            get { return _Type; }
            set { if (this._Type != value) { _Type = value; } }
        }
        private double? _Size;
        public double? Size
        {
            get { return _Size; }
            set { if (this._Size != value) { _Size = value; } }
        }
        private DateTime? _LastModified;
        public DateTime? LastModified
        {
            get { return _LastModified; }
            set { if (this._LastModified != value) { _LastModified = value; } }
        }
    }
}
