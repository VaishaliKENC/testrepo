/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Shrihari
* Created:<5/3/2019>
* Last Modified:
*/

using YPLMS2._0.API.Entity;
using System;

namespace YPLMS2._0.API.Entity
{
/// <summary>
/// class ProductLandingPageBannerLanguage : BaseEntity 
/// </summary>
/// 
public class ProductLandingPageBannerLanguage : BaseEntity
{
/// <summary>
/// Default Constructor
/// </summary>
public ProductLandingPageBannerLanguage()
{ }


private string _strLanguageID;
/// <summary>
/// LanguageID
/// </summary>
public string LanguageID
{
get { return _strLanguageID; }
set { if (this._strLanguageID != value) { _strLanguageID = value; } }
}

private string _strBannerAltText;
/// <summary>
/// BannerAltText
/// </summary>
public string BannerAltText
{
get { return _strBannerAltText; }
set { if (this._strBannerAltText != value) { _strBannerAltText = value; } }
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