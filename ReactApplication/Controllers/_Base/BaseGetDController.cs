using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ReactApplication.Controllers.Base
{
    public class BaseGetDController<Dto, KeyType> : ControllerBase
        where Dto : IBaseDto, IEntityWithId<KeyType>, new()
    {
        private IComplexProvider _db = null;
        protected IComplexProvider db
        {
            get
            {
                if (_db == null)
                    throw new NullReferenceException();
                return _db;
            }
            set { _db = value; }
        }

        protected void UseService(Type dtoType)
        {
            _db.UseOneService(dtoType);
        }

        public BaseGetDController(IComplexProvider provider)
        {
            db = provider;
        }

        protected virtual void BeforeAddOrUpdate(Dto item) { }
        protected virtual void AfterAddOrUpdate(Dto item) { }

        [HttpGet]
        [Route("Delete")]
        public object Delete(KeyType id)
        {
            db.Set<Dto, KeyType>().RemoveById(id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}
