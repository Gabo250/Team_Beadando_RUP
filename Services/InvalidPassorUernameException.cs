using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class InvalidPassorUernameException : Exception
    {
        public InvalidPassorUernameException() : base("Rosz felhasználó név vagy jelszó!")
        {

        }
    }
}
