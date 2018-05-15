using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiCoreDojo.Domain
{
    public class Player : IWithId
    {
        public Guid Id { get; }
        public string Name { get; }
        public Guid CurrentLevelId { get; set; }

        public Player(Guid id, string name, Guid currentLevelId)
        {
            Id = id;
            Name = name;
            CurrentLevelId = currentLevelId;
        }

        // Just an example "domain" method to make the entity a little less 
        // anemic, and at least a little interesting to unit test.
        public void MoveTo(Guid targetLevelId)
        {
            if (this.CurrentLevelId == targetLevelId)
            {
                throw new InvalidOperationException("Cannot move to the level you're already in!");
            }

            this.CurrentLevelId = targetLevelId;
        }
    }
}
