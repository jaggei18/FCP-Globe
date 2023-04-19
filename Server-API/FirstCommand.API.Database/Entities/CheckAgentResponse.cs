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
    public class CheckAgentResponse
    {        
        public string? ServicingAgentInd { get; set; }
        public string? ErrMessage { get; set; }
    }
}
