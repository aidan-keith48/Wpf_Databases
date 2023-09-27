using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Databases.Classes
{
    public class Reviews
    {
        private int id;
        private string name;
        private string email;
        private string product;
        private string comment;

        public Reviews()
        {

        }

        public Reviews(string name, string email, string product, string comment)
        {
            this.name = name;
            this.email = email;
            this.product = product;
            this.comment = comment;
        }

        public Reviews(int id, string name, string email, string comment, string product)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Comment = comment;
            this.Product = product;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Comment { get => comment; set => comment = value; }
        public string Product { get => product; set => product = value; }
    }
}
