/* 
* Copyright Brainvisa Technologies Pvt. Ltd.
* This source file and source code is proprietary property of Brainvisa Technologies Pvt. Ltd. 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Brainvisa’s Client.
* Author:Rajendra
* Created:<22/07/10>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class CDynamicVarValue:BaseEntity 
    /// </summary>
    [Serializable]
    public class CDynamicVarValue : BaseEntity
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CDynamicVarValue()
        { }

        
        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get
        }


       private string _passPhrase;

        public string PassPhrase
        {
            get { return _passPhrase; }
            set { _passPhrase = value; }
        }
        private string _initVector;

        public string InitVector
        {
            get { return _initVector; }
            set { _initVector = value; }
        }
        private int _minSaltLen;

        public int MinSaltLen
        {
            get { return _minSaltLen; }
            set { _minSaltLen = value; }
        }
        private int _maxSaltLen;

        public int MaxSaltLen
        {
            get { return _maxSaltLen; }
            set { _maxSaltLen = value; }
        }
        private int _keySize;

        public int KeySize
        {
            get { return _keySize; }
            set { _keySize = value; }
        }
        private string _hashAlgorithm;

        public string HashAlgorithm
        {
            get { return _hashAlgorithm; }
            set { _hashAlgorithm = value; }
        }
        private string _saltValue;

        public string SaltValue
        {
            get { return _saltValue; }
            set { _saltValue = value; }
        }
        private int _passwordIterations;

        public int PasswordIterations
        {
            get { return _passwordIterations; }
            set { _passwordIterations = value; }
        }
        

       
    }

}