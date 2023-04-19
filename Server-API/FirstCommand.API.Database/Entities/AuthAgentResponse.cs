using System.ComponentModel.DataAnnotations;

namespace FirstCommand.API.Database.Entities
{
    public class AuthAgentResponse
    {
        [Key]
        public int ID_User { get; set; }
        public string? LoginType { get; set; }
        public string? ErrMessage { get; set; }
    }
}
