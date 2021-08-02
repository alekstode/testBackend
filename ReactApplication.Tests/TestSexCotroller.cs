using BLL.Interface.Dto;
using BLL.Interface.Exception;
using BLL.Interface.Interface;
using DAL.Interface;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using BLL.Interface;
using BLL.Local.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactApplication.Controllers;

namespace BLL.Local.Controllers
{
    [TestFixture]
    public class TestSexController
    {
        private SexController Controller;

        [SetUp]
        public void Setup()
        {
            var mock = new Mock<IComplexProvider>();

            mock.Setup(lp => lp.Sex).Returns(new StubSexService());
            mock.Setup(lp => lp.Set<SexDto>()).Returns(new StubSexService());
            mock.Setup(lp => lp.Set<SexDto, int>()).Returns(new StubSexService());

            Controller = new SexController(mock.Object);
        }

        #region Load List
        [Test]
        public void GetAll()
        {
            var female = new SexDto()
            {
                id = 1,
                name = "Женский",
                code = "female",
                description = ""
            };
            var male = new SexDto()
            {
                id = 2,
                name = "Мужской",
                code = "male",
                description = ""
            };
            var neededList = new List<SexDto>() { female, male };

            var resultList = Controller.Get();

            Assert.IsTrue(neededList.SequenceEqual((IEnumerable<SexDto>)resultList));
        }
        #endregion

        #region Load One Item
        [Test]
        public void GetOne_Unknown()
        {
            var id = 10;

            var result = Controller.Get(id) as ObjectResult;

            Assert.AreEqual(result?.StatusCode, StatusCodes.Status404NotFound);
        }

        [Test]
        public void GetOne_Female()
        {
            var neededFemaleSex = new SexDto()
            {
                id = 1,
                name = "Женский",
                code = "female",
                description = ""
            };

            var resultFemaleSex = Controller.Get(neededFemaleSex.id);

            Assert.AreEqual(neededFemaleSex, resultFemaleSex);
        }

        [Test]
        public void GetOne_Male()
        {
            var neededMaleSex = new SexDto()
            {
                id = 2,
                name = "Мужской",
                code = "male",
                description = ""
            };

            var resultMaleSex = Controller.Get(neededMaleSex.id);

            Assert.AreEqual(neededMaleSex, resultMaleSex);
        }
        #endregion

        #region Add
        [Test]
        public void Add_WithId()
        {
            var newSex = new SexDto()
            {
                id = 1,
                name = "Женский",
                code = "female",
                description = ""
            };

            var result = Controller.Post(newSex) as ObjectResult;

            Assert.AreEqual(result?.StatusCode, StatusCodes.Status400BadRequest);
        }

        [Test]
        public void Add_Existing()
        {
            var newSex = new SexDto()
            {
                id = 0,
                name = "Женский",
                code = "female",
                description = ""
            };
            var result = Controller.Post(newSex) as ObjectResult;

            Assert.AreEqual(result?.StatusCode, (int)HttpStatusCode.Conflict);
        }

        [Test]
        public void Add_New()
        {
            var neededId = 3;
            var newSex = new SexDto()
            {
                id = 0,
                name = "Не определился",
                code = "undef",
                description = "Только ради фейсбука"
            };

            var result = (SexDto)Controller.Post(newSex);

            Assert.AreEqual(neededId, result.id);
            Assert.AreEqual(newSex, result);
        }
        #endregion

        #region Update
        [Test]
        public void Update_Unknown()
        {
            var updateUnknownSex = new SexDto()
            {
                id = 10,
                name = "Неизвестно",
                code = "unknown",
                description = "Пол не установлен"
            };

            var result = Controller.Put(updateUnknownSex) as ObjectResult;

            Assert.AreEqual(result?.StatusCode, StatusCodes.Status404NotFound);
        }


        [Test]
        public void Update_Female()
        {
            var updateFemaleSex = new SexDto()
            {
                id = 1,
                name = "Женский 2",
                code = "female",
                description = "Описание женского пола"
            };

            var result = Controller.Put(updateFemaleSex);

            Assert.AreEqual(updateFemaleSex, result);
        }
        #endregion

        #region Remove
        [Test]
        public void RemoveById_Unknown()
        {
            var id = 10;

            var result = Controller.Delete(id) as ObjectResult;

            Assert.AreEqual(result?.StatusCode, StatusCodes.Status404NotFound);
        }

        [Test]
        public void RemoveById_Female()
        {
            var id = 1;

            Assert.DoesNotThrow(() => Controller.Delete(id));
        }
        #endregion
    }
}
