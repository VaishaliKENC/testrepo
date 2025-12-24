/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<12/27/2011>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
/// <summary>
/// class ProgramTypeMaster : BaseEntity 
/// </summary>
/// 
public class ProgramTypeMaster : BaseEntity
{
/// <summary>
/// Default Contructor
/// </summary>
public ProgramTypeMaster()
{ }


private string _strName;
/// <summary>
/// Name
/// </summary>
public string Name
{
get { return _strName; }
set { if (this._strName != value) { _strName = value; } }
}

private string _strDescription;
/// <summary>
/// Description
/// </summary>
public string Description
{
get { return _strDescription; }
set { if (this._strDescription != value) { _strDescription = value; } }
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