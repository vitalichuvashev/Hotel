using System.ComponentModel.DataAnnotations;

namespace Hotell.Models
{
    public class Registration
    {

    

        [Required(ErrorMessage = "Firstname is required field")]
        public string Firstname {  get; set; }= string.Empty;

        [Required(ErrorMessage = "Lastname is required field")]
        public string Lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Personal code is required field")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Personal code must be 11 numbers")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers allowed")]
        public string PersonalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required field")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Wrong email format")]
        public string Email { get; set; } = string.Empty;


    }
}
