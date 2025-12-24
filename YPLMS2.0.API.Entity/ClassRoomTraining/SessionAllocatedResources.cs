/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<12/26/2011>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
/// <summary>
/// class SessionAllocatedResources : BaseEntity 
/// </summary>
/// 
public class SessionAllocatedResources : BaseEntity
{
/// <summary>
/// Default Contructor
/// </summary>
public SessionAllocatedResources()
{ }


    private string _strSessionId;
/// <summary>
/// SessionId
/// </summary>
    public string SessionId
    {
    get { return _strSessionId; }
    set { if (this._strSessionId != value) { _strSessionId = value; } }
    }

    private string _strProgramId;
/// <summary>
/// SessionId
/// </summary>
    public string ProgramId
    {
        get { return _strProgramId; }
        set { if (this._strProgramId != value) { _strProgramId = value; } }
    }

    private string _strLanguageId;
    /// <summary>
    /// LanguageId
    /// </summary>
    public string LanguageId
    {
        get { return _strLanguageId; }
        set { if (this._strLanguageId != value) { _strLanguageId = value; } }
    }

    private string _strLanguageName;
    /// <summary>
    /// LanguageId
    /// </summary>
    public string LanguageName
    {
        get { return _strLanguageName; }
        set { if (this._strLanguageName != value) { _strLanguageName = value; } }
    }

    private string _strRefMaterialId;
/// <summary>
/// RefMaterialId
/// </summary>
    public string RefMaterialId
    {
        get { return _strRefMaterialId; }
        set { if (this._strRefMaterialId != value) { _strRefMaterialId = value; } }
    }

    private string _strRefMaterialDocumentName;
/// <summary>
/// RefMaterialDocumentId
/// </summary>
    public string RefMaterialDocumentName
    {
        get { return _strRefMaterialDocumentName; }
        set { if (this._strRefMaterialDocumentName != value) { _strRefMaterialDocumentName = value; } }
    }

    private string _strRefmaterialFileName;
/// <summary>
/// RefmaterialFileName
/// </summary>
    public string RefMaterialFileName
    {
        get { return _strRefmaterialFileName; }
        set { if (this._strRefmaterialFileName != value) { _strRefmaterialFileName = value; } }
    }


    private Nullable<bool> _isActive;
    /// <summary>
    /// IsActive
    /// </summary>
    public Nullable<bool> IsActive
    {
        get { return _isActive; }
        set { if (this._isActive != value) { _isActive = value; } }
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
        GetAll,
        GetAllAllocatedResourcesBySessionId,
        GetAllocatedResourcesForLearner,
        GetAllMappedResources
    }
    }
}