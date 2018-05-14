using System;
using System.Collections.Generic;
using System.Linq;
using InfiCoreDojo.Api.Controllers;
using InfiCoreDojo.Api.DTO;
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

        [Fact]
        public void Restart_WhenPlayerNotFound_ReturnsFailure()
        {
            Level fakeLevel = CreateFakeLevelInDalMock();
            Player fakePlayer = CreateFakePlayerInDalMock(fakeLevel);
            var controller = new PlayerController(playerDalMock.Object, levelDalMock.Object);

            var command = new Reset { PlayerName = "unknown-name" };
            var result = controller.Restart(command);

            Assert.False(result.Success);
        }

        [Fact]
        public void Restart_WhenPlayerFound_ReturnsSuccess()
        {
            Level fakeLevel = CreateFakeLevelInDalMock();
            Player fakePlayer = CreateFakePlayerInDalMock(fakeLevel);
            var controller = new PlayerController(playerDalMock.Object, levelDalMock.Object);

            var command = new Reset { PlayerName = fakePlayer.Name };
            var result = controller.Restart(command);

            Assert.True(result.Success);
        }

        [Fact]
        public void Restart_WhenPlayerFound_SavesPlayer()
        {
            // Whether you like this kind of test is in part a matter of
            // taste. It's here anyways, for demo purposes...

            Level fakeLevel = CreateFakeLevelInDalMock();
            Player fakePlayer = CreateFakePlayerInDalMock(fakeLevel);
            var controller = new PlayerController(playerDalMock.Object, levelDalMock.Object);

            var command = new Reset { PlayerName = fakePlayer.Name };
            var result = controller.Restart(command);

            // Verify that the DAL's "Upsert" was called with some
            // player having the fakePlayer's Id. Without better and
            // stronger assertions this is merely a weak smoke test.
            playerDalMock.Verify(dal => dal.Upsert(It.Is<Player>(p => p.Id == fakePlayer.Id)));
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
            var fakeLevel = new Level { Id = Guid.NewGuid(), IsStartingLevel = true };

            levelDalMock
                .Setup(dal => dal.Get(fakeLevel.Id))
                .Returns(fakeLevel);

            var fakeLevelsList = new Level[] { fakeLevel };

            levelDalMock
                .Setup(dal => dal.Query())
                .Returns(fakeLevelsList.AsQueryable());

            return fakeLevel;
        }
    }
}
