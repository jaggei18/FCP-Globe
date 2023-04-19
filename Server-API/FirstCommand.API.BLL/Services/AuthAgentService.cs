using FirstCommand.API.Database;
using FirstCommand.API.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace FirstCommand.API.BLL.Services
{
    public class AuthAgentService
    {
        private readonly FirstCommandDBContext _firstCommandDBContext;

        public AuthAgentService()
        {
            _firstCommandDBContext = new FirstCommandDBContext();
        }
        public async Task<AuthAgentResponse> GetAuthAgentInfo(string agentID, bool bAllAccessInd)
        {            
            try
            {
                var param = new SqlParameter[]
                {
                    // Create parameter(s)
                    new SqlParameter() { ParameterName = "@AgentNumber", Value = agentID},
                    new SqlParameter() { ParameterName = "@AllAccessInd",Value = bAllAccessInd},
                    new SqlParameter() { ParameterName = "@ID_User", Value = SqlInt32.Null},
                    new SqlParameter() { ParameterName = "@LoginType",Value = SqlString.Null},
                    new SqlParameter() { ParameterName = "@ErrMessage", Value = SqlString.Null}
                 };

                var result = await _firstCommandDBContext.AuthAgentResponse.FromSqlRaw("BypassedLoginValidation_AWS @AgentNumber,@AllAccessInd,@ID_User Output, @LoginType Output, @ErrMessage Output Select @ID_User AS ID_User,@LoginType AS LoginType,@ErrMessage AS ErrMessage", param).ToListAsync();

                AuthAgentResponse? authAgentResponse = new();
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        authAgentResponse.ID_User = item.ID_User;
                        authAgentResponse.LoginType = item.LoginType;
                        authAgentResponse.ErrMessage = item.ErrMessage;
                    }
                }
                return authAgentResponse;
            }
            catch (Exception ex)
            {
                return new AuthAgentResponse()
                {
                    ErrMessage= ex.Message
                };
            }
        }
    }
}
