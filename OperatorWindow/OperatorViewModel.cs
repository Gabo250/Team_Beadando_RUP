using Model;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorWindow
{
    /// <summary>
    /// OperatorVM class
    /// </summary>
    public class OperatorViewModel : Bindable
    {
        private List<Alkatresz> alkatreszek ;       
        private IDataService  dataService;
        private IMessageService messageService;       
        private Uzenet uzenet;
        private List<Uzenet> bejovouzenetek;
        private ObservableCollection<Uzenet> kimenouzenetek;
        private List<Megrendeles> rendelesek;        
        private ObservableCollection<Bicikli> bikes;        

        /// <summary>
        /// operator vm cons
        /// </summary>
        /// <param name="op">aktualis operator</param>
        public OperatorViewModel(User op)
        {
            this.Operator = op;
            dataService = new DataService();
            messageService = new MessageService(op.Nev);
            bikes = new ObservableCollection<Bicikli>();           
            rendelesek = new List<Megrendeles>();            
            rendeless();
            Elvallaltrendeles();
            alkatreszek = dataService.GetAllAlkatresz();
            bejovouzenetek = (messageService as MessageService).Bejovouzik;
            kimenouzenetek = (messageService as MessageService).Kiuzik;            
        }

        /// <summary>
        /// Gets operator
        /// </summary>
        public User Operator
        {
            get;            
        }

        /// <summary>
        /// Gets or sets alkatreszek
        /// </summary>
        public List<Alkatresz> Alkatreszek
        {
            get
            {
                return alkatreszek;
            }

            set
            {
                alkatreszek = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets or sets vallalt megrendelesek
        /// </summary>
        public Megrendeles Vallalt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets uzik
        /// </summary>
        public List<Uzenet> Bejovouzenetek
        {
            get
            {
                return bejovouzenetek;
            }

            set
            {
                bejovouzenetek = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets or sets kuldendo uzenet
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
        /// Gets or sets rendelesek
        /// </summary>
        public List<Megrendeles> Rendelesek
        {
            get
            {
                return rendelesek;
            }

            set
            {
                rendelesek = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets or sets elvallalt biciklik
        /// </summary>
        public ObservableCollection<Bicikli> Bikes
        {
            get
            {
                return bikes;
            }

            set
            {
                bikes = value;
                OPC();
            }
        }

        /// <summary>
        /// Gets or sets kimenouzenetek
        /// </summary>
        public ObservableCollection<Uzenet> Kimenouzenetek
        {
            get
            {
                return kimenouzenetek;
            }

            set
            {
                kimenouzenetek = value;
            }
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
                    if (it.Nev != Operator.Nev)
                    {
                        l.Add(it);
                    }
                }

                return l;
            }
        }

        /// <summary>
        /// uzenet kuldes
        /// </summary>
        public void Senduzi()
        {
            Kimenouzenetek.Add(uzenet);
            (messageService as MessageService).SendMessage(uzenet);
        }

        /// <summary>
        /// megrendeles updatolasa
        /// </summary>
        /// <param name="rend">akt rendeles</param>
        public void Updaterend(Megrendeles rend)
        {
            (dataService as DataService).UpdateStatus(rend);
        }

        /// <summary>
        /// bvicikli status frissites
        /// </summary>
        /// <param name="bic">adott bicikli</param>
        public void UpdateBikeStatus(Bicikli bic)
        {
            (dataService as DataService).Bikeupdate(bic);
        }

        /// <summary>
        /// kello alkareszek
        /// </summary>
        /// <param name="bikes">megrendeléshez tartozo biciklik</param>
        /// <returns>kello alkatreszek listája</returns>
        public List<Alkatresz> KelloAlkMeghatarozas(List<Bicikli> bikes)
        {
            List<Alkatresz> kell =  (dataService as DataService).KelloAlkatreszMeghatarozas(bikes);
            return kell;
        }

        /// <summary>
        /// van e elég alkatresz a rendeléshez a raktárban
        /// </summary>
        /// <param name="kell">kell alkatresz mennyiség</param>
        /// <returns>van vagy nincs</returns>
        public bool Raktar_kello_alkatreszek_Osszhasonlitas(List<Alkatresz> kell)
        {
            return (dataService as DataService).KelloAlkatreszOsszehasonlitas(alkatreszek, kell);
        }

        /// <summary>
        /// rendelesek amik szerelore varnak vagy saját elvállalt rendelések
        /// </summary>
        private void rendeless()
        {
            foreach (var it in dataService.GetAllMegrendeles())
            {
                if (it.Status == Allapot.Operatorra_var || (it.Dolgozo == Operator.Nev && it.Status != Allapot.Befejezve))
                {
                    rendelesek.Add(it);
                    if (it.Dolgozo == Operator.Nev)
                    {
                        Vallalt = it;
                    }
                }
            }
        }

        /// <summary>
        /// elvállalt rendelésbe a biciklik hozzáadása
        /// </summary>
        private void Elvallaltrendeles()
        {
            foreach (var it in rendelesek)
            {
                if (it.Status == Allapot.Folyamatban)
                {
                    foreach (var bic in it.Biciklik)
                    {
                        bikes.Add(bic);
                    }                    
                }
            }
        }

        /// <summary>
        /// adott rendeléshgez tartozo biciklik elkészultek-e
        /// </summary>
        /// <returns>igen vagy nem</returns>
        public bool OsszbicikliElkeszult()
        {
            int db = 0;
            foreach (var it in bikes)
            {
                if (it.Keszvan)
                {
                    db++;
                }
            }

            if (db == bikes.Count)
            {
                return true;
                
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// update kimutatas
        /// </summary>
        public void UpdateKimut()
        {
            (dataService as DataService).UpdateKimutatas(Operator);
        }

        /// <summary>
        /// bicikli elkeszult
        /// </summary>
        public void BikeElkeszult()
        {
            for (int i = 0; i < Bikes.Count; i++)
            {
                Bikes.RemoveAt(i);
            }

            Vallalt.Status = Allapot.Befejezve;
            Updaterend(Vallalt);
        }

        /// <summary>
        /// raktar update
        /// </summary>
        /// <param name="kell">kellendo alkatreszek</param>
        public void Rakup(List<Alkatresz> kell)
        {
            foreach (var it in kell)
            {
                foreach (var van in Alkatreszek)
                {
                    if (it.Megnevezes == van.Megnevezes)
                    {
                        van.Mennyi -= it.Mennyi;
                    }
                }
            }

            (dataService as DataService).RakUpdate(alkatreszek);
        }
    }
}
