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
    /// data s. class
    /// </summary>
    public class DataService : IDataService
    {
        private List<Alkatresz> alkatreszek;
        private ObservableCollection<AlkatreszRendeles> alkatreszRendelesek;
        private ObservableCollection<Megrendeles> megrendelesek;
        private DataAccessLayer repository;
        private List<User> userek;

        /// <summary>
        /// dataservice cons
        /// </summary>
        public DataService()
        {
            repository = new DataAccessLayer();
            alkatreszRendelesek = new ObservableCollection<AlkatreszRendeles>();
            megrendelesek = repository.GetAllMegrendeles;
            alkatreszek = repository.GetAllAlkatresz;
            userek = repository.Userek();
            RendelesIDMAx = repository.RendelesMaxID;
        }
             
        /// <summary>
        /// allalkatresz lista
        /// </summary>
        /// <returns>alkatreszek</returns>
        public List<Alkatresz> GetAllAlkatresz()
        {
            return alkatreszek;
        }

        /// <summary>
        /// rendeles idja
        /// </summary>
        public int RendelesIDMAx
        {
            get;
            set;
        }

        /// <summary>
        /// owihfow
        /// </summary>
        /// <returns>alkrendelesek</returns>
        public ObservableCollection<AlkatreszRendeles> GetAllAlkatreszRendeles()
        {
            return alkatreszRendelesek;
        }       

        /// <summary>
        /// ohiu
        /// </summary>
        /// <returns>megrendelesek</returns>
        public ObservableCollection<Megrendeles> GetAllMegrendeles()
        {
            return repository.GetAllMegrendeles;
        }
        
        /// <summary>
        /// ougi
        /// </summary>
        /// <param name="alkatreszRendeles">alkrendeles atadasa</param>
        public void SendAlkatreszRendeles(ObservableCollection<AlkatreszRendeles> alkatreszRendeles)
        {
            repository.SendAlkatreszRendeles(alkatreszRendeles);
        }

        /// <summary>
        /// Gets felhasznalok
        /// </summary>
        public List<User> Userek
        {
            get
            {
                return userek;
            }           
        }

        /// <summary>
        /// megrendelesz statuszanak frissitese
        /// </summary>
        /// <param name="rend">adott rendeles</param>
        public void UpdateStatus(Megrendeles rend)
        {
            repository.UpadateRendeles(rend);
        }

        /// <summary>
        /// bike update
        /// </summary>
        /// <param name="bic">adott bic</param>
        public void Bikeupdate(Bicikli bic)
        {
            repository.Megrend_BicUpdate(bic);
        }

        /// <summary>
        /// kimutatasupdate
        /// </summary>
        /// <param name="us">operator</param>
        public void UpdateKimutatas(User us)
        {
            repository.UpdateKimut(us);
        }

        /// <summary>
        /// rendelheto alkareszek
        /// </summary>
        /// <returns>alkatreszek listaja</returns>
        public List<Alkatresz> RendelhetoAlkatreszek()
        {
            List<Alkatresz> r = new List<Alkatresz>();
            r.Add(new Alkatresz(("Bowden")));
            r.Add(new Alkatresz("Kerék"));
            r.Add(new Alkatresz("Kormány"));
            r.Add(new Alkatresz("Merevvilla"));
            r.Add(new Alkatresz("Nyereg"));
            r.Add(new Alkatresz("Pedál"));
            r.Add(new Alkatresz("Teleszkop"));
            r.Add(new Alkatresz("Váz"));
            return r;
        }

        /// <summary>
        /// továbbitas a dataaccess felé
        /// </summary>
        /// <param name="alk">alkatreszek</param>
        public void RakUpdate(List<Alkatresz> alk)
        {
            repository.UpRaktar(alk);
        }

        /// <summary>
        /// megrendeléshez kello alkatreszek meghatarozasa
        /// </summary>
        /// <param name="biciklik">megrendelt biciklik</param>
        /// <returns>kellendo alkatresz mennyiség</returns>
        public List<Alkatresz> KelloAlkatreszMeghatarozas(List<Bicikli> biciklik)
        {
            List<Alkatresz> kello = RendelhetoAlkatreszek();
            foreach (var bicikli in biciklik)
            {
                foreach (var alkatresz in bicikli.Alkatreszek)
                {
                    foreach (var kelloalk in kello)
                    {
                        if (alkatresz.Megnevezes == kelloalk.Megnevezes)
                        {
                            kelloalk.Mennyi += alkatresz.Mennyi;
                        }
                    }
                }
            }

            return kello;
        }

        /// <summary>
        /// van e elég alkatrész a megrendeléshez
        /// </summary>
        /// <param name="van">raktárban lévő alkatrészek</param>
        /// <param name="kell">megrendeléshez kellő alkatérszek</param>
        /// <returns>van e elég vagy nincs</returns>
        public bool KelloAlkatreszOsszehasonlitas(List<Alkatresz> van, List<Alkatresz> kell)
        {
            foreach (var v in van)
            {
                foreach (var ke in kell)
                {
                    if (v.Mennyi - ke.Mennyi < 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// rendelés felvétele
        /// </summary>
        /// <param name="rend">rendelés</param>
        public void RendelesFelvetele(Megrendeles rend)
        {
            repository.RendelesFelvetel(rend);
        }

        /// <summary>
        /// rendelheto bicikli tipusok
        /// </summary>
        /// <returns>bicikli tipusok listaja</returns>
        public List<BicikliTipus> Biketipusok()
        {
            List<BicikliTipus> types = new List<BicikliTipus>();
            types.Add(BicikliTipus.Orszaguti);
            types.Add(BicikliTipus.Terep);
            types.Add(BicikliTipus.Verseny);
            return types;
        }

        /// <summary>
        /// biciklik alkareszeivel egyutt
        /// </summary>
        /// <returns>list</returns>
        public List<Bicikli> Bicalkatreszekkel()
        {
            return repository.BicAlkat();
        }

        /// <summary>
        /// adot rendelesnél hány db bicikli van kész már
        /// </summary>
        /// <param name="rend">rendeles
        /// <returns>mennyi/returns>
        public int Megrendeleshez_Menyi_bic_Done(Megrendeles rend)
        {
            return repository.Rendhez_bic_mennyiRdy(rend);
        }

        /// <summary>
        /// szerelok kimutatásai
        /// </summary>
        /// <returns>kimutatas lista</returns>
        public List<Kimutatas> Operator_Kimutatasok()
        {
            return repository.Op_Kimutatasok();
        }

        /// <summary>
        /// a max elkészitett bicikli szám meghatározása
        /// </summary>
        /// <returns>max bic szám</returns>
        public int Max_keszitett_Bic(List<Kimutatas> operatorok)
        {
            int max = 0;
            foreach (var it in operatorok)
            {
                if (it.Keszbiciklik > max)
                {
                    max = it.Keszbiciklik;
                }
            }

            return max;
        }
    }
}
