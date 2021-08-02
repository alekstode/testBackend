using DAL.EF.EF.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EfStub.EF
{
    public class StubSexEf : StubBaseIdIntEf<Sex>
    {
        #region Init test Data
        protected override void InitData()
        {
            var sexFemale = new Sex()
            {
                id = 1,
                code = "female",
                name = "Женский",
                description = ""
            };
            var sexMale = new Sex()
            {
                id = 2,
                code = "male",
                name = "Мужской",
                description = ""
            };
            TheWholeEntities = new List<Sex>() { sexFemale, sexMale };
        }
        #endregion

        #region CUD
        public override bool HasSameItem(Sex dto)
        {
            return TheWholeEntities.Any(x =>
                   x.code.ToLower() == dto.code.ToLower()
                && x.description.ToLower() == dto.description.ToLower()
                && x.name.ToLower() == dto.name.ToLower()
            );
        }

        #endregion
    }
}
