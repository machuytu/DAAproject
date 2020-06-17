using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectDAA1.Models
{
    public class LoginModel
    {
        [Key]
        public int idtk { set; get; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Bạn phải nhập tài khoản")]
        public string matk { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [Display(Name = "Password")]
        public string Password { set; get; }

    }
}