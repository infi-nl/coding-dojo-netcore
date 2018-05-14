using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiCoreDojo.Domain
{
    public interface IWithId
    {
        Guid Id { get; }
    }
}
