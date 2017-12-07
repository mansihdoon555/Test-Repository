using System.Collections.Generic;
using System.Data.Common;
using _729solutions.Entities.Base;

namespace _729solutions.DataAccess.Base
{
    public interface IDataMapper
    {
        void Insert(Entity entity, DbTransaction transaction);
        void Insert(Entity entity);
        void Delete(Entity entity);
        void Delete(Entity entity, DbTransaction transaction);
        void Update(Entity entity, DbTransaction transaction);
        void Update(Entity entity);
        Entity Find(int id);
        List<Entity> Find(Entity entity);
        List<Entity> Find();
    }
}
