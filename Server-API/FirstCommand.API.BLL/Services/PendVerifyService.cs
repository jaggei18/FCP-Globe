using FirstCommand.API.Database;
using FirstCommand.API.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.BLL.Services
{
    public class PendVerifyService
    {
        private readonly FirstCommandDBContext _firstCommandDBContext;

        public PendVerifyService()
        {
            _firstCommandDBContext = new FirstCommandDBContext();
        }

        public async Task<IEnumerable<PendVerifyResponse>> GetPendVerifyService(string agentNumber, int? userId)
        {
            try
            {
                var param = new SqlParameter[]
                            {
                    // Create parameter(s)
                    new SqlParameter() { ParameterName = "@AgentNumber", Value = agentNumber},
                    new SqlParameter() { ParameterName = "@UserId",Value = userId ?? SqlInt32.Null},
                    new SqlParameter() { ParameterName = "@ErrMessage", Value = "" }
                            };


                var result = await _firstCommandDBContext.PendVerifyResponse.FromSqlRaw("EXECUTE [dbo].[PendingAgtValid_AWS] @AgentNumber,@UserId Output, @ErrMessage Output Select @UserId AS UserId, @ErrMessage AS ErrMessage", param).ToListAsync();
                return result != null ? result : new List<PendVerifyResponse>();
                //PendVerifyResponse pendVerifyResponse = new();

                //if (result != null)
                //{
                //    foreach (var item in result)
                //    {
                //        pendVerifyResponse.UserID = item.UserID;
                //        pendVerifyResponse.ErrMessage = item.ErrMessage;
                //    }
                //}
                //return pendVerifyResponse;
            }
            catch (Exception ex)
            {
                return new List<PendVerifyResponse>()
                {
                };
            }
        }
    }
}
