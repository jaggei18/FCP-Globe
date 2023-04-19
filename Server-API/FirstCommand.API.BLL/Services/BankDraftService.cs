using FirstCommand.API.Database;
using FirstCommand.API.Database.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FirstCommand.API.BLL.Services
{
    public class BankDraftService
    {
        private readonly FirstCommandDBContext _firstCommandDBContext;

        public BankDraftService()
        {
            _firstCommandDBContext = new FirstCommandDBContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stype"></param>
        /// <param name="policynum"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BankDraftStype1Response>> GetBankDraftSType1Service(int stype, string policynum, string company)
        {
            try
            {
                SqlParameter[] param = GetBankDraftSqlParameter(stype, policynum, company);
                var result = await _firstCommandDBContext.BankDraftStype1Response.FromSqlRaw("EXECUTE Select_BankDraft_AWS @Stype, @PolNo, @Company", param).ToListAsync();
                return result != null ? result : new List<BankDraftStype1Response>();
                //BankDraftStype1Response bankDraftStype1Response = new();

                //if (result != null)
                //{
                //    foreach (var item in result)
                //    {
                //        bankDraftStype1Response.PolicyNumber = item.PolicyNumber;
                //        bankDraftStype1Response.Company = item.Company;
                //        bankDraftStype1Response.CompanyName = item.CompanyName;
                //    }
                //}
                //return bankDraftStype1Response;
            }
            catch (Exception ex)
            {
                return new List<BankDraftStype1Response>()
                {
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stype"></param>
        /// <param name="policynum"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BankDraftStype2Response>> GetBankDraftSType2Service(int stype, string policynum, string company)
        {
            try
            {
                SqlParameter[] param = GetBankDraftSqlParameter(stype, policynum, company);
                var result = await _firstCommandDBContext.BankDraftStype2Response.FromSqlRaw("EXECUTE Select_BankDraft_AWS @Stype, @PolNo, @Company", param).ToListAsync();
                return result != null ? result : new List<BankDraftStype2Response>();
                //BankDraftStype2Response bankDraftStype2Response = new();

                //if (result != null)
                //{
                //    foreach (var item in result)
                //    {
                //        bankDraftStype2Response.PolicyNumber = item.PolicyNumber;
                //        bankDraftStype2Response.Company = item.Company;
                //        bankDraftStype2Response.CompanyName = item.CompanyName;
                //        bankDraftStype2Response.Status = item.Status;
                //        bankDraftStype2Response.Bill_Type = item.Bill_Type;
                //        bankDraftStype2Response.BillTypeDescription = item.BillTypeDescription;
                //        bankDraftStype2Response.Frequency = item.Frequency;
                //        bankDraftStype2Response.FrequencyDescription = item.FrequencyDescription;
                //        bankDraftStype2Response.Bank_Draft_Billing_PolicyNumber = item.Bank_Draft_Billing_PolicyNumber;
                //        bankDraftStype2Response.Draft_Day = item.Draft_Day;
                //        bankDraftStype2Response.Draft_Payor = item.Draft_Payor;
                //        bankDraftStype2Response.Draft_Bank = item.Draft_Bank;
                //        bankDraftStype2Response.Writing_Agent = item.Writing_Agent;
                //    }
                //}
                //return bankDraftStype2Response;
            }
            catch (Exception ex)
            {
                return new List<BankDraftStype2Response>()
                {
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stype"></param>
        /// <param name="policynum"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BankDraftStype3Response>> GetBankDraftSType3Service(int stype, string policynum, string company)
        {
            try
            {
                SqlParameter[] param = GetBankDraftSqlParameter(stype, policynum, company);

                var result = await _firstCommandDBContext.BankDraftStype3Response.FromSqlRaw("EXECUTE Select_BankDraft_AWS @Stype, @PolNo, @Company", param).ToListAsync();
                return result != null ? result : new List<BankDraftStype3Response>();

                //var bankDraftStype3Response = new List<BankDraftStype3Response>();
                //if (result != null)
                //{
                //    foreach (var item in result)
                //    {
                //        bankDraftStype3Response.Add(new BankDraftStype3Response
                //        {
                //            PolicyNumber = item.PolicyNumber,
                //            Company = item.Company,
                //            CompanyName = item.CompanyName,
                //            Status = item.Status,
                //            Bill_Type = item.Bill_Type,
                //            BillTypeDescription = item.BillTypeDescription,
                //            Writing_Agent = item.Writing_Agent,
                //            PaidTo_Date = item.PaidTo_Date,
                //            Modal_Premium = item.Modal_Premium
                //        });
                //    }
                //}
                //return bankDraftStype3Response;
            }
            catch (Exception ex)
            {
                return new List<BankDraftStype3Response>
                {
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stype"></param>
        /// <param name="policynum"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        private static SqlParameter[] GetBankDraftSqlParameter(int stype, string policynum, string company)
        {
            var param = new SqlParameter[]
                            {
                    // Create parameter(s)    
                    new SqlParameter() { ParameterName = "@Stype", Value = stype },
                    new SqlParameter() { ParameterName = "@PolNo", Value = policynum },
                    new SqlParameter() { ParameterName = "@Company", Value = company }
                            };
            return param;
        }
    }
}
