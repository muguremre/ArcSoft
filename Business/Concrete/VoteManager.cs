using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class VoteManager : IVoteService
    {
        IVoteDal _serviceDal;
        public VoteManager(IVoteDal voteDal)
        {
            _serviceDal = voteDal;
        }
        public void Add(VoteMaterials entity)
        {
          _serviceDal.Add(entity);
        }

        public void Delete(VoteMaterials entity)
        {
            _serviceDal.Delete(entity);
           
        }

        public List<VoteMaterials> GetAll(Expression<Func<VoteMaterials, bool>> filter = null)
        {
            return _serviceDal.GetAll();
            
        }

        //public void OpenEvent(VoteMaterials entity)
        //{
            

        //}

        public void Update(VoteMaterials entity)
        {
           _serviceDal.Update(entity);
        }
    }
}
