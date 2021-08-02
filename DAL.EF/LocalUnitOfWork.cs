using System;
using System.Collections.Generic;
using System.Text;
using BLL.Interface;
using DAL.EF.Repository;
using DAL.Interface;

namespace DAL.EF
{
    // Координатор уровня репозитория
    public partial class LocalUnitOfWork : IUnitOfWork
    {
        public bool IsInited
        {
            get { return false; }
        }

        public LocalUnitOfWork()
        {
            CustomUnit();
        }

        public ICrudRepository<Dto, KeyType> Set<Dto, KeyType>() where Dto : IBaseDto, IEntityWithId<KeyType>
        {

            var cu = CustomUnit<Dto, KeyType>();
            if (cu != null)
                return cu;

            throw new NotImplementedException();
        }
        public ICrudRepository<Dto> Set<Dto>() where Dto : IBaseDto
        {
            var cu = CustomUnit<Dto>();
            if (cu != null)
                return cu;

            throw new NotImplementedException();
        }
    }
}
