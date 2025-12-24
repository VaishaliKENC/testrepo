using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
     [Serializable]
    public class UserPhoto : BaseEntity
    {
         public UserPhoto()
        {            
            
        }

         public new enum Method
         {
             GetImageName,
             UpdatePhoto,
             update,
             Add
            
         }

         public new enum ListMethod
         {
             GetImageName,
             UpdatePhoto
         }

         /// <summary>
         /// User Name Alias
         /// </summary>
         private string _strSystemUserGUID;
         /// <summary>
         /// User Name Alias
         /// </summary>
         public string SystemUserGUID
         {
             get { return _strSystemUserGUID; }
             set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
         }

         private string _strImageName;
         /// <summary>
         /// User Name Alias
         /// </summary>
         public string ImageName
         {
             get { return _strImageName; }
             set { if (this._strImageName != value) { _strImageName = value; } }
         }


         private bool _IsDisplay;
         /// <summary>
         /// User Name Alias
         /// </summary>
         public bool IsDisplay
         {
             get { return _IsDisplay; }
             set { if (this._IsDisplay != value) { _IsDisplay = value; } }
         }

         private bool _IsAllowUploadPhoto;
         /// <summary>
         /// User Name Alias
         /// </summary>
         public bool IsAllowUploadPhoto
         {
             get { return _IsAllowUploadPhoto; }
             set { if (this._IsAllowUploadPhoto != value) { _IsAllowUploadPhoto = value; } }
         }
    }
}
