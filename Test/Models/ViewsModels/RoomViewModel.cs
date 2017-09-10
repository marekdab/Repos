using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAL;

namespace Models.ViewsModels
{
    public class RoomViewModel
    {
        public List<Room> ListOfRooms { get; set; }

        public List<Player> PlayersInRoom { get; set; }

        public Player UsPlayer { get; set; }

        public Room Room { get; set; }

        public List<Game> GamesInRoom { get; set; }

        public Game NewGame { get; set; }

        //room
        public Room NewRoom { get; set; }

        public Room SelectedRoom { get; set; }
    }
}
