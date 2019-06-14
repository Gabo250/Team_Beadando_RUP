using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// uzenet osztaly
    /// </summary>
    public class Uzenet : Bindable
    {
        private string cimzett;
        private string felado;        
        private DateTime kuldesDatuma;
        private string szovegTorzs;
        private string targy;

        /// <summary>
        /// cons of uzi
        /// </summary>
        /// <param name="felado">felado atadasa</param>
        /// <param name="cimzett">cimzett atadasa</param>
        /// <param name="id">id atadasa</param>
        /// <param name="szovegtorzs">sz.torzs atadasa</param>
        /// <param name="targy">targy atadasa</param>
        public Uzenet(string felado, string cimzett, string szovegtorzs, string targy, DateTime date)
        {
            this.felado = felado;
            this.cimzett = cimzett;    
            this.szovegTorzs = szovegtorzs;
            this.targy = targy;
            this.kuldesDatuma = date;
        }

        /// <summary>
        /// Gets cimzett
        /// </summary>
        public string Cimzett
        {
            get
            {
                return cimzett;
            }
        }

        /// <summary>
        /// Gets felado
        /// </summary>
        public string Felado
        {
            get
            {
                return felado;
            }            
        }       

        /// <summary>
        /// Gets or sets kuldesidatum
        /// </summary>
        public DateTime KuldesDatuma
        {
            get
            {
                return kuldesDatuma;
            }

            set
            {
                kuldesDatuma = value;
            }
        }

        /// <summary>
        /// Gets or sets szovegtorzs
        /// </summary>
        public string SzovegTorzs
        {
            get
            {
                return szovegTorzs;
            }

            set
            {
                szovegTorzs = value;
            }
        }

        /// <summary>
        /// Gets or sets Targy
        /// </summary>
        public string Targy
        {
            get
            {
                return targy;
            }

            set
            {
                targy = value;
                OPC();
            }
        }
    }
}
