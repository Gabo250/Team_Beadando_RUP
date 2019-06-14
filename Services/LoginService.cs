using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// loginserv class
    /// </summary>
    public class LoginService : ILoginService
    {
        private DataAccessLayer repository;
        private List<User> users;

        public LoginService()
        {
            repository = new DataAccessLayer();
            users = repository.Userek();
        }        

        /// <summary>
        /// log
        /// </summary>
        /// <param name="password">jelszo</param>
        /// <param name="userName">felhasznalonev</param>
        /// <returns></returns>
        public User Login(string password, string userName)
        {
            foreach (var us in users)
            {
                if (us.Password == password && us.Username == userName)
                {                
                    return us;
                }
            }

            throw new InvalidPassorUernameException();
        }
    }
}
