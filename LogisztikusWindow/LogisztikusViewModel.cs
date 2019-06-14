using Model;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisztikusWindow
{
    class LogisztikusViewModel : Bindable
    {
        private List<Alkatresz> alkatreszKeszlet;
        private AlkatreszRendeles alkatreszRendeles;
        private ObservableCollection<AlkatreszRendeles> alkatreszRendelesek;
        private IDataService dataService;
        private ObservableCollection<Megrendeles> megrendelesek;
        private IMessageService messageService;
        private User felhasznalo;
        private Uzenet uzenet;
        private ObservableCollection<Uzenet> kimenouzik;
        private List<Uzenet> bejovouzik;
        private ObservableCollection<Alkatresz> rendelendoAlkatreszek;       
        private int hanydb;

        public LogisztikusViewModel(User us)
        {
            this.felhasznalo = us;
            dataService = new DataService();
            messageService = new MessageService(felhasznalo.Nev);           
            Rendelheto = (dataService as DataService).RendelhetoAlkatreszek();
            rendelendoAlkatreszek = new ObservableCollection<Alkatresz>();           
            alkatreszKeszlet = dataService.GetAllAlkatresz();
            kimenouzik = (messageService as MessageService).Kiuzik;
            bejovouzik = (messageService as MessageService).Bejovouzik;
            megrendelesek = dataService.GetAllMegrendeles();
            alkatreszRendelesek = dataService.GetAllAlkatreszRendeles();                 
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
        /// Gets or sets rendelendo alkareszek
        /// </summary>
        public ObservableCollection<Alkatresz> RendelendoAlkatreszek
        {
            get
            {
                return rendelendoAlkatreszek;
            }

            set
            {
                rendelendoAlkatreszek = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets or sets alkatrreszrendelesnel, adott alkatreszbol hany db kell
        /// </summary>
        public int Hanydb
        {
            get
            {
                return hanydb;
            }

            set
            {
                hanydb = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets or sets rendelheto alkatreszek
        /// </summary>
        public List<Alkatresz> Rendelheto
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets aktualis logisztikus
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
        /// Gets or sets alkatreszkeszlet
        /// </summary>
        public List<Alkatresz> AlkatreszKeszlet
        {
            get
            {
                return alkatreszKeszlet;
            }

            set
            {
                alkatreszKeszlet = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets or sets alkatreszrendeles
        /// </summary>
        public AlkatreszRendeles AlkatreszRendeles
        {
            get
            {
                return alkatreszRendeles;
            }

            set
            {
                alkatreszRendeles = value;
            }
        }

        /// <summary>
        /// Gets or sets Bejovo uzenetek
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
                OPC();
            }
        }

        /// <summary>
        /// Gets or sets megrendelt alkatrészek
        /// </summary>
        public ObservableCollection<AlkatreszRendeles> AlkatreszRendelesek
        {
            get
            {
                return alkatreszRendelesek;
            }

            set
            {
                alkatreszRendelesek = value;
                OPC();
            }
        }       

        /// <summary>
        /// Gets or sets megrendelések
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
        /// Gets or sets aktuális uzenet
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
            }
        }

        /// <summary>
        /// Gets or sets Kimeno uzenetek
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
        /// alkatreszrendels kuldese
        /// </summary>      
        public void Sendalkatreszrendeles()
        {
            foreach (var it in AlkatreszRendelesek)
            {
                foreach (var l in AlkatreszKeszlet)
                {
                    if (l.Megnevezes == it.Alkatresz.Megnevezes)
                    {
                        l.Mennyi += it.Mennyiseg;
                    }
                }
            }

            (dataService as DataService).SendAlkatreszRendeles(alkatreszRendelesek);            
        }

        /// <summary>
        /// uzenet kuldese
        /// </summary>
        public void Senduzenet()
        {
            Kimenouzik.Add(Uzenet);
            (messageService as MessageService).SendMessage(uzenet);
        }

    }
}
