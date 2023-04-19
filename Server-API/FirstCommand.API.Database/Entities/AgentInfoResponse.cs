using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class AgentInfoResponse
    {
        [Key]
        public string? PolicyNumber { get; set; }
        public string? Insured { get; set; }
        public DateTime? Last_Payment_Date { get; set; }
        public string? Allotment_Payor { get; set; }
        //public string? Writing_Agent { get; set; }

    }
}
