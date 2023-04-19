using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class CheckAgentRequest
    {
        //public Guid SessionGUID { get; set; }
        public string? PolicyNumber { get; set; }
        public string? AgentNumber { get; set; }
        public string? ServicingAgentInd { get; set; }
    }
}
