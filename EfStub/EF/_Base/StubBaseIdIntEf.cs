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
    public class StubBaseIdIntEf<Entity> : StubBaseEf<Entity, int>
        where Entity : IEntityWithId<int>
    {

        #region CUD

        protected override int GetNextKey()
        {
            return TheWholeEntities.Select(x => x.id).Max() + 1;
        }
        #endregion
    }
}
