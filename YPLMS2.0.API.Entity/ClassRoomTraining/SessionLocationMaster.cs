/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<12/23/2011>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
/// <summary>
/// class SessionLocationMaster : BaseEntity 
/// </summary>
/// 
public class SessionLocationMaster : SessionLocationLanguage
{
/// <summary>
/// Default Contructor
/// </summary>
public SessionLocationMaster()
{

}

    //private string _strLocationName;
    ///// <summary>
    ///// LocationName
    ///// </summary>
    //public string LocationName
    //{
    //    get { return _strLocationName; }
    //    set { if (this._strLocationName != value) { _strLocationName = value; } }
    //}

    //private string _strLocationVenue;
    ///// <summary>
    ///// LocationVenue
    ///// </summary>
    //public string LocationVenue
    //{
    //    get { return _strLocationVenue; }
    //    set { if (this._strLocationVenue != value) { _strLocationVenue = value; } }
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
        UpdateLanguage,
        DeleteLocationLanguage
    }

/// <summary>
/// enum ListMethod
/// </summary>
    public new enum ListMethod
    {
        GetAll,
        GetLocationLanguages,
        ActivateDeActivateStatus
    }

    private Nullable<bool> _isActive;
    public Nullable<bool> IsActive
    {
        get { return _isActive; }
        set { if (this._isActive != value) { _isActive = value; } }
    }

    private string _strSessionLocationId;
    /// <summary>
    /// SessionLocationId
    /// </summary>
    public string SessionLocationId
    {
        get { return _strSessionLocationId; }
        set { if (this._strSessionLocationId != value) { _strSessionLocationId = value; } }
    }
}
}