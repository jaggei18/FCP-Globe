using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class PendVerifyResponse
    {
        [Key]
        public int UserID { get; set; }
        public string? ErrMessage { get; set; }
    }
}
