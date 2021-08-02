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
    public class LocalStudentRepository :
        EfCrudRepository<Student, StudentDto, LocalStudentDto, int>,
        IStudentRepository
    {

        // Переопределяем из-за настроек энтити
        public override StudentDto GetOneById(int id)
        {
            var item = TheWholeEntities
                .Include(x => x.idSexNavigation)
                .Include(x => x.idAcademicPerformanceNavigation)
                .FirstOrDefault(x => x.id == id);
            return new LocalStudentDto().ConvertToDto(item);
        }

        // Переопределяем из-за настроек энтити
        public override List<StudentDto> Items()
        {
            return TheWholeEntities
                .Include(x => x.idSexNavigation)
                .Include(x => x.idAcademicPerformanceNavigation)
                .Select(x => new LocalStudentDto().ConvertToDto(x)).ToList();
        }

        // Переопределяем проверку на уникальность
        public override bool HasSameItem(StudentDto dto)
        {
            return TheWholeEntities.Any(x =>
                   x.surName.ToLower() == dto.surName.ToLower()
                && x.firstName.ToLower() == dto.firstName.ToLower()
                && x.secondName.ToLower() == dto.secondName.ToLower()
                && x.dob == dto.dob
            );
        }
    }
}
