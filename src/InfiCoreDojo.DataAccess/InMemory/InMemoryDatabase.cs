using System;
using System.Collections.Generic;
using System.Linq;
using InfiCoreDojo.Domain;

namespace InfiCoreDojo.DataAccess.InMemory
{
    // Should be bootstrapped as singleton
    public class InMemoryDatabase
    {
        private static IList<Level> _levels = new List<Level>();
        private static IList<Player> _players = new List<Player>();

        // An "instance" of this class just publicly exposes the static
        // fields with levels and players.
        public ICollection<Level> Levels => _levels;
        public ICollection<Player> Players => _players;

        // Non lazy non-threadsafe(!) singleton, for demo purposes.
        public static InMemoryDatabase Instance => new InMemoryDatabase();

        // This is a Static Constructor, which will run just once on
        // each application run, filling the private static fields
        // with some seed data.
        static InMemoryDatabase()
        {
            // Seed some mock data...

            var level1 = new Level
            {
                Id = Guid.NewGuid(),
                IsStartingLevel = true,
                LevelName = "Hallway",
                Description = "A gloomy hallway, you see two doors. What do you do?",
            };

            var level2 = new Level
            {
                Id = Guid.NewGuid(),
                LevelName = "Red Room",
                Description = "You've moved into the red room. The exit disappears. You're stuck now!",
            };

            var level3 = new Level
            {
                Id = Guid.NewGuid(),
                LevelName = "Blue Room",
                Description = "You feel all blue in this depressing room. Nothing to see here...",
            };

            level1.Answers.Add(new Answer { Id = Guid.NewGuid(), TargetLevelId = level2.Id, Description = "Red door" });
            level1.Answers.Add(new Answer { Id = Guid.NewGuid(), TargetLevelId = level3.Id, Description = "Blue door" });

            level3.Answers.Add(new Answer { Id = Guid.NewGuid(), TargetLevelId = level1.Id, Description = "Back to Hallway" });

            _levels.Add(level1);
            _levels.Add(level2);
            _levels.Add(level3);

            _players.Add(new Player(Guid.NewGuid(), "jane", level1.Id));
            _players.Add(new Player(Guid.NewGuid(), "john", level1.Id));
            _players.Add(new Player(Guid.NewGuid(), "marc", level1.Id));
        }

        private InMemoryDatabase()
        {
        }
    }
}
