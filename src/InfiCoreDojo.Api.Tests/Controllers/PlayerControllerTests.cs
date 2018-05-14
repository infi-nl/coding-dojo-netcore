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
    public class PlayerControllerTests
    {
        private Mock<IPlayerDal> playerDalMock;
        private Mock<ILevelDal> levelDalMock;

        public PlayerControllerTests()
        {
            playerDalMock = new Mock<IPlayerDal>();
            levelDalMock = new Mock<ILevelDal>();
        }

        [Fact]
        public void GetCurrentLevel_retrieves_level_saved_on_player()
        {
            Level fakeLevel = CreateFakeLevelInDalMock();
            Player fakePlayer = CreateFakePlayerInDalMock(fakeLevel);
            var controller = new PlayerController(playerDalMock.Object, levelDalMock.Object);

            var result = controller.GetCurrentLevel(fakePlayer.Name);

            Assert.Equal(result, fakeLevel);
        }

        private Player CreateFakePlayerInDalMock(Level currentLevel)
        {
            var fakePlayer = new Player(Guid.NewGuid(), "johndoe", currentLevel.Id);

            playerDalMock
                .Setup(dal => dal.Get(fakePlayer.Id))
                .Returns(fakePlayer);

            playerDalMock
                .Setup(dal => dal.FindByName(fakePlayer.Name))
                .Returns(fakePlayer);

            return fakePlayer;
        }

        private Level CreateFakeLevelInDalMock()
        {
            var fakeLevel = new Level { Id = Guid.NewGuid() };

            levelDalMock
                .Setup(dal => dal.Get(fakeLevel.Id))
                .Returns(fakeLevel);

            return fakeLevel;
        }
    }
}
