using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interface
{
    public interface ILocalizedCrudRepository<Dto, KeyType>: ICrudRepository<Dto, KeyType>
        where Dto: IEntityWithId<KeyType>
    {
        List<Dto> LangItems(int? langId);
    }
}
