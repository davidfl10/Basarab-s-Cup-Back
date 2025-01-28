using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace finalexam_back.Models
{
    public class Team
    {
        public int Id { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Your Team Name must have at least 2 characters")]
        public string Name { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Player Name must have at least 2 characters")]
        public string Player1 { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Player Name must have at least 2 characters")]
        public string Player2 { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Player Name must have at least 2 characters")]
        public string Player3 { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Player Name must have at least 2 characters")]
        public string Player4 { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Player Name must have at least 2 characters")]
        public string Player5 { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Player Name must have at least 2 characters")]
        public string Player6 { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Player Name must have at least 2 characters")]
        public string Player7 { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Player Name must have at least 2 characters")]
        public string Player8 { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Player Name must have at least 2 characters")]
        public string Player9 { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Player Name must have at least 2 characters")]
        public string Player10 { get; set; }
        public string? Player11 { get; set; }
        public string? Player12 { get; set; }
        public string? Player13 { get; set; }
        public string? Player14 { get; set; }
        public int Played { get; set; } = 0;
        public int GD { get; set; } = 0;
        public int Points { get; set; } = 0;
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
