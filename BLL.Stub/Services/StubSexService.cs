using BLL.Interface.Dto;
using BLL.Interface.Interface;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Interface.Exception;

namespace BLL.Local.Services
{
    public class StubSexService : ISexService
    {
        private List<SexDto> TheWholeEntities;

        #region Init test Data
        public StubSexService()
        {
            var sexFemale = new SexDto()
            {
                id = 1,
                code = "female",
                name = "Женский",
                description = ""
            };
            var sexMale = new SexDto()
            {
                id = 2,
                code = "male",
                name = "Мужской",
                description = ""
            };
            TheWholeEntities = new List<SexDto>() { sexFemale, sexMale };
        }
        #endregion

        #region Load Data
        public SexDto GetOneById(int id)
        {
            var item = TheWholeEntities.FirstOrDefault(x=>x.id == id);
            if (item == null)
            {
                throw MakeNullReferenceWithId(id);
            }
            return item;
        }

        public List<SexDto> Items()
        {
            return TheWholeEntities;
        }
        #endregion

        #region CUD

        protected virtual string ValidateAdd(SexDto item) { return string.Empty; }
        protected virtual bool UseUniqueValidation() { return true; }

        public virtual SexDto Add(SexDto item)
        {
            if (!item.IsNew())
            {
                throw MakeInvalidOperationException(item.id);
            }
            if (UseUniqueValidation())
            {
                var hasSameItem = HasSameItem(item);
                if (hasSameItem)
                {
                    throw new NonUniqueException();
                }
            }
            var validationMessage = ValidateAdd(item);
            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                throw MakeValidationException(validationMessage);
            }

            var id = TheWholeEntities.Select(x => x.id).Max() + 1;
            item.id = id;
            TheWholeEntities.Add(item);
            return item;
        }

        protected virtual string ValidateUpdate(SexDto item) { return string.Empty; }
        public SexDto Update(SexDto item)
        {
            var itemToUpdate = TheWholeEntities.FirstOrDefault(x=>x.id == item.id);
            if (itemToUpdate == null)
            {
                throw MakeNullReferenceWithItem(item);
            }
            var validationMessage = ValidateUpdate(item);
            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                throw MakeValidationException(validationMessage);
            }

            var index = TheWholeEntities.IndexOf(itemToUpdate);
            TheWholeEntities[index] = item;
            return item;
        }

        public void RemoveById(int id)
        {
            var itemToRemove = TheWholeEntities.FirstOrDefault(x=>x.id == id);
            if (itemToRemove == null)
            {
                throw MakeNullReferenceWithId(id);
            }
            TheWholeEntities.Remove(itemToRemove);
        }

        public bool HasSameItem(SexDto dto)
        {
            return TheWholeEntities.Any(x =>
                x.code.ToLower() == dto.code.ToLower()
                && x.description.ToLower() == dto.description.ToLower()
                && x.name.ToLower() == dto.name.ToLower()
            );
        }
        #endregion


        #region Exceptions 
        private NullReferenceException MakeNullReferenceWithItem(SexDto item)
        {
            var id = item.id;
            return MakeNullReferenceWithId(id);
        }
        private NullReferenceException MakeNullReferenceWithId(int id)
        {
            var message = MakeMessageNullReferenceWithId(id);
            return new NullReferenceException(message);
        }

        private string MakeMessageNullReferenceWithId(int id)
        {
            var typeName = typeof(SexDto).Name;
            return $"{typeName} with id = {id} not found";
        }

        private InvalidOperationException MakeInvalidOperationException(int id)
        {
            var typeName = typeof(SexDto).Name;
            var message = $"This operation is invalid for provided {typeName}";
            return new InvalidOperationException(message);
        }

        protected CustomValidationException MakeValidationException(string message)
        {
            var typeName = typeof(SexDto).Name;
            var msg = $"Validation error for {typeName}. {message}";
            return new CustomValidationException(msg);
        }

        protected NonUniqueException MakeNonUniqueException()
        {
            var typeName = typeof(SexDto).Name;
            var msg = $"{typeName} with same fields are already exists";
            return new NonUniqueException(msg);
        }
        #endregion

    }
}
