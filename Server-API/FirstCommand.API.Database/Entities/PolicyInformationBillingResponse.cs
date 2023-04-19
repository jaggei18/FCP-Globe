using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class PolicyInformationBillingResponse
    {
        [Key]
        public string? PolicyNumber { get; set; }

        public string? Policy_Prefix { get; set; }

        public string? Policy_Suffix { get; set; }

        public string? Company { get; set; }

        public string? Status { get; set; }

        public string? Insured { get; set; }

        public string? Owner { get; set; }

        public string? Address_1 { get; set; }

        public string? Address_2 { get; set; }

        public string? Address_3 { get; set; }

        public string? Address_4 { get; set; }

        public string? Bill_Type { get; set; }

        public string? Frequency { get; set; }

        public DateTime? Issue_Date { get; set; }

        public DateTime? PaidTo_Date { get; set; }

        public DateTime? DOB { get; set; }

        public decimal? Cash_Value { get; set; }

        public decimal? Loan_Value { get; set; }

        public decimal? Loan_PayOff { get; set; }

        public decimal? Modal_Premium { get; set; }

        public decimal? Last_Payment_Amount { get; set; }

        public DateTime? Last_Payment_Date { get; set; }

        public string? SSNO { get; set; }

        public string? Allotment_Amount { get; set; }

        public DateTime? Allotment_Cycle_Date { get; set; }

        public string? Allotment_Payor { get; set; }

        public string? Bank_Draft_Billing_PolicyNumber { get; set; }

        public string? Draft_Day { get; set; }

        public string? Draft_Payor { get; set; }

        public string? Draft_Bank { get; set; }

        public DateTime? OPAI_Letter_date { get; set; }

        public string? Writing_Agent { get; set; }

        public short? Issue_Age { get; set; }

        public DateTime? Next_OPAI_Date { get; set; }

        public DateTime? Cycle_Date { get; set; }

        public string? StatusDesc { get; set; }
        public string? CompanyName { get; set; }
        public string? BillTypeDescription { get; set; }
        public string? FrequencyDescription { get; set; }
    }
}
