using Buku_Absen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buku_Absen.Repository.Interface
{
    public interface IRepository<Entity, Key> where Entity:class
    {
        IEnumerable<Entity> Get();

        Entity Get(Key key);
        int Insert(Entity entity);
        int update(Entity entity, Key key);
        int Delete(Key key);
    }
}
