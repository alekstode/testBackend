using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IBaseDto
    {
        bool IsNew();
    }
    public abstract class DtoInt : IEntityWithId<int>, IBaseDto
    {
        public int id { get; set; }
        public bool IsNew()
        {
            return id == 0;
        }
    }
}
