using APIMySQL.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMySQL.Data.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable> GetRoomUser(int id);
        Task<IEnumerable> GetMessagesUser(int id);
        Task<bool> CreateRoom(Room room, int iduser1, int iduser2);
        Task<bool> DeleteRoom(int idroom);
        Task<bool> CreateMessage(Message message);
    }
}
