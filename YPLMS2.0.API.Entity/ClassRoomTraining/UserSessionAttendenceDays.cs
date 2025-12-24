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
/// class UserSessionAttendenceDays : BaseEntity 
/// </summary>
/// 
public class UserSessionAttendenceDays : BaseEntity
{
/// <summary>
/// Default Contructor
/// </summary>
public UserSessionAttendenceDays()
{ }


private string _strSystemUserGuId;
/// <summary>
/// SystemUserGuId
/// </summary>
public string SystemUserGuId
{
get { return _strSystemUserGuId; }
set { if (this._strSystemUserGuId != value) { _strSystemUserGuId = value; } }
}

private string _strAttendenceDate;
/// <summary>
/// AttendenceDate
/// </summary>
public string AttendenceDate
{
get { return _strAttendenceDate; }
set { if (this._strAttendenceDate != value) { _strAttendenceDate = value; } }
}

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

private string _strattendence;
/// <summary>
/// attendence
/// </summary>
public string attendence
{
get { return _strattendence; }
set { if (this._strattendence != value) { _strattendence = value; } }
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