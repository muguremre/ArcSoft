using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
    public interface IVoteMaterials
    {
       
        public Guid VoteMaterialId { get; set; }
        public int VoteCount { get; set; }
    }
}
