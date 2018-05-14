using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiCoreDojo.Domain
{
    public class Level : IWithId
    {
        public Guid Id { get; set; }
        public bool IsStartingLevel { get; set; }
        public string LevelName { get; set; }
        public string Description { get; set; }
        public ICollection<Answer> Answers { get; private set; } = new List<Answer>();
    }
}
