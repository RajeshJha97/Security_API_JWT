using System.ComponentModel.DataAnnotations;

namespace Security_Dem2.Models
{
    public class User
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
