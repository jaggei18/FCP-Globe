using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using FirstCommand.API.Base;
using FirstCommand.API.BLL.Services;
using FirstCommand.API.Database.Entities;
using Newtonsoft.Json;
using System.Net;


namespace FirstCommand.API.Lambdas
{
    public class AuthAgentFunctions: BaseLambdaFunction
    {
        public async Task<APIGatewayProxyResponse> AuthAgent(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var response = JsonResponse();
            try
            {
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    AuthAgentRequest authAgentRequest = new AuthAgentRequest();
                    authAgentRequest = JsonConvert.DeserializeObject<AuthAgentRequest>(request.Body);
                    context.Logger.LogInformation("Call AuthAgent function");

                    if (authAgentRequest != null)
                    {
                        bool bAllAccessInd = false;
                        if (authAgentRequest.role.ToUpper() == "ADMIN") { 
                            bAllAccessInd = true;
                        }
                         
                        var AuthAgentService = new AuthAgentService();
                        var authAgentResponse = await AuthAgentService.GetAuthAgentInfo(authAgentRequest.agentID, bAllAccessInd);

                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Body = JsonConvert.SerializeObject(authAgentResponse);
                        return response;
                    }
                }
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Body = "Please provide correct data.";
                return response;

            }
            catch (Exception ex)
            {
                context.Logger.LogInformation("Exception in AuthAgent Function" + ex.Message);
                return JsonResponse();
            }

        }
    }
}
