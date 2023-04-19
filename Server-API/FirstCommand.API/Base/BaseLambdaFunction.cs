using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Net;
// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
//[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace FirstCommand.API.Base
{
    public class BaseLambdaFunction
    {
        internal static APIGatewayProxyResponse JsonResponse()
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Body = "",
                Headers = new Dictionary<string, string> {
                { "Content-Type", "application/json" },
                { "Access-Control-Allow-Origin", "*" }//,
                  //{ "Access-Control-Allow-Methods", "*" },
                  //{ "Access-Control-Allow-Headers", "*" },
                  //{ "Access-Control-Max-Age", "300" }
                }
            };
        }
    }
}
