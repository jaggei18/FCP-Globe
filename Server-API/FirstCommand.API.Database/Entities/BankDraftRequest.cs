using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class BankDraftRequest
    {
        public int Stype { get; set; }

        [MaxLength(10, ErrorMessage = "The PolicyNumber value cannot exceed 10 characters.")]
        public string PolNo { get; set; }

        [MaxLength(1, ErrorMessage = "The Company value cannot exceed 1 characters.")]
        public string Company { get; set; }
    }
}
