using System.ComponentModel.DataAnnotations;

namespace FirstCommand.API.Database.Entities
{
    public class InqVerifyResponse
    {
        [Key]
        public string? PolicyNumber { get; set; }
        public string? Company { get; set; }
        public string? Status { get; set; }
        public string? SSNO { get; set; }        
        public string? Writing_Agent { get; set; }
        public string? ServicingAgentInd { get; set; }
    }
}
