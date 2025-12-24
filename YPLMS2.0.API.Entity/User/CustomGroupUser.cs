/* 
* Copyright Brainvisa Technologies Pvt. Ltd.
* This source file and source code is proprietary property of Brainvisa Technologies Pvt. Ltd. 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Brainvisa’s Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// CustomGroupUser Class inheridated from Learner
    /// </summary>
    public class CustomGroupUser : Learner
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public CustomGroupUser()
        { }

        private string _strCustomGroupId;
        /// <summary>
        /// Custom Group Id
        /// </summary>
        public string CustomGroupId
        {
            get { return _strCustomGroupId; }
            set { if (this._strCustomGroupId != value) { _strCustomGroupId = value; } }
        }

        private bool _bIsAdmin;
        /// <summary>
        /// To check Is Admin
        /// </summary>
        public bool IsAdmin
        {
            get { return _bIsAdmin; }
            set { if (this._bIsAdmin != value) { _bIsAdmin = value; } }
        }

        public bool ValidateChild(bool pIsUpdate)
        {
            if (!pIsUpdate)
            {
                if (String.IsNullOrEmpty(CreatedById))
                    return false;
            }

            if (String.IsNullOrEmpty(CustomGroupId))
                return false;

            if (String.IsNullOrEmpty(ID))
                return false;

            if (String.IsNullOrEmpty(LastModifiedById))
                return false;

            return true;
        }

    }
}