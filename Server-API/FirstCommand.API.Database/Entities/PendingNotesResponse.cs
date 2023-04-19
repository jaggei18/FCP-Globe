using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstCommand.API.Database.Entities
{
    public class PendingNotesResponse
    {
        [Key]
        public string? PolicyNumber { get; set; } //Description
        public string? Type { get; set; }
        public string? NoteMessage { get; set; } //Description
        public string? NoteNo { get; set; }
        public string? NoteSeq { get; set; }
        public string? NoteSatisfied { get; set; } //Satisfied
        public DateTime? DateRequest { get; set; } //Date Requested
    }
}
