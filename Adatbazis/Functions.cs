using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adatbazis
{
    /// <summary>
    /// funkciók osztálya
    /// </summary>
    public class Functions
    {
        private /*static*/ TITAN_KEREKEntities entiti;
        private /*static*/ int iuz;
        private int imegrend;
        private int rendid;
        private int bicrendid;      

        /// <summary>
        /// functions cons
        /// </summary>
        public Functions()
        {
            entiti = new TITAN_KEREKEntities();            
            if (Uzik().Count == 0)
            {
                iuz = 0;
            }
            else
            {
                iuz = (int)entiti.Uzeneteks.Max(x => x.ID);
            }

            bicrendid = (int)entiti.BiciklikRendelesres.Max(x => x.ID);
            Rendid = (int)entiti.Rendeleseks.Max(x => x.ID);                    
            imegrend = (int)entiti.Rendeleseks.Max(x => x.ID);
        }
       
        /// <summary>
        /// megadja hogy milyen regisztralt felhasznalok vannak
        /// </summary>
        /// <returns>felhasznalopk listajat</returns>
        public /*static*/ List<Felhasznalo> Userek()
        {
            var us = entiti.Felhasznaloes.Select(x => x).ToList();
            return us;
        }

        /// <summary>
        /// megadja az adatbazisbol az osszes uzit
        /// </summary>
        /// <returns>uzenetek listaja</returns>
        public /*static*/ List<Uzenetek> Uzik()
        {
            var uz = entiti.Uzeneteks.Select(y => y).ToList();
            return uz;
        }

        /// <summary>
        /// rendelés aktuális legnagyobb ID-ja
        /// </summary>
        public int Rendid
        {
            get
            {
                return rendid;
            }

            set
            {
                rendid = value;
            }
        }

        /// <summary>
        /// biciklik hozzajuk tartozo alkatrszel
        /// </summary>
        /// <returns>list</returns>
        public List<Bicikli> BichezAlkareszek()
        {
            var bialk = entiti.BikeAlkatreszeks.Select(x => x).ToList();
            List<Bicikli> bi = new List<Bicikli>();
            foreach (var biciklik in bialk)
            {
                BicikliTipus b;
                if (biciklik.TIPUS == "Orszaguti")
                {
                    b = BicikliTipus.Orszaguti;
                }
                else if (biciklik.TIPUS == "Terep")
                {
                    b = BicikliTipus.Terep;
                }
                else
                {
                    b = BicikliTipus.Verseny;
                }

                List<Alkatresz> alk = new List<Alkatresz>();
                alk.Add(new Alkatresz("Bowden") { Mennyi = (int)biciklik.BOWDEN });
                alk.Add(new Alkatresz("Kerek") { Mennyi = (int)biciklik.KEREK });
                alk.Add(new Alkatresz("Kormany") { Mennyi = (int)biciklik.KORMANY });
                alk.Add(new Alkatresz("Merevvilla") { Mennyi = (int)biciklik.MEREVVILLA });
                alk.Add(new Alkatresz("Nyereg") { Mennyi = (int)biciklik.NYEREG });
                alk.Add(new Alkatresz("Pedal") { Mennyi = (int)biciklik.PEDAL });
                alk.Add(new Alkatresz("Teleszkop") { Mennyi = (int)biciklik.TELESZKOP });
                alk.Add(new Alkatresz("Vaz") { Mennyi = (int)biciklik.VAZ });
                bi.Add(new Bicikli(b, alk, false, (int)biciklik.ID));
            }

            return bi;
        }

        /// <summary>
        /// Megadja tipustol fuggoen mennyi alkatresz kell egy biciklihez
        /// </summary>
        /// <returns></returns>
        public /*static*/ List<BikeAlkatreszek> BiciklihezMennyialkatreszkell()
        {
            var bialk = entiti.BikeAlkatreszeks.Select(x => x).ToList();
            return bialk;
        }

        /// <summary>
        /// Megadja az osszes rendelest
        /// </summary>
        /// <returns>ossz rendelesek listaja</returns>
        public /*static*/ List<Rendelesek> Osszrendeles()
        {
            var rend = entiti.Rendeleseks.Select(x => x).ToList();
            return rend;
        }

        /// <summary>
        /// megadja melyik dolgozo hany biciklit szerelt ossze eddig
        /// </summary>
        /// <returns>kimutatasok listaja</returns>
        public /*static*/ List<Kimutata> Kimutatas()
        {
            var ki = entiti.Kimutatas.Select(x => x).ToList();
            return ki;
        }

        /// <summary>
        /// adott megrendelésnél hány bicikli van kész
        /// </summary>
        /// <param name="rend">megrendeles</param>
        /// <returns>amennyi kesz van</returns>
        public int Rendeleshez_tartozo_Biciklibol_Hanyvan_Kesz(Megrendeles rend)
        {
            var ren = entiti.BiciklikRendelesres.Where(x => x.RENDID == rend.Id).ToList();
            int mennyi = 0;
            foreach (var it in ren)
            {
                if (it.STATUSZ == 2)
                {
                    mennyi++;
                }
            }

            return mennyi;
        }

        /// <summary>
        /// megadot ID-u rendleshez biciklik
        /// </summary>
        /// <param name="rendelesID">rendeleshez tartozo id</param>
        /// <returns></returns>
        public List<Bicikli> RendelesekheztartozoBiciklik(int rendelesID)
        {
            var bikok = entiti.BiciklikRendelesres.Where(x => x.RENDID == rendelesID).Select(y => y).ToList();
            List<Bicikli> biciklik = new List<Bicikli>();
            foreach (var it in bikok)
            {
                BicikliTipus bt;
                if (it.TIPUS == "Orszaguti")
                {
                    bt = BicikliTipus.Orszaguti;
                }
                else if (it.TIPUS == "Terep")
                {
                    bt = BicikliTipus.Terep;
                }
                else
                {
                    bt = BicikliTipus.Verseny;
                }

                List<Alkatresz> alkk = new List<Alkatresz>();
                foreach (var item in BiciklihezMennyialkatreszkell())
                {
                    if (item.TIPUS == bt.ToString())
                    {
                        alkk.Add(new Alkatresz("Bowden") { Mennyi = (int)item.BOWDEN });
                        alkk.Add(new Alkatresz("Kerék") { Mennyi = (int)item.KEREK });
                        alkk.Add(new Alkatresz("Kormány") { Mennyi = (int)item.KORMANY });
                        alkk.Add(new Alkatresz("Merevvilla") { Mennyi = (int)item.MEREVVILLA });
                        alkk.Add(new Alkatresz("Nyereg") { Mennyi = (int)item.NYEREG });
                        alkk.Add(new Alkatresz("Pedál") { Mennyi = (int)item.PEDAL });
                        alkk.Add(new Alkatresz("Teleszkop") { Mennyi = (int)item.TELESZKOP });
                        alkk.Add(new Alkatresz("Váz") { Mennyi = (int)item.VAZ });
                    }
                }

                bool kesz;
                if (it.STATUSZ == 1)
                {
                    kesz = false;
                }
                else
                {
                    kesz = true;
                }

                biciklik.Add(new Bicikli(bt, alkk, kesz, (int)it.ID));               
            }

            return biciklik;
        }

        /// <summary>
        /// operator osszeszerelét bicikli mennyiségét növeli
        /// </summary>
        /// <param name="nev">operator neve</param>
        /// <param name="menyi">osszeszerelt bicikli mennyisege</param>
        public /*static*/ void UpdateKimutatas(string nev, int menyi)
        {
            var kim = entiti.Kimutatas.Where(x => x.NEV == nev).Select(y => y).First();
            kim.MENNYISEG += menyi;
            entiti.SaveChanges();
        }

        /// <summary>
        /// adot megrendeles statuszat valtoztatja
        /// </summary>
        /// <param name="meg">a megrendeles</param>
        public /*static*/ void UpdateMegrendeles(Megrendeles meg)
        {
            var me = entiti.Rendeleseks.Where(x => x.ID == meg.Id).First();
            me.STATUSZ = (int)meg.Status;
            me.OPERATOR = meg.Dolgozo;
            entiti.SaveChanges();
        }

        /// <summary>
        /// uj megrendelés felvétele
        /// </summary>
        /// <param name="rendeles">adott rendelés</param>
        public void RenelesFelvetel(Megrendeles rendeles)
        {
            var rend = new Rendelesek()
            {
                ID = ++Rendid,
                NEV = rendeles.Rendelo,
                STATUSZ = (int)rendeles.Status
            };

            foreach (var it in rendeles.Biciklik)
            {
                var bices = new BiciklikRendelesre()
                {
                    ID = ++bicrendid,
                    RENDID = Rendid,
                    STATUSZ = 1,
                    TIPUS = it.Tipus.ToString()
                };

                entiti.SaveChanges();
            }

            entiti.SaveChanges();         
        }

        /// <summary>
        /// Adott bicikli statusz frissites
        /// </summary>
        /// <param name="bi">adott bike</param>
        public void UpdateBiciklikesz(Bicikli bi)
        {
            var bicik = entiti.BiciklikRendelesres.Where(x => x.ID == bi.ID).First();
            if (bi.Keszvan)
            {
                bicik.STATUSZ = 2;
            }
            else
            {
                bicik.STATUSZ = 1;
            }

            entiti.SaveChanges();
        }

        /// <summary>
        /// Megadja hogy melyik alkatreszbol mennyi van
        /// </summary>
        /// <returns></returns>
        public /*static*/ Raktar RaktarbanlevoAlkareszek()
        {
            var rak = entiti.Raktars.Select(x => x).First();
            return rak;
        }

        /// <summary>
        /// kello anyagok levonasa raktarbol
        /// </summary>
        /// <param name="alkatreszek">levonando alkatreszek</param>
        public void RaktarUpdate(List<Alkatresz> alkatreszek)
        {
            var keszlet = entiti.Raktars.Select(x => x).First();
            foreach (var it in alkatreszek)
            {
                switch (it.Megnevezes)
                {
                    case "Bowden":
                        keszlet.BOWDEN = it.Mennyi;
                        break;
                    case "Kerek":
                        keszlet.KEREK = it.Mennyi;
                        break;
                    case "Kormany":
                        keszlet.KORMANY = it.Mennyi;
                        break;
                    case "Merevvilla":
                        keszlet.MEREVVILLA = it.Mennyi;
                        break;
                    case "Nyereg":
                        keszlet.NYEREG = it.Mennyi;
                        break;
                    case "Pedal":
                        keszlet.PEDAL = it.Mennyi;
                        break;
                    case "Teleszkop":
                        keszlet.TELESZKOP = it.Mennyi;
                        break;
                    case "Vaz":
                        keszlet.VAZ = it.Mennyi;
                       break;
                }
            }

            entiti.SaveChanges();
        }

        /// <summary>
        /// Elkuld egy uzenetet valakinek
        /// </summary>
        /// <param name="uzenet">kuldott uzenet</param>
        public /*static*/ void SendUzenet(Uzenet uzenet)
        {
            var uzi = new Uzenetek()
            {
                ID = ++iuz,
                FELADO = uzenet.Felado,
                CIMZETT = uzenet.Cimzett,
                KULDESDATUM = uzenet.KuldesDatuma,
                KOZLEMENY = uzenet.Targy
            };

            entiti.Uzeneteks.Add(uzi);
            entiti.SaveChanges();
        }

        /// <summary>
        /// Alkatreszt rendel
        /// </summary>
        /// <param name="alk">Megrendelendo alkatresz</param>
        public /*static*/ void AlkatreszRendeles(ObservableCollection<AlkatreszRendeles> alk)
        {
            var keszlet = entiti.Raktars.FirstOrDefault();
            foreach (var it in alk)
            {
                switch (it.Alkatresz.Megnevezes)
                {
                    case "Bowden":
                        keszlet.BOWDEN += it.Mennyiseg;
                        break;
                    case "Kerék":
                        keszlet.KEREK += it.Mennyiseg;
                        break;
                    case "Kormány":
                        keszlet.KORMANY += it.Mennyiseg;
                        break;
                    case "Merevvilla":
                        keszlet.MEREVVILLA += it.Mennyiseg;
                        break;
                    case "Nyereg":
                        keszlet.NYEREG += it.Mennyiseg;
                        break;
                    case "Pedál":
                        keszlet.PEDAL += it.Mennyiseg;
                        break;
                    case "Teleszkop":
                        keszlet.TELESZKOP += it.Mennyiseg;
                        break;
                    case "Váz":
                        keszlet.VAZ += it.Mennyiseg;
                        break;
                }
            }          

            entiti.SaveChanges();
        }
    }
}
