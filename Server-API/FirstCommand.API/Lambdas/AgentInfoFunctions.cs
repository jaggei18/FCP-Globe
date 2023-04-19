using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using FirstCommand.API.Base;
using FirstCommand.API.BLL.Services;
using FirstCommand.API.Database.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace FirstCommand.API.Lambdas
{
    public class AgentInfoFunctions : BaseLambdaFunction
    {
        public AgentInfoFunctions() { }

        private readonly ILogger<AgentInfoFunctions> _logger;

        public AgentInfoFunctions(ILogger<AgentInfoFunctions> logger)
        {
            _logger = logger;
        }

        public async Task<APIGatewayProxyResponse> AgentInfo(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var response = JsonResponse();
            try
            {
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    AgentInfoRequest agentInfoRequest = new AgentInfoRequest();
                    agentInfoRequest = JsonConvert.DeserializeObject<AgentInfoRequest>(request.Body);
                    context.Logger.LogInformation("Call AgentInfo function");
                    //_logger.LogInformation("Call AgentInfo function");

                    if (agentInfoRequest != null)
                    {
                        var agentInfo_Service = new AgentInfoService();
                        var agentInfo_Response = await agentInfo_Service.GetAgentService(agentInfoRequest.AgentNumber);
                        if (agentInfo_Response.Any())
                        {
                            response.StatusCode = (int)HttpStatusCode.OK;
                            response.Body = JsonConvert.SerializeObject(agentInfo_Response);
                        }
                        else
                        {
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                            response.Body = "Record not found.";
                        }
                        return response;
                    }
                }
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Body = "Please provide correct data.";
                return response;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Exception AgentInfo Function" + ex.Message);
                context.Logger.LogInformation("Exception AgentInfo Function" + ex.Message);
                return JsonResponse();
            }

        }

    }

}
