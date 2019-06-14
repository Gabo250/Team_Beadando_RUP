using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// message serv interface
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// get
        /// </summary>
        /// <returns>all message</returns>
        List<Uzenet> GetAllMessages();

        /// <summary>
        /// kuldes
        /// </summary>
        /// <param name="uzenet">uzi</param>
        /// <returns>kuldott e</returns>
        void SendMessage(Uzenet uzenet);
    }
}
