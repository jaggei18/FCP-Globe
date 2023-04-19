using FirstCommand.API.Database;
using FirstCommand.API.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.BLL.Services
{
    public class CheckAgentService
    {
        private readonly FirstCommandDBContext _firstCommandDBContext;

        public CheckAgentService()
        {
            _firstCommandDBContext = new FirstCommandDBContext();
        }

        /// <summary>
        /// Get Check Agent Service
        /// </summary>
        /// <param name="sessionGUID"></param>
        /// <param name="policynum"></param>
        /// <param name="agentNumber"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CheckAgentResponse>> GetCheckAgentService(string? policynum, string? agentNumber, string servicingAgentInd)
        {
            try
            {
                var param = new SqlParameter[]
                            {
                    // Create parameter(s)    
                    new SqlParameter() { ParameterName = "@PolicyNumber", Value = policynum },
                    new SqlParameter() { ParameterName = "@AgentNumber", Value = agentNumber },
                    new SqlParameter() { ParameterName = "@ServicingAgentInd", Value = servicingAgentInd },
                    new SqlParameter() { ParameterName = "@ErrMessage", Value = "" }
                            };
                var result = await _firstCommandDBContext.CheckAgentResponse.FromSqlRaw("EXECUTE IsAgentServicingAgent_AWS @PolicyNumber, @AgentNumber, @ServicingAgentInd Output, @ErrMessage Output Select @ServicingAgentInd AS ServicingAgentInd, @ErrMessage AS ErrMessage", param).ToListAsync();
                return result != null ? result : new List<CheckAgentResponse>();
                //CheckAgentResponse checkAgentResponse = new();

                //if (result != null)
                //{
                //    foreach (var item in result)
                //    {
                //        checkAgentResponse.ID_User = item.ID_User;
                //        checkAgentResponse.ServicingAgentInd = item.ServicingAgentInd;
                //        checkAgentResponse.ErrMessage = item.ErrMessage;
                //    }
                //}
                //return checkAgentResponse;
            }
            catch (Exception ex)
            {
                return new List<CheckAgentResponse>()
                {
                };
            }
        }
    }
}
