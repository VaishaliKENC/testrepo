/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Abhay
* Created:<12/26/2011>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
/// <summary>
/// class SupportResourceTypeMaster : BaseEntity 
/// </summary>
/// 
public class SupportResourceTypeMaster :BaseEntity
{
    /// <summary>
    /// Default Contructor
    /// </summary>
    public SupportResourceTypeMaster()
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
    /// VendorId
    /// </summary>
    public string VendorName
    {
        get { return _strVendorName; }
        set { if (this._strVendorName != value) { _strVendorName = value; } }
    }

    private string _strResourceType;
    /// <summary>
    /// ResourceType
    /// </summary>
    public string ResourceType
    {
        get { return _strResourceType; }
        set { if (this._strResourceType != value) { _strResourceType = value; } }
    }


    private bool _byteFileUploadRights;
    /// <summary>
    /// FileUploadRights
    /// </summary>
    public bool FileUploadRights
    {
        get { return _byteFileUploadRights; }
        set { if (this._byteFileUploadRights != value) { _byteFileUploadRights = value; } }
    }

    /// <summary>
    /// enum Method
    /// </summary>
    public new enum Method
    {
        Get,
        Add,
        Update,
        Delete
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