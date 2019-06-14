using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Operator osztaly
    /// </summary>
    public class Operator
    {
        private int id;
        private string userName;

        /// <summary>
        /// ez a operator konstruktora
        /// </summary>
        /// <param name="id">id atadasa</param>
        /// <param name="username">nev atadasa</param>
        public Operator(int id, string username)
        {
            this.id = id;
            this.userName = username;
        }

        /// <summary>
        /// Gets operator id-je
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }           
        }

        /// <summary>
        /// Gets opperator neve
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }          
        }
    }
}
