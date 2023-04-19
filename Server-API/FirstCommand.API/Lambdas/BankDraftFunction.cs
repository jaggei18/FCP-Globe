using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Azure;
using FirstCommand.API.Base;
using FirstCommand.API.BLL.Services;
using FirstCommand.API.Database.Entities;
using Newtonsoft.Json;
using System.Net;

namespace FirstCommand.API.Lambdas
{
    public class BankDraftFunction : BaseLambdaFunction
    {
        public async Task<APIGatewayProxyResponse> Select_BankDraft(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var response = JsonResponse();
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    BankDraftRequest bankDraftRequest = new();
                    bankDraftRequest = JsonConvert.DeserializeObject<BankDraftRequest>(request.Body);
                    context.Logger.LogInformation("Calls Select_BankDraft function");

                    if (bankDraftRequest != null)
                    {
                        var bankDraftService = new BankDraftService();
                        switch (bankDraftRequest.Stype)
                        {
                            case 1:
                                var bankDraftStype1Response = await bankDraftService.GetBankDraftSType1Service(bankDraftRequest.Stype, bankDraftRequest.PolNo, bankDraftRequest.Company);
                                response.StatusCode = (int)HttpStatusCode.OK;
                                response.Body = JsonConvert.SerializeObject(bankDraftStype1Response);
                                break;
                            case 2:
                                var bankDraftStype2Response = await bankDraftService.GetBankDraftSType2Service(bankDraftRequest.Stype, bankDraftRequest.PolNo, bankDraftRequest.Company);
                                response.StatusCode = (int)HttpStatusCode.OK;
                                response.Body = JsonConvert.SerializeObject(bankDraftStype2Response);
                                break;
                            case 3:
                                var bankDraftStype3Response = await bankDraftService.GetBankDraftSType3Service(bankDraftRequest.Stype, bankDraftRequest.PolNo, bankDraftRequest.Company);
                                response.StatusCode = (int)HttpStatusCode.OK;
                                response.Body = JsonConvert.SerializeObject(bankDraftStype3Response);
                                break;
                            default:
                                response.StatusCode = (int)HttpStatusCode.NotFound;
                                response.Body = "Please provide correct data.";
                                break;
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
                context.Logger.LogInformation("Exception in Select_BankDraft function --> " + ex.Message);
                return JsonResponse();
            }

        }        
    }
}
