using System;
using System.Collections.Generic;
using System.Text;
using BLL.Interface;
using BLL.Interface.Exception;
using DAL.Interface;

namespace BLL.Local.Services.Base
{
    public class LocalBaseCrudService<Dto, KeyType> : ICrudService<Dto, KeyType>
        where Dto : IBaseDto, IEntityWithId<KeyType>, new()
    {
        protected IUnitOfWork db { get; private set; }

        #region Ctor
        protected LocalBaseCrudService(IUnitOfWork uow)
        {
            SetUnitOfWork(uow);
        }

        protected void SetUnitOfWork(IUnitOfWork uow)
        {
            db = uow;
        }
        #endregion

        #region Load Data
        public virtual List<Dto> Items()
        {
            return db.Set<Dto>().Items();
        }

        public virtual Dto GetOneById(KeyType id)
        {
            var item = db.Set<Dto, KeyType>().GetOneById(id);
            if(item == null)
            {
                throw MakeNullReferenceWithId(id);
            }
            return item;
        }
        #endregion

        #region CUD
        protected virtual void BeforeAdd(Dto item) { }
        protected virtual string ValidateAdd(Dto item) { return string.Empty; }
        protected virtual bool UseUniqueValidation() { return false; }

        public virtual Dto Add(Dto item)
        {
            if (!item.IsNew())
            {
                throw MakeInvalidOperationException(item.id);
            }
            if (UseUniqueValidation())
            {
                var hasSameItem = db.Set<Dto>().HasSameItem(item);
                if (hasSameItem)
                {
                    throw MakeNonUniqueException();
                }
            }
            var validationMessage = ValidateAdd(item);
            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                throw MakeValidationException(validationMessage);
            }
            BeforeAdd(item);
            return db.Set<Dto>().Add(item);
        }

        protected virtual void BeforeUpdate(Dto item) { }
        protected virtual string ValidateUpdate(Dto item) { return string.Empty; }
        public virtual Dto Update(Dto item)
        {
            var itemToUpdate = db.Set<Dto, KeyType>().GetOneById(item.id);
            if (itemToUpdate == null)
            {
                throw MakeNullReferenceWithItem(item);
            }
            var validationMessage = ValidateUpdate(item);
            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                throw MakeValidationException(validationMessage);
            }
            BeforeUpdate(item);
            return db.Set<Dto>().Update(item);
        }

        protected virtual void BeforeDelete(KeyType id) { }
        public virtual void RemoveById(KeyType id)
        {
            var itemToRemove = db.Set<Dto, KeyType>().GetOneById(id);
            if (itemToRemove == null)
            {
                throw MakeNullReferenceWithId(id);
            }
            BeforeDelete(id);
            db.Set<Dto, KeyType>().RemoveById(id);
        }

        #endregion

        #region Exceptions 
        private NullReferenceException MakeNullReferenceWithItem(Dto item)
        {
            var id = item.id;
            return MakeNullReferenceWithId(id);
        }
        private NullReferenceException MakeNullReferenceWithId(KeyType id)
        {
            var message = MakeMessageNullReferenceWithId(id);
            return new NullReferenceException(message);
        }

        private string MakeMessageNullReferenceWithId(KeyType id)
        {
            var typeName = typeof(Dto).Name;
            return $"{typeName} with id = {id} not found";
        }

        private InvalidOperationException MakeInvalidOperationException(KeyType id)
        {
            var typeName = typeof(Dto).Name;
            var message = $"This operation is invalid for provided {typeName}";
            return new InvalidOperationException(message);
        }

        protected CustomValidationException MakeValidationException(string message)
        {
            var typeName = typeof(Dto).Name;
            var msg = $"Validation error for {typeName}. {message}";
            return new CustomValidationException(msg);
        }

        protected NonUniqueException MakeNonUniqueException()
        {
            var typeName = typeof(Dto).Name;
            var msg = $"{typeName} with same fields are already exists";
            return new NonUniqueException(msg);
        }
        #endregion

    }
}
