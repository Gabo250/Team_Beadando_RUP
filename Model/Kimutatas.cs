using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// kimutatas osztaly
    /// </summary>
    public class Kimutatas
    {
        private int id;
        private int keszbiciklik;
        private string nev;

        /// <summary>
        /// constr of kimutatas
        /// </summary>
        /// <param name="nev">operator neve</param>
        /// <param name="id">id atadasa</param>
        /// <param name="keszbiciklik">kesz biciklinek a szamanak az atadasa</param>
        public Kimutatas(string nev, int id, int keszbiciklik)
        {
            this.nev = nev;
            this.id = id;
            this.keszbiciklik = keszbiciklik;
        }

        /// <summary>
        /// Gets id
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// Gets keszbiciklik szama
        /// </summary>
        public int Keszbiciklik
        {
            get
            {
                return keszbiciklik;
            }          
        }

        /// <summary>
        /// Gets nev
        /// </summary>
        public string Nev
        {
            get
            {
                return nev;
            }
        }
    }
}
