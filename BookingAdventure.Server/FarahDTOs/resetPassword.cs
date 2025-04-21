using System.ComponentModel.DataAnnotations;

namespace BookingAdventure.Server.DTOs
{
    public class resetPassword
    {
        public int UserId { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "New password and confirmation do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
