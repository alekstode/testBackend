using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Interface;
using DAL.EF.EF.Context.Session;
using DAL.EF.EF.Entities;
using DAL.Interface;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository.Base
{
    public class EfCrudLocalizedRepository<Entity, LocalizedEntity, Dto, LocalLocalizedDto> : EfCrudRepository<Entity, Dto, LocalLocalizedDto>, ILocalizedCrudRepository<Dto>
        where Dto : IDto
        where LocalizedEntity : class, ILocalizedEntity
        where LocalLocalizedDto : ILocalLocalizedDto<Entity, Dto, LocalizedEntity>, new()
        where Entity : class, IEntityWithId
    {
        
        //public override List<Dto> Items()
        //{
        //    return ItemsByLanguage();
        //}
        //public virtual List<Dto> ItemsByLanguage(int languageId = -1)
        //{
        //    if (languageId == -1)
        //        return TheWholeEntities.Select(x => new LocalLocalizedDto().ConvertToLocalizedDto(x, null)).ToList();

        //    var temp2 = (from c in TheWholeEntities
        //                 join lng in db.Set<LocalizedEntity>().Where(x => x.IdLanguage == languageId) on c.Id equals lng.IdEntity into left
        //                 from lng in left.DefaultIfEmpty()
        //                 select new LocalLocalizedDto().ConvertToLocalizedDto(c, lng)).ToList();
        //    return temp2;

        //}

        public List<Dto> LangItems(int? langId)
        {
            if (langId == null)
                return TheWholeEntities.Select(x => new LocalLocalizedDto().ConvertToLocalizedDto(x, null)).ToList();

            var temp2 = (from c in TheWholeEntities
                join lng in db.Set<LocalizedEntity>().Where(x => x.IdLanguage == langId) on c.Id equals lng.IdEntity into left
                from lng in left.DefaultIfEmpty()
                select new LocalLocalizedDto().ConvertToLocalizedDto(c, lng)).ToList();
            return temp2;
        }
    }

    public interface ILocalizedDictionary : ILocalizedEntity
    {
        string Name { get; set; }
        string Description { get; set; }
    }

    public interface ILocalizedEntity : IEntityWithId
    {
        int IdEntity { get; }
        int IdLanguage { get; }
    }
}
