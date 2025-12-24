/*
* Copyright INDECOMM GLOBAL SERVICES
* This source file and source code is proprietary property of INDECOMM GLOBAL SERVICES
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* INDECOMM GLOBAL SERVICES’s Client.
* Author:
* Created:<12/23/2011>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
/// <summary>
/// class SpeakerMaster : BaseEntity 
/// </summary>
/// 
public class SpeakerMaster : SpeakerLanguage
{
/// <summary>
/// Default Contructor
/// </summary>
public SpeakerMaster()
{ }


    private string _strVendorId;
/// <summary>
/// VendorId
/// </summary>
    public string VendorId
    {
    get { return _strVendorId; }
    set { if (this._strVendorId != value) { _strVendorId = value; } }
    }

    private string _strVendorName;
    /// <summary>
    /// VendorName
    /// </summary>
    public string VendorName
    {
        get { return _strVendorName; }
        set { if (this._strVendorName != value) { _strVendorName = value; } }
    }

    private Nullable<bool> _isActive;
    public Nullable<bool> IsActive
    {
        get { return _isActive; }
        set { if (this._isActive != value) { _isActive = value; } }
    }

    //private string _strSpeakerName;
    ///// <summary>
    ///// SpeakerName
    ///// </summary>
    //public string SpeakerName
    //{
    //    get { return _strSpeakerName; }
    //    set { if (this._strSpeakerName != value) { _strSpeakerName = value; } }
    //}

    //private string _strEmail;
    ///// <summary>
    ///// Email
    ///// </summary>
    //public string Email
    //{
    //get { return _strEmail; }
    //set { if (this._strEmail != value) { _strEmail = value; } }
    //}

    //private string _strSpeakerDetails;
    ///// <summary>
    ///// Email
    ///// </summary>
    //public string SpeakerDetails
    //{
    //    get { return _strSpeakerDetails; }
    //    set { if (this._strSpeakerDetails != value) { _strSpeakerDetails = value; } }
    //}


/// <summary>
/// enum Method
/// </summary>
    public new enum Method
    {
        Get,
        Add,
        Update,
        Delete,
        UpdateLanguage
    }

/// <summary>
/// enum ListMethod
/// </summary>
    public new enum ListMethod
    {
        GetAll,
        GetSpeakerLanguages,
        ActivateDeActivateStatus
    }
}
}