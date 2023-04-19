using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class GovAllotmentResponse
    {
        public class GovAllotmentStype3Response
        {
            [Key]
            public string? PolicyNumber { get; set; }
            public string? Company { get; set; }
            public string? CompanyName { get; set; }
            public string? SSNO { get; set; }
            public string? Writing_Agent { get; set; }
            public string? Status { get; set; }
            public string? StatusDesc { get; set; }
        }

        public class GovAllotmentStype4Response
        {
            [Key]
            public string? PolicyNumber { get; set; }
            public string? Bill_Type { get; set; }
            public string? BillTypeDescription { get; set; }
            public string? Company { get; set; }
            public string? CompanyName { get; set; }
            public string? SSNO { get; set; }
            public string? Allotment_Amount { get; set; }
            public DateTime Allotment_Cycle_Date { get; set; }
            public string? Allotment_Payor { get; set; }
            public string? Status { get; set; }
            public string? StatusDesc { get; set; }
        }

        public class GovAllotmentStype5Response
        {
            [Key]
            public string? PolicyNumber { get; set; }
            public string? Bill_Type { get; set; }
            public string? BillTypeDescription { get; set; }
            public string? Company { get; set; }
            public string? CompanyName { get; set; }
            public string? SSNO { get; set; }            
            public string? Status { get; set; }
            public string? StatusDesc { get; set; }
            public string? Writing_Agent { get; set; }
            public DateTime? PaidTo_Date { get; set; }
            public decimal? Modal_Premium { get; set; }
        }

        public class GovAllotmentStype6Response
        {
            [Key]
            public string? PolicyNumber { get; set; }
            public string? SSNO { get; set; }
        }
    }
}
