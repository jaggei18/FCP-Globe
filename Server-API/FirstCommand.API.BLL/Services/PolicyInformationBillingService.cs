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
    public class PolicyInformationBillingService
    {
        private readonly FirstCommandDBContext _firstCommandDBContext;

        public PolicyInformationBillingService()
        {
            _firstCommandDBContext = new FirstCommandDBContext();
        }

        public async Task<IEnumerable<PolicyInformationBillingResponse>> GetPolicyInformationBillingService(int stype, string policynum)
        {
            try
            {
                var param = new SqlParameter[]
                           {
                    // Create parameter(s)    
                    new SqlParameter() { ParameterName = "@Stype", Value = stype },
                    new SqlParameter() { ParameterName = "@PolNo", Value = policynum }
                           };
                var result = await _firstCommandDBContext.PolicyInformationBillingResponse.FromSqlRaw("EXECUTE Select_PolicyInf_AWS @Stype, @PolNo", param).ToListAsync();
                return result != null ? result : new List<PolicyInformationBillingResponse>();
                //PolicyInformationBillingResponse res = new();

                //if (result != null)
                //{
                //    foreach (var item in result)
                //    {
                //        res.PolicyNumber = item.PolicyNumber;
                //        res.Policy_Prefix = item.Policy_Prefix;
                //        res.Policy_Suffix = item.Policy_Suffix;
                //        res.Company = item.Company;
                //        res.Status = item.Status;
                //        res.Insured = item.Insured;
                //        res.Owner = item.Owner;
                //        res.Address_1 = item.Address_1;
                //        res.Address_2 = item.Address_2;
                //        res.Address_3 = item.Address_3;
                //        res.Address_4 = item.Address_4;
                //        res.Bill_Type = item.Bill_Type;
                //        res.Frequency = item.Frequency;
                //        res.Issue_Date = item.Issue_Date;
                //        res.PaidTo_Date = item.PaidTo_Date;
                //        res.DOB = item.DOB;
                //        res.Cash_Value = item.Cash_Value;
                //        res.Loan_Value = item.Loan_Value;
                //        res.Loan_PayOff = item.Loan_PayOff;
                //        res.Modal_Premium = item.Modal_Premium;
                //        res.Last_Payment_Amount = item.Last_Payment_Amount;
                //        res.Last_Payment_Date = item.Last_Payment_Date;
                //        res.SSNO = item.SSNO;
                //        res.Allotment_Amount = item.Allotment_Amount;
                //        res.Allotment_Cycle_Date = item.Allotment_Cycle_Date;
                //        res.Allotment_Payor = item.Allotment_Payor;
                //        res.Bank_Draft_Billing_PolicyNumber = item.Bank_Draft_Billing_PolicyNumber;
                //        res.Draft_Day = item.Draft_Day;
                //        res.Draft_Payor = item.Draft_Payor;
                //        res.Draft_Bank = item.Draft_Bank;
                //        res.OPAI_Letter_date = item.OPAI_Letter_date;
                //        res.Writing_Agent = item.Writing_Agent;
                //        res.Issue_Age = item.Issue_Age;
                //        res.Next_OPAI_Date = item.Next_OPAI_Date;
                //        res.Cycle_Date = item.Cycle_Date;
                //        res.StatusDesc = item.StatusDesc;
                //        res.CompanyName = item.CompanyName;
                //        res.BillTypeDescription = item.BillTypeDescription;
                //        res.FrequencyDescription = item.FrequencyDescription;
                //    }
                //}
                //return res;
            }
            catch (Exception ex)
            {
                return new List<PolicyInformationBillingResponse>()
                {
                };
            }
        }

        public async Task<IEnumerable<PolicyInformationBilling_BeneficiaryResponse>> GetBeneficiaryService(string policynum)
        {
            try
            {
                var param = new SqlParameter[]
                           {
                    // Create parameter(s)
                    new SqlParameter() { ParameterName = "@PolNo", Value = policynum }
                           };
                var result = await _firstCommandDBContext.PolicyInformationBilling_BeneficiaryResponse.FromSqlRaw("EXECUTE Select_Beneficiary_AWS @PolNo", param).ToListAsync();
                return result != null ? result : new List<PolicyInformationBilling_BeneficiaryResponse>();
                //var res = new List<PolicyInformationBilling_BeneficiaryResponse>();

                //if (result != null)
                //{
                //    foreach (var item in result)
                //    {
                //        res.Add(new PolicyInformationBilling_BeneficiaryResponse
                //        {
                //            PolicyNumber = item.PolicyNumber,
                //            Sequence = item.Sequence,
                //            Type = item.Type,
                //            Name = item.Name,
                //            Relation = item.Relation,
                //            BeneDesc = item.BeneDesc,
                //            BeneType = item.BeneType
                //        });
                //    }
                //}
                //return res;
            }
            catch (Exception ex)
            {
                return new List<PolicyInformationBilling_BeneficiaryResponse>
                {
                };
            }
        }
    }
}
