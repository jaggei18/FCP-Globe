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
    public class PolicyInformationFunctions : BaseLambdaFunction
    {
        public async Task<APIGatewayProxyResponse> Select_PolicyInf(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var response = JsonResponse();
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    PolicyInformationBillingRequest req = new();
                    req = JsonConvert.DeserializeObject<PolicyInformationBillingRequest>(request.Body);
                    context.Logger.LogInformation("Calls Select_PolicyInf function");

                    if (req != null)
                    {
                        var service = new PolicyInformationBillingService();

                        var res = await service.GetPolicyInformationBillingService(req.Stype, req.PolNo);
                        response.Body = JsonConvert.SerializeObject(res);
                        response.StatusCode = (int)HttpStatusCode.OK;
                        return response;
                    }
                }
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Body = "Please provide correct data.";
                return response;

            }
            catch (Exception ex)
            {
                context.Logger.LogInformation("Exception in Select_PolicyInf function --> " + ex.Message);
                return JsonResponse();
            }
        }

        public async Task<APIGatewayProxyResponse> Select_Beneficiary(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var response = JsonResponse();
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    PolicyInformationBilling_BeneficiaryRequest req = new();
                    req = JsonConvert.DeserializeObject<PolicyInformationBilling_BeneficiaryRequest>(request.Body);
                    context.Logger.LogInformation("Calls Select_Beneficiary function");

                    if (req != null)
                    {
                        var service = new PolicyInformationBillingService();

                        var res = await service.GetBeneficiaryService(req.PolNo);
                        response.Body = JsonConvert.SerializeObject(res);
                        response.StatusCode = (int)HttpStatusCode.OK;
                        return response;
                    }
                }
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Body = "Please provide correct data.";
                return response;

            }
            catch (Exception ex)
            {
                context.Logger.LogInformation("Exception in Select_Beneficiary function --> " + ex.Message);
                return JsonResponse();
            }
        }

    }
}
