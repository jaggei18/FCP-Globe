using FirstCommand.API.Database;
using FirstCommand.API.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FirstCommand.API.Database.Entities.GovAllotmentResponse;

namespace FirstCommand.API.BLL.Services
{
    public class GovAllotmentService
    {
        private readonly FirstCommandDBContext _firstCommandDBContext;

        public GovAllotmentService()
        {
            _firstCommandDBContext = new FirstCommandDBContext();
        }
        private string procedure = "EXECUTE Select_GovAllotment_AWS @Stype, @PolNo, @SSN, @Company";

        public async Task<IEnumerable<GovAllotmentStype3Response>> GetGovAllotmentSType3Service(int stype, string policynum, string ssn, string company)
        {
            try
            {
                SqlParameter[] param = GetSqlParameter(stype, policynum, ssn, company);
                var result = await _firstCommandDBContext.GovAllotmentStype3Response.FromSqlRaw(procedure, param).ToListAsync();
                return result != null ? result : new List<GovAllotmentStype3Response>();                
            }
            catch (Exception ex)
            {
                return new List<GovAllotmentStype3Response>()
                {
                };
            }
        }

        public async Task<IEnumerable<GovAllotmentStype4Response>> GetGovAllotmentSType4Service(int stype, string policynum, string ssn, string company)
        {
            try
            {
                SqlParameter[] param = GetSqlParameter(stype, policynum, ssn, company);
                var result = await _firstCommandDBContext.GovAllotmentStype4Response.FromSqlRaw(procedure, param).ToListAsync();
                return result != null ? result : new List<GovAllotmentStype4Response>();
            }
            catch (Exception ex)
            {
                return new List<GovAllotmentStype4Response>()
                {
                };
            }
        }

        public async Task<IEnumerable<GovAllotmentStype5Response>> GetGovAllotmentSType5Service(int stype, string policynum, string ssn, string company)
        {
            try
            {
                SqlParameter[] param = GetSqlParameter(stype, policynum, ssn, company);
                var result = await _firstCommandDBContext.GovAllotmentStype5Response.FromSqlRaw(procedure, param).ToListAsync();
                return result != null ? result : new List<GovAllotmentStype5Response>();
            }
            catch (Exception ex)
            {
                return new List<GovAllotmentStype5Response>()
                {
                };
            }
        }

        public async Task<IEnumerable<GovAllotmentStype6Response>> GetGovAllotmentSType6Service(int stype, string policynum, string ssn, string company)
        {
            try
            {
                SqlParameter[] param = GetSqlParameter(stype, policynum, ssn, company);
                var result = await _firstCommandDBContext.GovAllotmentStype6Response.FromSqlRaw(procedure, param).ToListAsync();
                return result != null ? result : new List<GovAllotmentStype6Response>();
            }
            catch (Exception ex)
            {
                return new List<GovAllotmentStype6Response>()
                {
                };
            }
        }
        private static SqlParameter[] GetSqlParameter(int stype, string policynum, string ssn, string company)
        {
            var param = new SqlParameter[]
                            {
                    // Create parameter(s)    
                    new SqlParameter() { ParameterName = "@Stype", Value = stype },
                    new SqlParameter() { ParameterName = "@PolNo", Value = policynum },
                    new SqlParameter() { ParameterName = "@SSN", Value = ssn },
                    new SqlParameter() { ParameterName = "@Company", Value = company }
                            };
            return param;
        }

    }
}
