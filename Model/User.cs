using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// szerepkor felsorolas
    /// </summary>
    public enum Role
    {
        Ugyvezeto = 1, Logisztikus = 2, Operator = 3
    }

    /// <summary>
    /// user class
    /// </summary>
    public class User
    {        
        private int id;
        private string password;
        private Role privilege;
        private string userName;
        private string nev;

        /// <summary>
        /// cons of user
        /// </summary>
        /// <param name="id">ID atadasa</param>
        /// <param name="pass">pass atadasa</param>
        /// <param name="priv">role atadasa</param>
        /// <param name="username">usernev atadasa</param>
        /// <param name="nev">nev atadasa</param>
        public User(int id, string pass, Role priv, string username, string nev)
        {
            this.id = id;
            this.password = pass;
            this.privilege = priv;
            this.userName = username;
            this.nev = nev;
        }

        /// <summary>
        /// Gets ID
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// Gets pass
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }            
        }

        /// <summary>
        /// Gets or sets role
        /// </summary>
        public Role Privilege
        {
            get
            {
                return privilege;
            }  
            
            set
            {
                privilege = value;
            }          
        }

        /// <summary>
        /// Gets username
        /// </summary>
        public string Username
        {
            get { return userName; }
        }

        /// <summary>
        /// Gets nev
        /// </summary>
        public string Nev
        {
            get { return nev; }          
        }

        public override string ToString()
        {
            return nev + "(" + privilege.ToString() + ")";
        }
    }
}
