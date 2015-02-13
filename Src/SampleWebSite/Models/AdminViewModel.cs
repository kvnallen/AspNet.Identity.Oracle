using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IdentitySample.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        
        [Display(Name = "Regra")]
        [Required(ErrorMessage = "{0} é obrigatório", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Description { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "{0} deve ser um e-mail")]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}