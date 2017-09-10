using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAL
{
    public class GameDAL
    {
        public Game Add(Game model)
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                var m = ctx.Games.Add(new Game()
                {
                    Name = model.Name,
                    Password = model.Password,
                    Time = model.Time,
                    Room = model.Room,
                    Description = model.Description
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
                var ic = ctx.Games.Where(x => x.Id == id).FirstOrDefault();
                ctx.Games.Remove(ic);
                ctx.SaveChanges();
            }
        }

        public Game Get(int GameId)
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                return ctx.Games.Where(x => x.Id == GameId).FirstOrDefault();
            }
        }

        public List<Game> GetAll()
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                return ctx.Games.ToList();
            }
        }

        public Game Update(Game model)
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                var ic = ctx.Games.Where(x => x.Id == model.Id).FirstOrDefault();
                ic.Name = model.Name;
                ic.Password = model.Password;
                ic.Room = model.Room;
                ic.Description = model.Description;
                ctx.SaveChanges();
                return model;
            }
        }
    }
}
