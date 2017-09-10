using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.ViewsModels;
using DAL.DAL;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        PlayerDAL PlayerDAL = new PlayerDAL();
        RoomDAL RoomDAL = new RoomDAL();
        GameDAL GameDAL = new GameDAL();

        public ActionResult Index()
        {
            PlayerViewModel playerViewModel = new PlayerViewModel();
            return View(playerViewModel);
        }

        [HttpPost]
        public ActionResult Index(PlayerViewModel playerViewModel)
        {
            PlayerDAL playerDAL = new PlayerDAL();

            List<Player> ListOfPlayers = playerDAL.GetAll();
            bool IsExisting = false;

            foreach (var player in ListOfPlayers)
            {
                if(playerViewModel.Nick == player.Nick)
                {
                    IsExisting = true;
                }

            }

            if(IsExisting)
            {
                Session["username"] = playerViewModel.Nick;
                //jesli istnieje to logujemy
            }
            else
            {
                playerDAL.Add(new Player { Nick = playerViewModel.Nick });
                Session["username"] = playerViewModel.Nick;
                //dodajemy do sesji
            }

            return View(playerViewModel);
        }

        public ActionResult Room()
        {
            RoomViewModel roomViewModel = new RoomViewModel();

            roomViewModel.UsPlayer = PlayerDAL.GetByName(Session["username"].ToString());
            roomViewModel.ListOfRooms = RoomDAL.GetAll();

            if (roomViewModel.UsPlayer.Room != null)
            {
                roomViewModel.Room = RoomDAL.Get(Convert.ToInt32(roomViewModel.UsPlayer.Room));
                roomViewModel.PlayersInRoom = PlayerDAL.GetAll().Where(x => x.Room == roomViewModel.Room.Id).ToList();
                roomViewModel.GamesInRoom = GameDAL.GetAll().Where(x => x.Room == roomViewModel.Room.Id).ToList();
            }

            return View(roomViewModel);
        }


        [HttpPost]
        public RedirectToRouteResult CreateRoom(RoomViewModel roomViewModel)
        {

            List<Room> ListOfRooms = RoomDAL.GetAll();
            bool IsExisting = false;

            foreach (var room in ListOfRooms)
            {
                if (roomViewModel.NewRoom.Name == room.Name)
                {
                    IsExisting = true;
                }
            }

            if(!IsExisting)
            {
                RoomDAL.Add(new Room
                {
                    Name = roomViewModel.NewRoom.Name,
                    Password = roomViewModel.NewRoom.Password,
                    CreatorId = PlayerDAL.GetByName(Session["username"].ToString()).Id
                });
            }

            return RedirectToAction("Room");
        }

        [HttpPost]
        public RedirectToRouteResult GoToRoom(string RoomId, string Password)
        {
            if (Password == "")
                Password = null;

            int IdRoom = Convert.ToInt32(RoomId);

            if(RoomDAL.Get(IdRoom).Password == Password)
            {
                var player = PlayerDAL.GetByName(Session["username"].ToString());
                player.Room = IdRoom;
                PlayerDAL.Update(player);
            }
                return RedirectToAction("Room");
        }

        [HttpPost]
        public RedirectToRouteResult OutFromRoom()
        {
            var player = PlayerDAL.GetByName(Session["username"].ToString());
            player.Room = null;
            PlayerDAL.Update(player);
            return RedirectToAction("Room");
        }

        [HttpPost]
        public RedirectToRouteResult AddGame(RoomViewModel roomViewModel)
        {
            var game = new Game();
            game = roomViewModel.NewGame;
            game.Room = PlayerDAL.GetByName(Session["username"].ToString()).Room;

            if (roomViewModel.NewGame.Password == "")
                game.Password = null;

            GameDAL.Add(game);
            return RedirectToAction("Room");
        }
    }
}