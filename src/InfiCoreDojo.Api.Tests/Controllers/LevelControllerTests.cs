using System;
using System.Collections.Generic;
using System.Linq;
using InfiCoreDojo.Api.Controllers;
using InfiCoreDojo.DataAccess;
using InfiCoreDojo.Domain;
using Moq;
using Xunit;

namespace InfiCoreDojo.Api.Tests.Controllers
{
    public class LevelControllerTests
    {
        [Fact]
        public void Can_construct_default_controller()
        {
            var levelDalMock = new Mock<ILevelDal>();

            // Just testing this doesn't throw any exceptions:
            var controller = new LevelController(levelDalMock.Object);
        }

        [Fact]
        public void Put_checks_id_from_uri_with_body()
        {
            var levelDalMock = new Mock<ILevelDal>();
            var controller = new LevelController(levelDalMock.Object);

            var level = new Level { Id = Guid.NewGuid() };
            var otherGuid = Guid.NewGuid();

            var exception = Assert.Throws<InvalidOperationException>(() => controller.Put(otherGuid, level));

            // Some extra smoke tests around the exception message:
            Assert.Contains(level.Id.ToString(), exception.Message);
            Assert.Contains(otherGuid.ToString(), exception.Message);
        }
    }
}
