using FirstCommand.API.Database;
using FirstCommand.API.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FirstCommand.API.BLL.Services
{
    public class AgentInfoService
    {
        private readonly FirstCommandDBContext _firstCommandDBContext;

        public AgentInfoService()
        {
            _firstCommandDBContext = new FirstCommandDBContext();
        }

        public async Task<IEnumerable<AgentInfoResponse>> GetAgentService(int AgentNumber)
        {
            try
            {
                SqlParameter pPendAgentNo = new SqlParameter("@PendAgentNo", AgentNumber);

                var agentData = await this._firstCommandDBContext.AgentInfoResponse.FromSqlRaw("EXECUTE Select_PendingRec_AWS @PendAgentNo", pPendAgentNo).ToListAsync();

                //AgentInfoResponse agentInfoResponse = new();
                //if (agentData != null)
                //{
                //    foreach (var item in agentData)
                //    {
                //        agentInfoResponse.PolicyNumber = item.PolicyNumber;
                //        agentInfoResponse.Insured = item.Insured;
                //        agentInfoResponse.Last_Payment_Date = item.Last_Payment_Date;
                //    }
                //}
                return agentData != null ? agentData : new List<AgentInfoResponse>();
            }
            catch (Exception ex)
            {
                return new List<AgentInfoResponse>
                {
                };
            }

        }
    }
}
