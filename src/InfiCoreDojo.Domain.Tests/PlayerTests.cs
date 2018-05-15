using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace InfiCoreDojo.Domain.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void Tests_are_working()
        {
            Assert.True(9001 > 42);
        }

        [Fact]
        public void MoveTo_throws_when_targeting_current_level()
        {
            var player = new Player(Guid.NewGuid(), "mary", Guid.NewGuid());
            Assert.Throws<InvalidOperationException>(() => player.MoveTo(player.CurrentLevelId));
        }

        [Fact]
        public void MoveTo_updates_current_level()
        {
            var targetLevelId = Guid.NewGuid();
            var player = new Player(Guid.NewGuid(), "mary", Guid.NewGuid());

            player.MoveTo(targetLevelId);

            Assert.Equal(targetLevelId, player.CurrentLevelId);
        }

    }
}
