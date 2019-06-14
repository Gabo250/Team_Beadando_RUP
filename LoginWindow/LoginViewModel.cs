using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Model;

namespace LoginWindow
{
    /// <summary>
    /// loginvm class
    /// </summary>
    public class LoginViewModel : Bindable
    {
        ILoginService loginService;
        private string userName;
        private string password;
        private User user;

        /// <summary>
        /// login cons
        /// </summary>
        public LoginViewModel()
        {
            loginService = new LoginService();            
        }

        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
                OPC();
            }
        }       

        /// <summary>
        /// Gets or set username
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets or sets user
        /// </summary>
        public User User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
                OPC();
            }
        }

        /// <summary>
        /// user atadas
        /// </summary>
        /// <returns>user</returns>
        public User Logining()
        {
            return loginService.Login(password, userName);                   
        }
    }
}
