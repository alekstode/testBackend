using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BLL.Interface;
using BLL.Interface.Dto;
using BLL.Interface.Interface;
using DAL.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace EfStub.EF
{
    public class StubBaseEf<Entity, KeyType> 
        where Entity : IEntityWithId<KeyType>
    {
        protected List<Entity> TheWholeEntities;

        #region Init test Data
        public StubBaseEf()
        {
            InitData();
        }

        protected virtual void InitData() { throw new NotImplementedException(); }
        #endregion

        #region Load Data
        public IQueryable<Entity> Items()
        {
            return TheWholeEntities.AsQueryable();
        }
        #endregion

        #region CUD
        public Entity Add(Entity dto)
        {
            var id = GetNextKey();
            dto.id = id;
            
            TheWholeEntities.Add(dto);

            return dto;
        }

        protected virtual KeyType GetNextKey()
        {
            throw new NotImplementedException();
        }

        public Entity Update(Entity entity)
        {
            var old = TheWholeEntities.First(x => x.id.Equals(entity.id));
            var index = TheWholeEntities.IndexOf(old);

            TheWholeEntities[index] = entity;

            return entity;
        }

        public void RemoveById(KeyType id)
        {
            var old = TheWholeEntities.First(x => x.id.Equals(id));
            TheWholeEntities.Remove(old);
        }

        public virtual bool HasSameItem(Entity entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
