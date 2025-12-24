using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity.Configuration;

namespace YPLMS2._0.API.DataAccessManager.Configuration
{

    public class ClientFeatureConfigurationDAM : DataAccess
    {
        // passing in false means we want the master connection string, not the client
        public ClientFeatureConfigurationDAM()
            : base(false)
        {
        }

        public ClientFeatureConfiguration GetForClient(string clientId)
        {
            const string storedProcedure = "sproc_ClientFeatures_sel";

            SqlParameter parameter = new SqlParameter("ClientID", SqlDbType.VarChar, 100);
            parameter.Value = clientId;

            ClientFeatureConfiguration configuration = null;
            Action<SqlDataReader> onReaderCreated = r => configuration = PopulateClientFeatureConfiguration(r, clientId);
            ExecuteReader(storedProcedure, clientId, onReaderCreated, parameter);

            return configuration;
        }

        public ClientFeatureMatrix GetClientFeatureMatrix()
        {
            var matrix = new ClientFeatureMatrix();

            // fill features
            string storedProcedure = "sproc_Features_selAll";
            Action<SqlDataReader> onReaderCreated = r => PopulateClientFeatureMatrixWithFeatures(matrix, r);
            ExecuteReader(storedProcedure, "1", onReaderCreated, new SqlParameter[0]);

            // fill clients
            storedProcedure = "sproc_ClientMaster_LstAllActive";
            onReaderCreated = r => PopulateClientFeatureMatrixWithClients(matrix, r);
            ExecuteReader(storedProcedure, "1", onReaderCreated, new SqlParameter[0]);

            // fill matrix
            storedProcedure = "sproc_ClientFeatures_selAll";
            onReaderCreated = r => PopulateClientFeatureMatrix(matrix, r);
            ExecuteReader(storedProcedure, "1", onReaderCreated, new SqlParameter[0]);

            return matrix;
        }

        public void Save(ClientFeatureMatrix cfm)
        {
            // save changes
            string sprocEnable = "sproc_ClientFeatures_Enable";
            string sprocDisable = "sproc_ClientFeatures_Disable";
            string sproc = String.Empty;

            foreach (var client in cfm.ChangedValues.Keys)
            {
                var clientFeatures = cfm.ChangedValues[client];
                foreach (var feature in clientFeatures.Keys)
                {
                    sproc = clientFeatures[feature] ? sprocEnable : sprocDisable;
                    SqlParameter[] parameters = new[]
                                            {
                                                new SqlParameter("ClientId", SqlDbType.VarChar, 100)
                                                    {
                                                            Value = client
                                                    },
                                                new SqlParameter("FeatureId", SqlDbType.Int)
                                                {
                                                    Value = feature
                                                }
                                            };
                    ExecuteNonQuery(sproc, "1", parameters);
                }
            }
        }

        private static void PopulateClientFeatureMatrixWithFeatures(ClientFeatureMatrix matrix, SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    matrix.AddFeature(Convert.ToInt32(reader["FeatureId"]), reader["Name"].ToString(), reader["Description"].ToString());
                }
            }
        }

        private static void PopulateClientFeatureMatrixWithClients(ClientFeatureMatrix matrix, SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    matrix.AddClient(reader["ClientId"].ToString(), reader["ClientName"].ToString());
                }
            }
        }

        private static void PopulateClientFeatureMatrix(ClientFeatureMatrix matrix, SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    matrix.LoadFeatureFromDB(reader["ClientId"].ToString(),
                                             Convert.ToInt32(reader["FeatureId"]));
                }
            }
        }

        private static ClientFeatureConfiguration PopulateClientFeatureConfiguration(SqlDataReader reader, string clientId)
        {
            var configuration = new ClientFeatureConfiguration();
            configuration.ClientId = clientId;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    reader.SetValueForColumn<string>("Name",
                                                     i => configuration.AddEnabledFeature(i));
                }
            }
            return configuration;
        }
    }
}
