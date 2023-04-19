using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class AuthAgentRequest
    {        
        public string? agentID { get; set; }
        public string? role { get; set; }        
    }
}
