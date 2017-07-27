/*
    <author>yudha.hyp@gmail.com</author>
    <summary>
        Copyright (c) yudha, All Right Reserved.
    </summary> 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPlatform.Credential
{
    public class User
    {    
        public string UserName { get; set; }
        public string Name { get; set; }
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
