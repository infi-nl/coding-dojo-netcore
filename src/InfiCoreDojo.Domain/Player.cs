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
        // (That is, when we get to the Dojo step where we improve this method.)
        public void MoveTo(Guid targetLevelId)
        {
            this.CurrentLevelId = targetLevelId;
        }
    }
}
