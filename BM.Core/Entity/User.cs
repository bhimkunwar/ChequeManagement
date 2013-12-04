using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BM.Core.Entity
{
    public class User
    {
        public virtual int id { get; set; }
        [Required]
        [Display(Name = "Login Name")]
        public virtual string loginName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public virtual string loginPassword { get; set; }
        [Required]
        [Display(Name = "Is Active ?")]
        public virtual bool IsActive { get; set; }
        [Required]
        [Display(Name = "Role")]
        public virtual string Role { get; set; }
    }
}
