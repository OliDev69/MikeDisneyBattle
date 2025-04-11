using System.ComponentModel.DataAnnotations;

namespace DisneyBattle.Front.Models
{
    public class RegisterModel
    {
        [Required]
        [MinLength(4)]
        public string Pseudo { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [Compare(nameof(MotDePasseConfirmation))]
        public string MotDePasse { get; set; } = null!;
        [Required]
        [Compare(nameof(MotDePasse))]
        public string MotDePasseConfirmation { get; set; } = null!;
    }
}
