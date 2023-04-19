using FirstCommand.API.Database;
using FirstCommand.API.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace FirstCommand.API.BLL.Services
{
    public class CoverageService
    {
        private readonly FirstCommandDBContext _firstCommandDBContext;

        public CoverageService()
        {
            _firstCommandDBContext = new FirstCommandDBContext();
        }

        public async Task<IEnumerable<CoverageResponse>> GetCoverageService(string PolicyNumber)
        {
            try
            {
                SqlParameter pNo = new SqlParameter("@PolNo", PolicyNumber);
                var res = await this._firstCommandDBContext.CoverageResponse.FromSqlRaw("EXECUTE Select_Coverage_AWS @PolNo", pNo).ToListAsync();

                //CoverageResponse coverageResponse = new();

                //if (res != null)
                //{
                //    foreach (var item in res)
                //    {
                //        coverageResponse.CovDesc = item.CovDesc;
                //        coverageResponse.Face_Amount = item.Face_Amount;
                //        coverageResponse.Annual_Premium = item.Annual_Premium;
                //    }
                //}
                //return coverageResponse;
                return res != null ? res : new List<CoverageResponse>();
            }
            catch (Exception ex)
            {
                return new List<CoverageResponse>
                {
                };
            }
        }
    }
}
