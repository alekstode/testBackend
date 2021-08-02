using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interface
{
    public interface ICrudRepository<Dto>
    {
        List<Dto> Items();
        Dto Add(Dto dto);
        Dto Update(Dto dto);
        bool HasSameItem(Dto dto);
    }
    public interface ICrudRepository<Dto, KeyType>: ICrudRepository<Dto>
        where Dto: IEntityWithId<KeyType>
    {
        Dto GetOneById(KeyType id);
        void RemoveById(KeyType id);

    }
}
