using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiCoreDojo.Domain
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid TargetLevelId { get; set; }
    }
}
