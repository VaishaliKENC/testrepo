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
/// class UserSessionAttendence : BaseEntity 
/// </summary>
/// 
public class UserSessionAttendence : BaseEntity
{
/// <summary>
/// Default Contructor
/// </summary>
public UserSessionAttendence()
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

private DateTime _strAttendenceFromDate;
/// <summary>
/// AttendenceFromDate
/// </summary>
public DateTime AttendenceFromDate
{
get { return _strAttendenceFromDate; }
set { if (this._strAttendenceFromDate != value) { _strAttendenceFromDate = value; } }
}

private DateTime _strAttendenceToDate;
/// <summary>
/// AttendenceToDate
/// </summary>
public DateTime AttendenceToDate
{
get { return _strAttendenceToDate; }
set { if (this._strAttendenceToDate != value) { _strAttendenceToDate = value; } }
}

private float _strAttendenceDays;
/// <summary>
/// AttendenceDays
/// </summary>
public float AttendenceDays
{
get { return _strAttendenceDays; }
set { if (this._strAttendenceDays != value) { _strAttendenceDays = value; } }
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