/*
    <author>yudha.hyp@gmail.com</author>
    <summary>
        Copyright (c) yudha, All Right Reserved.
    </summary> 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WebPlatform.Credential
{
    public class User
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3]\.)|(([\w-]+\.)+))([a-zA-Z{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter valid email.")]
        public string Email { get; set; }
        public string Status { get; set; }
        public string IsBackup{ get; set; } 
        public bool IsActive { get; set; }
        public Role Role { get; set; } 

        public User()
        {
            Role = new Role();
        }
    }
}
