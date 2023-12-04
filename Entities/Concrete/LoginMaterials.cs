using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class LoginMaterials : ILoginMaterials
    {
        [Key] // Bu alan birincil anahtar
        public int LoginMaterialId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

    }
}
