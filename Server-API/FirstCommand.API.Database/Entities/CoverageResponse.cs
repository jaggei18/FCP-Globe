using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class CoverageResponse
    {
        [Key]
        public string? PolicyNumber { get; set; }
        public string? CovDesc { get; set; }
        public string? Type { get; set; }
        public int? Face_Amount { get; set; }
        public short? Sequence { get; set; }
        public decimal? Annual_Premium { get; set; }
    }
}
