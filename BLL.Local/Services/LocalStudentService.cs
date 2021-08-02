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
        #endregion
    }
}
