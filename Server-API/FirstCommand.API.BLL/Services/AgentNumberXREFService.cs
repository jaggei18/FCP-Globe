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
    public class AgentNumberXREFService
    {
        private readonly FirstCommandDBContext _firstCommandDBContext;

        public AgentNumberXREFService()
        {
            _firstCommandDBContext = new FirstCommandDBContext();
        }

        public async Task<IEnumerable<AgentNumberXREFResponse>> GetAgentNumberXREFService(string agentNumber)
        {
            try
            {
                var param = new SqlParameter[]
                            {
                    // Create parameter(s)
                    new SqlParameter() { ParameterName = "@AgentNumber", Value = agentNumber },
                    new SqlParameter() { ParameterName = "@ErrMessage", Value = "" }
                            };

                var result = await _firstCommandDBContext.AgentNumberXREFResponse.FromSqlRaw("EXECUTE [dbo].[XREFAgentNumber_AWS] @AgentNumber Output, @ErrMessage Output Select @AgentNumber AS AgentNumber, @ErrMessage AS ErrMessage", param).ToListAsync();
                return result != null ? result : new List<AgentNumberXREFResponse>();
                //AgentNumberXREFResponse agentNumberXREFResponse = new();

                //if (result != null)
                //{
                //    foreach (var item in result)
                //    {
                //        agentNumberXREFResponse.AgentNumber = item.AgentNumber;
                //        agentNumberXREFResponse.ErrMessage = item.ErrMessage;
                //    }
                //}
                //return agentNumberXREFResponse;
            }
            catch (Exception ex)
            {
                return new List<AgentNumberXREFResponse>()
                {
                };
            }
        }
    }
}
