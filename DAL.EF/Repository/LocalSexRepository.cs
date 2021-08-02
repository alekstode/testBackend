using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BLL.Interface.Dto;
using BLL.Interface.Interface;
using DAL.EF.Dto;
using DAL.EF.EF.Entities;
using DAL.EF.Repository.Base;
using DAL.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
    public class LocalSexRepository :
        EfCrudRepository<Sex, SexDto, LocalSexDto, int>,
        ISexRepository
    {
        public override bool HasSameItem(SexDto dto)
        {
            return TheWholeEntities.Any(x =>
                   x.code.ToLower() == dto.code.ToLower()
                && x.description.ToLower() == dto.description.ToLower()
                && x.name.ToLower() == dto.name.ToLower()
            );
        }
        public override SexDto GetOneById(int id)
        {
            var item = TheWholeEntities
                .FirstOrDefault(x => x.id == id);
            return new LocalSexDto().ConvertToDto(item);
        }

        public override List<SexDto> Items()
        {
            return TheWholeEntities
                .Select(x => new LocalSexDto().ConvertToDto(x)).ToList();
        }
    }
}
