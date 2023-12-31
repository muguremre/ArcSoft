﻿using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class LoginMaterials : ILoginMaterials
    {
        [Key]
        public Guid LoginMaterialId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
        public bool isAdmin { get; set; }

        [ForeignKey("VoteMaterials")]
        public Guid VoteMaterialId { get; set; }

    }
}
