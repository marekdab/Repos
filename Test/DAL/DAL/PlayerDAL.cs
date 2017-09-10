using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAL
{
    public class PlayerDAL
    {
        public Player Add(Player model)
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                var m = ctx.Players.Add(new Player()
                {
                    Nick = model.Nick
                });//tu dodajemy model
                ctx.SaveChanges();//tu zapisujemy zmiany
                model.Id = m.Id;
                ctx.SaveChanges();
                return model;
            }
        }

        public void Delete(int id)
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                var ic = ctx.Players.Where(x => x.Id == id).FirstOrDefault();
                ctx.Players.Remove(ic);
                ctx.SaveChanges();
            }
        }

        public Player Get(int PlayerId)
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                return ctx.Players.Where(x => x.Id == PlayerId).FirstOrDefault();
            }
        }

        public Player GetByName(string PlayerName)
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                return ctx.Players.Where(x => x.Nick.Contains(PlayerName)).FirstOrDefault();
            }
        }

        public List<Player> GetAll()
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                return ctx.Players.ToList();
            }
        }

        public Player Update(Player model)
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                var ic = ctx.Players.Where(x => x.Id == model.Id).FirstOrDefault();
                ic.Nick = model.Nick;
                ic.Room = model.Room;
                ctx.SaveChanges();
                return model;
            }
        }

    }
}
