using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// alk.rendelkes class
    /// </summary>
    public class AlkatreszRendeles
    {
        private Alkatresz alkatresz;       
        private int mennyiseg;

        /// <summary>
        /// constructor of alk.rendeles
        /// </summary>
        /// <param name="alk">alkatresz atadasa</param>
        /// <param name="id">id atadasa</param>
        /// <param name="mennyiseg">mennyisegi szam atadasa</param>
        public AlkatreszRendeles(Alkatresz alk, int mennyiseg)
        {
            this.alkatresz = alk;           
            this.mennyiseg = mennyiseg;
        }

        /// <summary>
        /// Gets rendelt alkatresz
        /// </summary>
        public Alkatresz Alkatresz
        {
            get
            {
                return alkatresz;
            }            
        }        

        /// <summary>
        /// Gets rendeles mennyisege
        /// </summary>
        public int Mennyiseg
        {
            get
            {
                return mennyiseg;
            }            
        }
    }
}
