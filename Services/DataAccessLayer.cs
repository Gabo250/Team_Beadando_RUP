using Adatbazis;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// class data
    /// </summary>
    public class DataAccessLayer
    {
        private Functions adat;

        /// <summary>
        /// adatok cons
        /// </summary>
        public DataAccessLayer()
        {
            adat = new Functions();
            RendelesMaxID = adat.Rendid;
        }

        /// <summary>
        /// Gets all alkatresz
        /// </summary>
        public List<Alkatresz> GetAllAlkatresz
        {
            get { return Alkatreszeklist(); }
        }        

        /// <summary>
        /// Gets opperator osszes feladata
        /// </summary>
        public List<OperatorFeladat> GetAllFeladat
        {
            get { return feladatok(); }
        }

        /// <summary>
        /// Gets osszes megrendeles
        /// </summary>
        public ObservableCollection<Megrendeles> GetAllMegrendeles
        {
            get { return Megrendelesek(); }
        }

        /// <summary>
        /// Gets osszesuzenet
        /// </summary>
        public List<Uzenet> GetAllMessages
        {
            get { return Uzenetek(); }
        }

        /// <summary>
        /// dolgozók
        /// </summary>
        /// <returns>useren</returns>
        public List<User> Userek()
        {
            List<User> r = new List<User>();
            foreach (var us in adat.Userek())
            {
                Role ro;
                if (us.BEOSZTAS == "Operator")
                {
                    ro = Role.Operator;
                }
                else if (us.BEOSZTAS == "Logisztikus")
                {
                    ro = Role.Logisztikus;
                }
                else if (us.BEOSZTAS == "Ugyvezeto")                
                {
                    ro = Role.Ugyvezeto;
                }
                else
                {
                    ro = Role.Ugyvezeto;
                }

                r.Add(new User((int)us.ID, us.Jelszo, ro, us.FelhasznaloNev, us.NEV));
            }

            return r;
        }

        /// <summary>
        /// ez nemtom mi
        /// </summary>
        /// <param name="alkatreszRendeles"alkatreszrendeles></param>
        public void SendAlkatreszRendeles(ObservableCollection<AlkatreszRendeles> alkatreszRendelesek)
        {
            adat.AlkatreszRendeles(alkatreszRendelesek);
        }

        /// <summary>
        /// ezt se
        /// </summary>
        /// <param name="uzenet">atadot uzenet</param>
        /// <returns></returns>
        public void SendMessage(Uzenet uzenet)
        {
            adat.SendUzenet(uzenet);
        }
        
        /// <summary>
        /// rendeles status update
        /// </summary>
        /// <param name="rend">megrendeles</param>
        public void UpadateRendeles(Megrendeles rend)
        {
            adat.UpdateMegrendeles(rend);
        }

        /// <summary>
        /// Gets or sets Rendeles max ID
        /// </summary>
        public int RendelesMaxID
        {
            get;
            set;
        }

        /// <summary>
        /// osszes alkatresz propertynek
        /// </summary>
        /// <returns>osszes alkatresz</returns>
        private List<Alkatresz> Alkatreszeklist()
        {
            List<Alkatresz> r = new List<Alkatresz>();            
            r.Add(new Alkatresz(("Bowden")) { Mennyi = (int)adat.RaktarbanlevoAlkareszek().BOWDEN});
            r.Add(new Alkatresz("Kerék") {  Mennyi = (int)adat.RaktarbanlevoAlkareszek().KEREK });
            r.Add(new Alkatresz("Kormány") { Mennyi = (int)adat.RaktarbanlevoAlkareszek().KORMANY });
            r.Add(new Alkatresz("Merevvilla") { Mennyi = (int)adat.RaktarbanlevoAlkareszek().MEREVVILLA });
            r.Add(new Alkatresz("Nyereg") { Mennyi = (int)adat.RaktarbanlevoAlkareszek().NYEREG });
            r.Add(new Alkatresz("Pedál") { Mennyi = (int)adat.RaktarbanlevoAlkareszek().PEDAL });
            r.Add(new Alkatresz("Teleszkop") { Mennyi = (int)adat.RaktarbanlevoAlkareszek().TELESZKOP });
            r.Add(new Alkatresz("Váz") { Mennyi = (int)adat.RaktarbanlevoAlkareszek().VAZ });
            return r;
        }

        /// <summary>
        /// uzenetek propertynek
        /// </summary>
        /// <returns>osszes uzenet</returns>
        private List<Uzenet> Uzenetek()
        {
            List<Uzenet> r = new List<Uzenet>();
            foreach (var item in adat.Uzik())
            {
                r.Add(new Uzenet(item.FELADO, item.CIMZETT, "ariel", item.KOZLEMENY, Convert.ToDateTime(item.KULDESDATUM)));
            }

            return r;
        }

        /// <summary>
        /// megrendelesek propertynek
        /// </summary>
        /// <returns>megrendelesek listaja</returns>
        private ObservableCollection<Megrendeles> Megrendelesek()
        {
            ObservableCollection<Megrendeles> m = new ObservableCollection<Megrendeles>();
            foreach (var it in adat.Osszrendeles())
            {
                List<Bicikli> b = adat.RendelesekheztartozoBiciklik((int)it.ID);
                m.Add(new Megrendeles((int)it.ID, b, (Allapot)it.STATUSZ, it.NEV, it.OPERATOR));
            }

            return m;
        }

        /// <summary>
        /// bicikli statusz változás
        /// </summary>
        /// <param name="bike">bicikli</param>
        public void Megrend_BicUpdate(Bicikli bike)
        {
            adat.UpdateBiciklikesz(bike);
        }

        /// <summary>
        /// operatorfeladatok
        /// </summary>
        /// <returns>operator feladatok</returns>
        private List<OperatorFeladat> feladatok()
        {
            List<OperatorFeladat> op = new List<OperatorFeladat>();
            foreach (var item in Megrendelesek())
            {
                op.Add(new OperatorFeladat(item.Biciklik, (Allapot)item.Status));
            }

            return op;
        }

        /// <summary>
        /// kesz biciklikor +1 hozzaadas
        /// </summary>
        /// <param name="us">adot operatornak</param>
        public void UpdateKimut(User us)
        {
            adat.UpdateKimutatas(us.Nev, 1);
        }

        /// <summary>
        /// raktárbol adot rendeléshez a kellő alkatrészek levonása
        /// </summary>
        /// <param name="alk">alkatreszek</param>
        public void UpRaktar(List<Alkatresz> alk)
        {
            adat.RaktarUpdate(alk);
        }

        /// <summary>
        /// rendelés felvétel
        /// </summary>
        /// <param name="rend">rendelés</param>
        public void RendelesFelvetel(Megrendeles rend)
        {
            adat.RenelesFelvetel(rend);
        }

        /// <summary>
        /// bic alkatreszek
        /// </summary>
        /// <returns>list</returns>
        public List<Bicikli> BicAlkat()
        {
            return adat.BichezAlkareszek();
        }

        /// <summary>
        /// hanby db bic van már kész
        /// </summary>
        /// <param name="rend">rendeles</param>
        /// <returns>mennyi</returns>
        public int Rendhez_bic_mennyiRdy(Megrendeles rend)
        {
            return adat.Rendeleshez_tartozo_Biciklibol_Hanyvan_Kesz(rend);
        }

        /// <summary>
        /// operator kimutatasok
        /// </summary>
        /// <returns>lista</returns>
        public List<Kimutatas> Op_Kimutatasok()
        {
            List<Kimutatas> ki = new List<Kimutatas>();
            foreach (var it in adat.Kimutatas())
            {
                ki.Add(new Kimutatas(it.NEV, (int)it.ID, (int)it.MENNYISEG));
            }

            return ki;
        }
    }
}
