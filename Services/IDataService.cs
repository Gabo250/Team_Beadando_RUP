using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// data s. interface
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// osszalkatresz
        /// </summary>
        /// <returns>alkatreszek</returns>
        List<Alkatresz> GetAllAlkatresz();

        /// <summary>
        /// ouhgioug
        /// </summary>
        /// <returns>alkatreszrendelesek</returns>
        ObservableCollection<AlkatreszRendeles> GetAllAlkatreszRendeles();

        /// <summary>
        /// uiogiug
        /// </summary>
        /// <returns>minden megrendelés</returns>
        ObservableCollection<Megrendeles> GetAllMegrendeles();

        /// <summary>
        /// ougig
        /// </summary>
        /// <param name="alkatreszRendeles">iugig</param>
        /// <returns>ufuzf</returns>
        void SendAlkatreszRendeles(ObservableCollection<AlkatreszRendeles> alkatreszRendeles);       
    }
}
