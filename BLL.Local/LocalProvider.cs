using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BLL.Interface;
//using BLL.Interface.Dto;
//using BLL.Interface.Interface;
using BLL.Local.Services;
using BLL.Local.Services.Base;
using DAL.EF;
using DAL.Interface;

namespace BLL.Local
{
    public partial class LocalProvider : IComplexProvider
    {
        private Dictionary<Type, object> AllServices;
       
        private IUnitOfWork uow;

        public LocalProvider()
        {
            AllServices = new Dictionary<Type, object>();
            uow = new LocalUnitOfWork();
        }

        public void UseOneService(Type dtoType)
        {
            UseCustomServices(dtoType);
        }

        public void UseAllServices()
        {
            throw new Exception();
        }

        public ICrudService<Dto> Set<Dto>() where Dto : IBaseDto
        {
            var type = typeof(Dto);
            if (!AllServices.ContainsKey(type))
                throw new NotImplementedException();

            return (ICrudService<Dto>)AllServices[type];
        }
        public ICrudService<Dto, KeyType> Set<Dto, KeyType>() where Dto : IBaseDto, IEntityWithId<KeyType>
        {
            var type = typeof(Dto);
            if (!AllServices.ContainsKey(type))
                throw new NotImplementedException();

            return AllServices[type] as ICrudService<Dto, KeyType>;
        }
    }
}
