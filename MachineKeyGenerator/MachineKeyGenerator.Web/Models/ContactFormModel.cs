using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MachineKeyGenerator.Web.Models
{
    public class ContactFormModel
    {
        [Required(AllowEmptyStrings = false), StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false), StringLength(100, MinimumLength = 6)]
        public string Email { get; set; }

        [AllowHtml]
        [Required(AllowEmptyStrings=false), StringLength(300, MinimumLength=25)]
        public string Message { get; set; }
    }
}