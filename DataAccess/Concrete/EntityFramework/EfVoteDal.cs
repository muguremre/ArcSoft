using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfVoteDal : EfEntityRepositoryBase<VoteMaterials,ArcSoftLoginContext> , IVoteDal
    {
        public List<VoteMaterials> GetMaterialDetails()
        {
            throw new NotImplementedException();
        }
    }
}
