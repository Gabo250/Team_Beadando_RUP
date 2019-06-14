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
    /// mess service class
    /// </summary>
    public class MessageService : IMessageService
    {
        private DataAccessLayer repository;
        private List<Uzenet> uzenetek;
        private List<Uzenet> bejovouzik;
        private ObservableCollection<Uzenet> kimenouzik;
        private string nev;

        public MessageService(string nev)
        {
            this.nev = nev;
            repository = new DataAccessLayer();
            uzenetek = repository.GetAllMessages;
            bejovouzik = new List<Uzenet>();
            kimenouzik = new ObservableCollection<Uzenet>();
            beuzik();
            kiuzik();
        }      

        /// <summary>
        /// ez
        /// </summary>
        /// <returns>uzik</returns>
        public List<Uzenet> GetAllMessages()
        {
            return uzenetek;
        }
        
        /// <summary>
        /// kuld
        /// </summary>
        /// <param name="uzenet">uzi</param>
        /// <returns></returns>
        public void SendMessage(Uzenet uzenet)
        {
            repository.SendMessage(uzenet);
        }

        /// <summary>
        /// Gets or sets bejovo uzenetek
        /// </summary>
        public List<Uzenet> Bejovouzik
        {
            get { return bejovouzik; }
            set { bejovouzik = value; }
        }

        /// <summary>
        /// Gets or sets kimenouzenetek
        /// </summary>
        public ObservableCollection<Uzenet> Kiuzik
        {
            get
            {
                return kimenouzik;
            }

            set
            {
                this.kimenouzik = value;
            }
        }

        private void kiuzik()
        {
            foreach (var it in repository.GetAllMessages)
            {
                if (it.Felado == nev)
                {
                    kimenouzik.Add(it);
                }
            }
        }

        private void beuzik()
        {
            foreach (var it in repository.GetAllMessages)
            {
                if (it.Cimzett == nev)
                {
                    bejovouzik.Add(it);
                }
            }
        }
    }
}
