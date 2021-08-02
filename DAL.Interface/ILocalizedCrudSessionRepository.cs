using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interface
{
    public interface ILocalizedCrudSessionRepository<T, KeyType> : ILocalizedCrudRepository<T, KeyType>, ISessionRepository
        where T: IEntityWithId<KeyType>
    {
    }
}
