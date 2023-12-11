using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ILoginService : IEntityRepositoryBase<LoginMaterials>
    {
       bool CheckCredentials(string email, string password);
    }
}
