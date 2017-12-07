using System.Collections.Generic;

namespace _729solutions.DataAccess.Base
{
    public interface ICacheableDataMapper : IDataMapper
    {
        bool LoadedFromDB { get; set; }
        void UpdateCache(List<Entities.Base.Entity> list);
    }
}
