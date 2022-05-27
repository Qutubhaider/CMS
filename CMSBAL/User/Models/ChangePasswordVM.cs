using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.User.Models
{
    public  class ChangePasswordVM
    {
        [Required(ErrorMessage="Old Password is required.")]
        public string stOldPassword { get; set; }
        [StringLength(12, ErrorMessage = "Must be between 5 and 12 characters", MinimumLength = 5)]
        [Required(ErrorMessage = "New Password is required.")]
        public string stNewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required.")]
        [StringLength(12, ErrorMessage = "Must be between 5 and 12 characters", MinimumLength = 5)]
        [Compare("stNewPassword",ErrorMessage ="New Password and Confirm Password not matched.")]
        public string stNewPasswordConfirm { get; set; }
    }
}
