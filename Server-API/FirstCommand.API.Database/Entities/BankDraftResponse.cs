using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class BankDraftStype1Response
    {
        [Key]
        public string? PolicyNumber { get; set; }
        public string? Company { get; set; }
        public string? CompanyName { get; set; }
    }

    public class BankDraftStype2Response
    {
        [Key]
        public string? PolicyNumber { get; set; }
        public string? Company { get; set; }
        public string? CompanyName { get; set; }
        public string? Status { get; set; }
        public string? Bill_Type { get; set; }
        public string? BillTypeDescription { get; set; }
        public string? Frequency { get; set; }
        public string? FrequencyDescription { get; set; }
        public string? Bank_Draft_Billing_PolicyNumber { get; set; }
        public string? Draft_Day { get; set; }
        public string? Draft_Payor { get; set; }
        public string? Draft_Bank { get; set; }
        public string? Writing_Agent { get; set; }
    }

    public class BankDraftStype3Response
    {
        [Key]
        public string? PolicyNumber { get; set; }
        public string? Status { get; set; }
        public string? Company { get; set; }
        public string? CompanyName { get; set; }        
        public string? Bill_Type { get; set; }
        public string? BillTypeDescription { get; set; }
        public string? Writing_Agent { get; set; }
        public DateTime? PaidTo_Date { get; set; }
        public decimal? Modal_Premium { get; set; }
    }
}
