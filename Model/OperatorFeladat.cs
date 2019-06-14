using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// allapot felsorolas
    /// </summary>
    public enum Allapot
    {
        Operatorra_var = 1, Folyamatban = 2, Befejezve = 3
    }

    /// <summary>
    /// Ez a operator feladat osztalya
    /// </summary>
    public class OperatorFeladat
    {
        private Allapot allapot;
        private List<Bicikli> bicikli;
        private int haladas;
        private Operator opperator;

        /// <summary>
        /// feladat cons
        /// </summary>
        /// <param name="op">opperator atadas</param>
        /// <param name="id">id atadas</param>       
        public OperatorFeladat(List<Bicikli> bic, Allapot all)
        {
            this.bicikli = bic;
            this.allapot = all;
        }

        /// <summary>
        /// Gets or sets allapot
        /// </summary>
        public Allapot Allapot
        {
            get
            {
                return allapot;
            }

            set
            {
                allapot = value;
            }
        }

        /// <summary>
        /// Gets or sets Bicikli
        /// </summary>
        public List<Bicikli> Bicikli
        {
            get
            {
                return bicikli;
            }

            set
            {
                bicikli = value;
            }
        }

        /// <summary>
        /// Gets or sets munka haladasa
        /// </summary>
        public int Haladas
        {
            get
            {
                return haladas;
            }

            set
            {
                haladas = value;
            }
        }        

        /// <summary>
        /// Gets or sets maga az operator
        /// </summary>
        public Operator Opperator
        {
            get
            {
                return opperator;
            }

            set
            {
                opperator = value;
            }
        }
    }
}
