using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity.Configuration
{
   [Serializable] public class ClientFeatureMatrix
    {
        private Dictionary<string,ClientFeatureMatrixClient> _clients = new Dictionary<string, ClientFeatureMatrixClient>();
        private Dictionary<int, ClientFeatureMatrixFeature> _features = new Dictionary<int, ClientFeatureMatrixFeature>();
        // Client - Feature - Enabled
        private Dictionary<string, Dictionary<int, bool>> _enabled = new Dictionary<string, Dictionary<int, bool>>();
        // Only changed values
        private Dictionary<string, Dictionary<int, bool>> _enabledChanges =
            new Dictionary<string, Dictionary<int, bool>>();

        public List<ClientFeatureMatrixClient> Clients
        {
            get { return _clients.Values.ToList(); }
        }

        public List<ClientFeatureMatrixFeature> Features
        {
            get { return _features.Values.ToList(); }
        }

        public bool IsFeatureEnabled(string ClientId, int FeatureId)
        {
            if (_enabledChanges.ContainsKey(ClientId))
            {
                Dictionary<int, bool> featureDict = _enabledChanges[ClientId];
                if (featureDict.ContainsKey(FeatureId))
                {
                    return featureDict[FeatureId];
                }

            }
            if (_enabled.ContainsKey(ClientId))
            {
                Dictionary<int, bool> featureDict = _enabled[ClientId];
                if (featureDict.ContainsKey(FeatureId)) 
                {
                    return true;
                }
            }
            return false;
        }

        public Dictionary<string, Dictionary<int, bool>> ChangedValues
        {
            get { return _enabledChanges; }
        }

        public void AddFeature(int FeatureId, string FeatureName, string FeatureDescription)
        {
            _features.Add(FeatureId, new ClientFeatureMatrixFeature(FeatureId, FeatureName, FeatureDescription));
        }

        public void AddClient(string ClientId, string ClientName)
        {
            _clients.Add(ClientId, new ClientFeatureMatrixClient(ClientId, ClientName));
        }

        // use this to load features from the db
        public void LoadFeatureFromDB(string ClientId, int FeatureId)
        {
            // set enabled matrix
            if (_enabled.ContainsKey(ClientId))
            {
                Dictionary<int, bool> featureDict = _enabled[ClientId];
                if (featureDict.ContainsKey(FeatureId))
                {
                    featureDict[FeatureId] = true;
                }
                else
                {
                    featureDict.Add(FeatureId, true);
                }
            }
            else
            {
                _enabled.Add(ClientId, new Dictionary<int, bool>());
                _enabled[ClientId].Add(FeatureId, true);
            }
        }
        
        // use this to change a feature for later saving
        public void ChangeFeatureEnabled(string ClientId, int FeatureId, bool Enabled)
        {
            if (_enabledChanges.ContainsKey(ClientId))
            {
                Dictionary<int, bool> featureDict = _enabledChanges[ClientId];
                if (featureDict.ContainsKey(FeatureId))
                {
                    featureDict[FeatureId] = Enabled;
                } else
                {
                    featureDict.Add(FeatureId, Enabled);
                }
            } else
            {
                _enabledChanges.Add(ClientId, new Dictionary<int, bool>());
                _enabledChanges[ClientId].Add(FeatureId, Enabled);
            }
        }

    }

   [Serializable] public class ClientFeatureMatrixClient
    {
        public string Id;
        public string Name;
        public ClientFeatureMatrixClient(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }

   [Serializable] public class ClientFeatureMatrixFeature
    {
        public int Id;
        public string Name;
        public string Description;
        public ClientFeatureMatrixFeature(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }

}
