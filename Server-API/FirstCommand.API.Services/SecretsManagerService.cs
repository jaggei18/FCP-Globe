using FirstCommand.API.Services.Models;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

namespace FirstCommand.API.Services
{
    public class SecretsManagerService
    {
        public DbUserModel? GetSecretAsync()
        {
            var secretName = "rds-db-credentials/cluster-LCKWFVMX2OQKKANB7KLNO76AOQ/admin/1675319577940";
            var region = "us-east-1";
            var client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));
            var request = new GetSecretValueRequest
            {
                SecretId = secretName,
            };

            var response = client.GetSecretValueAsync(request).Result;
            if (response.SecretString == null)
            {
                throw new ArgumentNullException(response.SecretString);
            }

            return JsonConvert.DeserializeObject<DbUserModel>(response.SecretString);
        }
    }
}
