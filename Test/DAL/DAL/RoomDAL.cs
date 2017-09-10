using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAL
{
    public class RoomDAL
    {
        public Room Add(Room model)
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                var m = ctx.Rooms.Add(new Room()
                {
                    Name = model.Name,
                    Password = model.Password,
                    CreatorId = model.CreatorId
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
                var ic = ctx.Rooms.Where(x => x.Id == id).FirstOrDefault();
                ctx.Rooms.Remove(ic);
                ctx.SaveChanges();
            }
        }

        public Room Get(int RoomId)
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                return ctx.Rooms.Where(x => x.Id == RoomId).FirstOrDefault();
            }
        }

        public List<Room> GetAll()
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                return ctx.Rooms.ToList();
            }
        }

        public Room Update(Room model)
        {
            using (GameTestEntities ctx = new GameTestEntities())
            {
                var ic = ctx.Rooms.Where(x => x.Id == model.Id).FirstOrDefault();
                ic.CreatorId = model.CreatorId;
                ic.Name = model.Name;
                ic.Password = model.Password;
                ctx.SaveChanges();
                return model;
            }
        }
    }
}
