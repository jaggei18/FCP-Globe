using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class InqVerifyRequest
    {
        //public Guid SessionGUID { get; set; }
        public int ID_User { get; set; }
        public string? InquiryType { get; set; }
        public string? PolicyNumber { get; set; }
        public string? Policy_SSNO { get; set; }
    }
}
