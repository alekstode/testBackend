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

namespace DAL.EF.Repository
{
    public class StubBaseRepository<Dto, KeyType> where Dto: IEntityWithId<KeyType>
    {
        protected List<Dto> TheWholeEntities;

        #region Init test Data
        public StubBaseRepository()
        {
            InitData();
        }

        protected virtual void InitData() { throw new NotImplementedException(); }
        #endregion

        #region Load Data
        public Dto GetOneById(KeyType id)
        {
            var item = TheWholeEntities
                .FirstOrDefault(x => x.id.Equals(id));
            return item;
        }

        public List<Dto> Items()
        {
            return TheWholeEntities;
        }
        #endregion

        #region CUD
        public Dto Add(Dto dto)
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

        public Dto Update(Dto dto)
        {
            var old = TheWholeEntities.First(x => x.id.Equals(dto.id));
            var index = TheWholeEntities.IndexOf(old);

            TheWholeEntities[index] = dto;

            return dto;
        }

        public void RemoveById(KeyType id)
        {
            var old = TheWholeEntities.First(x => x.id.Equals(id));
            TheWholeEntities.Remove(old);
        }

        public virtual bool HasSameItem(Dto dto)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
