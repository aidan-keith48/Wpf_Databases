using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Databases.Classes
{
    public class User
    {
        
        private string username;

        public User( string username)
        {
            
            this.Username = username;
        }

        
        public string Username { get => username; set => username = value; }
    }
}
