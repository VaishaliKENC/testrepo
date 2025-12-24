using System;

namespace YPLMS2._0.API.Entity
{
   [Serializable] public class SamlSsoConfiguration
    {
        public string ClientId
        {
            get;
            set;
        }

        public int SamlSsoConfigurationId
        {
            get;
            set;
        }

        public bool IsDeleted
        {
            get;
            set;
        }

        public string IdentityProviderEndpoint
        {
            get;
            set;
        }

        public string ServiceProviderEndpoint
        {
            get;
            set;
        }

        public string BindingMethodQueryString
        {
            get;
            set;
        }

        public string ServiceProviderToIdPBindingMethod
        {
            get;
            set;
        }

        public string IdentityProviderToSPBindingMethod
        {
            get;
            set;
        }

        public byte[] IdentityProvider509Certificate
        {
            get;
            set;
        }

        public string UserIdentifierFieldName
        {
            get;
            set;
        }

        public long UserIdentifierColumnID
        {
            get;
            set;
        }
    }
}
