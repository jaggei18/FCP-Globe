using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class PendVerifyRequest
    {
        //public Guid SessionGUID { get; set; }
        public string? AgentNumber { get; set; }
        public int? UserId { get; set; }
    }
}
