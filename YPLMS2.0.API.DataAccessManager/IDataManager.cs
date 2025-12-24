/*
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh & Ashish
* Created:<25/03/10>
* Last Modified:<25/03/10>
*/
using YPLMS2._0.API.Entity;
using System.Collections.Generic;
namespace YPLMS2._0.API.DataAccessManager
{
    /// <summary>
    /// Base interface for all Data Access Managers.
    /// </summary>
    public interface IDataManager<T>
    {        

        /// <summary>
        /// Return Filled object
        /// </summary>
        /// <param name="pEntBase">Object with Search Criteria</param>
        /// <returns>Filled object</returns>
        T Get(T pEntBase);

        /// <summary>
        /// Update Filled object
        /// </summary>
        /// <param name="pEntBase">Object with filled properties</param>
        /// <returns>Updated object</returns>
        T Update(T pEntBase);
    }
}