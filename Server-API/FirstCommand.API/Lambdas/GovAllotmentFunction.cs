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
    public class GovAllotmentFunction : BaseLambdaFunction
    {
        public async Task<APIGatewayProxyResponse> Select_GovAllotment(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var response = JsonResponse();
                if (!string.IsNullOrWhiteSpace(request.Body))
                {
                    GovAllotmentRequest req = new();
                    req = JsonConvert.DeserializeObject<GovAllotmentRequest>(request.Body);
                    context.Logger.LogInformation("Calls Select_GovAllotment function");

                    if (req != null)
                    {
                        var service = new GovAllotmentService();
                        switch (req.Stype)
                        {
                            case 3:
                                var res3 = await service.GetGovAllotmentSType3Service(req.Stype, req.PolNo, req.SSN, req.Company);
                                response.StatusCode = (int)HttpStatusCode.OK;
                                response.Body = JsonConvert.SerializeObject(res3);
                                break;
                            case 4:
                                var res4 = await service.GetGovAllotmentSType4Service(req.Stype, req.PolNo, req.SSN, req.Company);
                                response.StatusCode = (int)HttpStatusCode.OK;
                                response.Body = JsonConvert.SerializeObject(res4);
                                break;
                            case 5:
                                var res5 = await service.GetGovAllotmentSType5Service(req.Stype, req.PolNo, req.SSN, req.Company);
                                response.StatusCode = (int)HttpStatusCode.OK;
                                response.Body = JsonConvert.SerializeObject(res5);
                                break;
                            case 6:
                                var res6 = await service.GetGovAllotmentSType6Service(req.Stype, req.PolNo, req.SSN, req.Company);
                                response.StatusCode = (int)HttpStatusCode.OK;
                                response.Body = JsonConvert.SerializeObject(res6);
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
                context.Logger.LogInformation("Exception in Select_GovAllotment function --> " + ex.Message);
                return JsonResponse();
            }

        }
    }
}
