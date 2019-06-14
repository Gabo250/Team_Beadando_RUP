using Model;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UgyvezetoWindow
{
    public class UgyvezetoViewModel : Bindable
    {
        private IDataService dataService;        
        private ObservableCollection<Megrendeles> megrendelesek;
        private IMessageService messageService;      
        private Uzenet uzenet;
        private User felhasznalo;
        private List<Uzenet> bejovouzik;
        private ObservableCollection<Uzenet> kimenouzik;
        private Megrendeles ujrendeles;
        private ObservableCollection<Bicikli> rendeltbiciklik;

        public UgyvezetoViewModel(User akt)
        {
            felhasznalo = akt;
            messageService = new MessageService(felhasznalo.Nev);
            dataService = new DataService();
            Biketipusok = (dataService as DataService).Biketipusok();
            kimenouzik = (messageService as MessageService).Kiuzik;
            bejovouzik = (messageService as MessageService).Bejovouzik;
            rendeltbiciklik = new ObservableCollection<Bicikli>();
            megrendelesek = dataService.GetAllMegrendeles();
            RendMaxID = (dataService as DataService).RendelesIDMAx;                   
        }

        /// <summary>
        /// rend id
        /// </summary>
        public int RendMaxID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets uj rendeléshez tartozó biciklik
        /// </summary>
        public ObservableCollection<Bicikli> Rendeltbiciklik
        {
            get
            {
                return rendeltbiciklik;
            }

            set
            {
                rendeltbiciklik = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets or sets megrendelés felvetele
        /// </summary>
        public Megrendeles Ujrendeles
        {
            get
            {
                return ujrendeles;
            }

            set
            {
                ujrendeles = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets or sets bike tipusok
        /// </summary>
        public List<BicikliTipus> Biketipusok
        {
            get;
            set;
        }

        /// <summary>
        /// Gets tobbi user
        /// </summary>
        public List<User> Kolegak
        {
            get
            {
                List<User> l = new List<User>();
                foreach (var it in (dataService as DataService).Userek)
                {
                    if (it.Nev != felhasznalo.Nev)
                    {
                        l.Add(it);
                    }
                }

                return l;
            }
        }       

        /// <summary>
        /// Gets or sets aktuális ugyvezeto
        /// </summary>
        public User Felhasznalo
        {
            get
            {
                return felhasznalo;
            }

            set
            {
                felhasznalo = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets or sets kimenőuzenetek
        /// </summary>
        public ObservableCollection<Uzenet> Kimenouzik
        {
            get
            {
                return kimenouzik;
            }

            set
            {
                kimenouzik = value;
            }
        }

        /// <summary>
        /// Gets or sets megrendelesek
        /// </summary>
        public ObservableCollection<Megrendeles> Megrendelesek
        {
            get
            {
                return megrendelesek;
            }

            set
            {
                megrendelesek = value;
                OPC();
            }
        }        

        /// <summary>
        /// Gets or sets új üzenet
        /// </summary>
        public Uzenet Uzenet
        {
            get
            {
                return uzenet;
            }

            set
            {
                uzenet = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets ot sets bejovo uzenetek
        /// </summary>
        public List<Uzenet> Bejovouzik
        {
            get
            {
                return bejovouzik;
            }

            set
            {
                bejovouzik = value;
            }
        }

        /// <summary>
        /// bicikli rendelt biciklikhez adása
        /// </summary>
        /// <param name="bic">adott bicikli</param>
        private void AddBike(Bicikli bic)
        {
            rendeltbiciklik.Add(bic);
        }

        /// <summary>
        /// kiválasztja a biciklit amit rendeléshez adunk
        /// </summary>
        /// <param name="tip">bicikli tipusa</param>
        public void RendhezBicikli(BicikliTipus tip, int mennyi)
        {
            List<Bicikli> l = (dataService as DataService).Bicalkatreszekkel();
            Bicikli b = l.Where(x => x.Tipus == tip).First();
            for (int i = 0; i < mennyi; i++)
            {
                AddBike(b);
            }          
        }

        /// <summary>
        /// Rendeles felvetele adatbazisba
        /// </summary>
        public void UjrendelesFelvetele()
        {
            Megrendelesek.Add(Ujrendeles);
            (dataService as DataService).RendelesFelvetele(ujrendeles);
        }

        /// <summary>
        /// hany bic van kész rendeléshez
        /// </summary>
        /// <param name="rend">rendeles</param>
        /// <returns>mennyi</returns>
        public int Bic_hany_van_kesz(Megrendeles rend)
        {
            return (dataService as DataService).Megrendeleshez_Menyi_bic_Done(rend);
        }

        /// <summary>
        /// uzenet kuldes
        /// </summary>
        public void UzenetKuldes()
        {
            Kimenouzik.Add(uzenet);
            (messageService as MessageService).SendMessage(uzenet);
        }
    }
}
