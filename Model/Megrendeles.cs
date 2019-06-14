using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Megrendeles osztaly
    /// </summary>
    public class Megrendeles : Bindable
    {
        private List<Bicikli> biciklik;
        private int id;
        private Allapot status;
        private int bikemennyi;
        private string rendelo;
        private string dolgozo;

        /// <summary>
        /// Cons of megrendeles
        /// </summary>
        /// <param name="id">id atadasa</param>
        /// <param name="megrendelo">megrendelo atadasa</param>
        public Megrendeles(int id, List<Bicikli> bi, Allapot status, string nev, string dolgozo)
        {
            this.id = id;
            this.biciklik = bi;
            this.status = status;
            this.bikemennyi = biciklik.Count;
            this.rendelo = nev;
            this.dolgozo = dolgozo;            
        }

        /// <summary>
        /// Gets or sets biciklik listaja
        /// </summary>
        public List<Bicikli> Biciklik
        {
            get
            {
                return biciklik;
            }

            set
            {
                biciklik = value;
            }
        }

        /// <summary>
        /// Gets or sets hany van kesz
        /// </summary>
        public int Hanyvankesz
        {
            get
            {
                return mennyi();
            }

            set
            {
                Hanyvankesz = value;
            }       
        }

        /// <summary>
        /// Gets megrendeles ID-ja
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }                       
        }

        /// <summary>
        /// Gets or sets status
        /// </summary>
        public Allapot Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets hany bicikli van a megrendelésbe
        /// </summary>
        public int Bikemennyi
        {
            get { return bikemennyi; }           
        }

        /// <summary>
        /// Gets megrendelo neve
        /// </summary>
        public string Rendelo
        {
            get { return rendelo; }
        }

        /// <summary>
        /// Gets or sets aktualis rendelesen dolgozo operator
        /// </summary>
        public string Dolgozo
        {
            get
            {
                return dolgozo;
            }

            set
            {
                dolgozo = value;
                OPC();
            }
        }

        private int mennyi()
        {
            int a = 0;
            foreach (var it in biciklik)
            {
                if (it.Keszvan)
                {
                    a++;
                }
            }

            return a;
        }
    }
}
