using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using FirstCommand.API.Base;
using FirstCommand.API.BLL.Services;
using FirstCommand.API.Database.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Lambdas
{
    public class CommonFunctions : BaseLambdaFunction
    {
        /// <summary>
        /// Check Agent
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> CheckAgent(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var response = JsonResponse();
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    CheckAgentRequest checkAgentRequest = new();
                    checkAgentRequest = JsonConvert.DeserializeObject<CheckAgentRequest>(request.Body);
                    context.Logger.LogInformation("Calls CheckAgent function");

                    if (checkAgentRequest != null)
                    {
                        var checkAgentService = new CheckAgentService();

                        var checkAgentResponse = await checkAgentService.GetCheckAgentService(checkAgentRequest.PolicyNumber, checkAgentRequest.AgentNumber, checkAgentRequest.ServicingAgentInd);
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Body = JsonConvert.SerializeObject(checkAgentResponse);

                        return response;
                    }
                }
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Body = "Please provide correct data.";
                return response;

            }
            catch (Exception ex)
            {
                context.Logger.LogInformation("Exception in CheckAgent function --> " + ex.Message);
                return JsonResponse();
            }
        }

        public async Task<APIGatewayProxyResponse> InqVerify(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var response = JsonResponse();
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    InqVerifyRequest inqVerifyRequest = new();
                    inqVerifyRequest = JsonConvert.DeserializeObject<InqVerifyRequest>(request.Body);
                    context.Logger.LogInformation("Calls InqVerify function");

                    if (inqVerifyRequest != null)
                    {
                        var checkAgentService = new InqVerifyService();

                        var inqVerifyResponse = await checkAgentService.GetInqVerifyService(inqVerifyRequest.ID_User, inqVerifyRequest.PolicyNumber);
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Body = JsonConvert.SerializeObject(inqVerifyResponse);

                        return response;
                    }
                }
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Body = "Please provide correct data.";
                return response;

            }
            catch (Exception ex)
            {
                context.Logger.LogInformation("Exception in InqVerify function --> " + ex.Message);
                return JsonResponse();
            }
        }

        public async Task<APIGatewayProxyResponse> PendVerify(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var response = JsonResponse();
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    PendVerifyRequest pendVerifyRequest = new();
                    pendVerifyRequest = JsonConvert.DeserializeObject<PendVerifyRequest>(request.Body);
                    context.Logger.LogInformation("Calls PendVerify function");

                    if (pendVerifyRequest != null)
                    {
                        var pendVerifyService = new PendVerifyService();

                        var pendVerifyResponse = await pendVerifyService.GetPendVerifyService(pendVerifyRequest.AgentNumber, pendVerifyRequest.UserId);
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Body = JsonConvert.SerializeObject(pendVerifyResponse);

                        return response;
                    }
                }
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Body = "Please provide correct data.";
                return response;

            }
            catch (Exception ex)
            {
                context.Logger.LogInformation("Exception in PendVerify function --> " + ex.Message);
                return JsonResponse();
            }
        }

        public async Task<APIGatewayProxyResponse> AgentNumberXREF(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var response = JsonResponse();
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    AgentNumberXREFRequest agentNumberXREFRequest = new();
                    agentNumberXREFRequest = JsonConvert.DeserializeObject<AgentNumberXREFRequest>(request.Body);
                    context.Logger.LogInformation("Calls AgentNumberXREF function");

                    if (agentNumberXREFRequest != null)
                    {
                        var agentNumberXREFService = new AgentNumberXREFService();

                        var agentNumberXREFResponse = await agentNumberXREFService.GetAgentNumberXREFService(agentNumberXREFRequest.AgentNumber);
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Body = JsonConvert.SerializeObject(agentNumberXREFResponse);

                        return response;
                    }
                }
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Body = "Please provide correct data.";
                return response;

            }
            catch (Exception ex)
            {
                context.Logger.LogInformation("Exception in AgentNumberXREF function --> " + ex.Message);
                return JsonResponse();
            }
        }

        public async Task<APIGatewayProxyResponse> Coverage(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var response = JsonResponse();
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    PendPolicyDetailsRequest coverageInfo = new();
                    coverageInfo = JsonConvert.DeserializeObject<PendPolicyDetailsRequest>(request.Body);
                    context.Logger.LogInformation("Calls Coverage function");

                    if (coverageInfo != null)
                    {
                        var coverageService = new CoverageService();

                        var coverageResponse = await coverageService.GetCoverageService(coverageInfo.PolicyNumber);
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Body = JsonConvert.SerializeObject(coverageResponse);
                        return response;
                    }
                }
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Body = "Please provide correct data.";
                return response;
            }
            catch (Exception ex)
            {
                context.Logger.LogInformation("Exception in Coverage function --> " + ex.Message);
                return JsonResponse();
            }
        }
    }
}
