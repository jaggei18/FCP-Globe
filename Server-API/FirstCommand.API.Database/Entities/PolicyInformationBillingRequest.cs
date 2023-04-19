using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class PolicyInformationBillingRequest
    {
        public int Stype { get; set; }

        [MaxLength(10, ErrorMessage = "The PolicyNumber value cannot exceed 10 characters.")]
        public string? PolNo { get; set; }
    }
}
