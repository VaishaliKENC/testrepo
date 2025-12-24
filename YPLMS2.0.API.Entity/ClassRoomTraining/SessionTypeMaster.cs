/*
* Copyright Encora* This source file and source code is proprietary property of Encora
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
/// class SessionTypeMaster : BaseEntity 
/// </summary>
/// 
public class SessionTypeMaster : BaseEntity
{
/// <summary>
/// Default Contructor
/// </summary>
public SessionTypeMaster()
{ }


private string _strSessionType;
/// <summary>
/// SessionType
/// </summary>
public string SessionType
{
get { return _strSessionType; }
set { if (this._strSessionType != value) { _strSessionType = value; } }
}

private string _strSessionTypeDescription;
/// <summary>
/// SessionTypeDescription
/// </summary>
public string SessionTypeDescription
{
get { return _strSessionTypeDescription; }
set { if (this._strSessionTypeDescription != value) { _strSessionTypeDescription = value; } }
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