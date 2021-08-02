﻿using System;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using PL.API.Open.Controllers.Base;

namespace PL.API.Open.Controllers
{
    [Route("[controller]")]
    public class BaseCRUDApiController<Dto, KeyType> : BaseCUDApiController<Dto, KeyType>
        where Dto : IBaseDto, IEntityWithId<KeyType>, new()
    {
        public BaseCRUDApiController(IComplexProvider unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns>Returns a list of records</returns>
        [HttpGet]
        public object Get()
        {
            var data = db.Set<Dto>().Items();
            return data;
        }

        /// <summary>
        /// Get one record by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a single record</returns>
        /// <response code="404">Record not found</response>    
        [HttpGet("{id}", Name = "Get")]
        public virtual object Get(KeyType id)
        {
            try
            {
                var dto = db.Set<Dto, KeyType>().GetOneById(id);
                return dto;
            }
            catch (NullReferenceException e)
            {
                return NotFound(e.Message);
            }
        }


    }
}