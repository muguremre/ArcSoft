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
    public class LoginManager : ILoginService
    {
        ILoginDal _loginDal;
        public LoginManager(ILoginDal loginDal )
        { 
            _loginDal = loginDal;
        }
        public void Add(LoginMaterials entity)
        {
            _loginDal.Add( entity );
        }

        public void Delete(LoginMaterials entity)
        {
            _loginDal.Delete( entity );
        }

        public List<LoginMaterials> GetAll(Expression<Func<LoginMaterials, bool>> filter = null)
        {
           return _loginDal.GetAll();
        }

        public List<LoginMaterials> GetLoginDetails()
        {
            return _loginDal.GetLoginDetails();
        }

        public bool CheckCredentials(string email, string password)
        {

            // Örnek olarak, kullanıcıyı veritabanında e-posta adresine göre bulunur
            var users = _loginDal.GetAll(u => u.Email == email);

            // Kullanıcı bulunamazsa veya şifre eşleşmezse false döndür
            if (users == null || !users.Any(user => user.Password == password))
            {
                return false;
            }

            // Eğer buraya kadar geldiyse, e-posta ve şifre doğrudur
            return true;
        }
        public void Update(LoginMaterials entity)
        {
           _loginDal.Update( entity );
        }
    }
}
