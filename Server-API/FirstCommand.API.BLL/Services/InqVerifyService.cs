using FirstCommand.API.Database;
using FirstCommand.API.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FirstCommand.API.BLL.Services
{
    public class InqVerifyService
    {
        private readonly FirstCommandDBContext _firstCommandDBContext;

        public InqVerifyService()
        {
            _firstCommandDBContext = new FirstCommandDBContext();
        }

        /// <summary>
        /// Get InqVerifyService
        /// </summary>
        /// <param name="sessionGUID"></param>
        /// <param name="inquiryType"></param>
        /// <param name="policy_Suffix"></param>
        /// <param name="policy_SSNO"></param>
        /// <returns></returns>
        public async Task<IEnumerable<InqVerifyResponse>> GetInqVerifyService(int userId, string policyNumber)
        {
            try
            {
                var param = new SqlParameter[]
                            {
                    // Create parameter(s)    
                    new SqlParameter() { ParameterName = "@ID_User", Value = userId },
                    //new SqlParameter() { ParameterName = "@InquiryType", Value = inquiryType },
                    new SqlParameter() { ParameterName = "@PolicyNumber", Value = policyNumber },
                    //new SqlParameter() { ParameterName = "@Policy_SSNO", Value = policy_SSNO }
                            };
                var result = await _firstCommandDBContext.InqVerifyResponse.FromSqlRaw("EXECUTE Select_PolicyList_AWS @ID_User, @PolicyNumber", param).ToListAsync();
                return result != null ? result : new List<InqVerifyResponse>();
                //InqVerifyResponse inqVerifyResponse = new();

                //if (result != null)
                //{
                //    foreach (var item in result)
                //    {
                //        inqVerifyResponse.PolicyNumber = item.PolicyNumber;
                //        inqVerifyResponse.Company = item.Company;
                //        inqVerifyResponse.Status = item.Status;
                //        inqVerifyResponse.SSNO = item.SSNO;
                //        inqVerifyResponse.Writing_Agent = item.Writing_Agent;
                //    }
                //}
                //return inqVerifyResponse;
            }
            catch (Exception ex)
            {
                return new List<InqVerifyResponse>()
                {
                };
            }
        }
    }
}
