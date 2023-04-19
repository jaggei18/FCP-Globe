using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using FirstCommand.API.Base;
using FirstCommand.API.BLL.Services;
using FirstCommand.API.Database.Entities;
using Newtonsoft.Json;
using System.Net;

namespace FirstCommand.API.Lambdas
{
    public class PendingDetailsFunction : BaseLambdaFunction
    {
        public async Task<APIGatewayProxyResponse> Select_PendingInf(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var response = JsonResponse();
            try
            {
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    PendPolicyDetailsRequest pendingDetailsInfo = new PendPolicyDetailsRequest();
                    pendingDetailsInfo = JsonConvert.DeserializeObject<PendPolicyDetailsRequest>(request.Body);
                    context.Logger.LogInformation("Call Select_PendingInf function");

                    if (pendingDetailsInfo != null)
                    {
                        var pendingDetailsService = new PendingDetailsService();
                        var pendInfo_Response = await pendingDetailsService.GetPendingDetailsService(pendingDetailsInfo.PolicyNumber);

                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Body = JsonConvert.SerializeObject(pendInfo_Response);
                        return response;
                    }
                }
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Body = "Please provide correct data.";
                return response;

            }
            catch (Exception ex)
            {
                context.Logger.LogInformation("Exception in Select_PendingInf Function" + ex.Message);
                return JsonResponse();
            }

        }

        public async Task<APIGatewayProxyResponse> Select_PendingNotes(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var response = JsonResponse();
            try
            {
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    PendPolicyDetailsRequest pendingNotesInfo = new PendPolicyDetailsRequest();
                    pendingNotesInfo = JsonConvert.DeserializeObject<PendPolicyDetailsRequest>(request.Body);
                    context.Logger.LogInformation("Call Select_PendingNotes function");

                    if (pendingNotesInfo != null)
                    {
                        var pendingNotesInfoService = new PendingDetailsService();
                        var pendInfo_Response = await pendingNotesInfoService.GetPendingNotesService(pendingNotesInfo.PolicyNumber);

                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Body = JsonConvert.SerializeObject(pendInfo_Response);
                        return response;
                    }
                }
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Body = "Please provide correct data.";
                return response;

            }
            catch (Exception ex)
            {
                context.Logger.LogInformation("Exception in Select_PendingNotes Function" + ex.Message);
                return JsonResponse();
            }

        }

    }
}
