using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    [Keyless]
    public class PolicyInformationBilling_BeneficiaryResponse
    {        
        public string? PolicyNumber { get; set; }
        //[Key]
        public short Sequence { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Relation { get; set; }
        public string? BeneDesc { get; set; }
        public string? BeneType { get; set; }
    }
}
