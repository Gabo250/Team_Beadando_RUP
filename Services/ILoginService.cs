using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// login service interface
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// login
        /// </summary>
        /// <param name="password">jelszo</param>
        /// <param name="userName">felhasznalonev</param>
        /// <returns></returns>
		User Login(string password, string userName);
    }
}
