using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Alkatresz class
    /// </summary>
    public class Alkatresz : Bindable
    {       
        private string megnevezes;
        private int mennyi;       

        /// <summary>
        /// Cons of alkatresz
        /// </summary>        
        /// <param name="megnevezes">nev atadasa</param>
        public Alkatresz(string megnevezes)
        {            
            this.megnevezes = megnevezes;
        }       

        /// <summary>
        /// Gets bicikli alkatresz neve
        /// </summary>
        public string Megnevezes
        {
            get
            {
                return megnevezes;
            }            
        }

        /// <summary>
        /// Gets or sets menyi kell ebbol
        /// </summary>
        public int Mennyi
        {
            get
            {
                return mennyi;
            }

            set
            {
                mennyi = value;
                OPC();
            }
        }

        /// <summary>
        /// nev kiiratasa
        /// </summary>
        /// <returns>nev</returns>
        public override string ToString()
        {
            return megnevezes;
        }

    }
}
