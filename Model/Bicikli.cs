using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Bicili tipusok felsorolasa
    /// </summary>
    public enum BicikliTipus
    {
        Orszaguti = 1, Terep = 2, Verseny = 3
    }

    /// <summary>
    /// Bicikli osztaly
    /// </summary>
    public class Bicikli : Bindable
    {
        private List<Alkatresz> alkatreszek;       
        private BicikliTipus tipus;
        private bool keszvan;
        private int id;

        /// <summary>
        /// contructor of bike
        /// </summary>
        /// <param name="id">id atadasa</param>
        /// <param name="alkatreszek">szukseges alaktreszek lista atadasa</param>
        /// <param name="tipus">bicikli tipusanak atadasa</param>
        public Bicikli(BicikliTipus tipus, List<Alkatresz> alkatreszek, bool rdy, int id)
        {
            this.tipus = tipus;
            this.alkatreszek = alkatreszek;
            this.keszvan = rdy;
            this.id = id;
        }

        /// <summary>
        /// Gets alkatreszek a biciklihez
        /// </summary>
        public List<Alkatresz> Alkatreszek
        {
            get
            {
                return alkatreszek;
            }            
        }      

        /// <summary>
        /// Gets bicikli tipusa
        /// </summary>
        public BicikliTipus Tipus
        {
            get
            {
                return tipus;
            }           
        }

        /// <summary>
        /// Gets or sets elkeszult-e már a bicikli
        /// </summary>
        public bool Keszvan
        {
            get
            {
                return keszvan;
            }

            set
            {
                keszvan = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets ID
        /// </summary>
        public int ID
        {
            get { return id; }
        }
    }
}
