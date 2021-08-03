using System;
using System.Collections.Generic;
using System.Text;
using BLL.Interface.Dto;
using BLL.Interface.Interface;
using BLL.Local.Services.Base;
using DAL.Interface;

namespace BLL.Local.Services
{
    public class LocalStudentService : LocalBaseCrudService<StudentDto, int>, IStudentService
    {
        public LocalStudentService(IUnitOfWork uow) : base(uow) { }

        #region CUD
        // Используем проверку на уникальность в этом сервисе
        protected override bool UseUniqueValidation => true;

        protected override string ValidateAdd(StudentDto item)
        {
            return ValidateCommonAddUpdate(item);
        }

        protected override string ValidateUpdate(StudentDto item)
        {
            return ValidateCommonAddUpdate(item);
        }

        private string ValidateCommonAddUpdate(StudentDto item)
        {
            var errors = new List<string>();
            var sex = db.Sex.GetOneById(item.idSex);
            if (sex == null)
            {
                var errorNumber = errors.Count + 1;
                errors.Add(errorNumber + ". idSex is out of range");
            }

            if (item.idAcademicPerformance.HasValue)
            {
                var score = db.AcademicPerformance.GetOneById(item.idAcademicPerformance.Value);
                if (score == null)
                {
                    var errorNumber = errors.Count + 1;
                    errors.Add(errorNumber + ". idAcademicPerformance is out of range");
                }
            }

            return string.Join("\n", errors);
        }
        #endregion
    }
}
