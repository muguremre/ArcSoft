using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class VoteMaterials : IVoteMaterials
    {
        [Key]
        public Guid VoteMaterialId { get; set; }

        public DateTime VoteTime{ get; set; }
        public int VoteCount { get; set; }
        public String? VoteName  { get; set; }

    }
}
