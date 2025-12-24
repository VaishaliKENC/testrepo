using System.Collections.Generic;
using System;
namespace YPLMS2._0.API.Entity.Configuration
{
   [Serializable] public class ClientFeatureConfiguration
    {
        // Feature Names
        public const string FastImport = "FastImport";
        public const string ThreadlessAssignment = "ThreadlessAssignment";
        public const string AsynchronousForgotPasswordEmail = "AsynchronousForgotPasswordEmail";


        private Dictionary<string,bool> _enabledFeatures = new Dictionary<string, bool>();

        public string ClientId
        {
            get; set; 
        }

        public void AddEnabledFeature(string featureName)
        {
            _enabledFeatures.Add(featureName, true);
        }

        public bool IsFeatureEnabled(string featureName)
        {
            return _enabledFeatures.ContainsKey(featureName);
        }
    }
}
