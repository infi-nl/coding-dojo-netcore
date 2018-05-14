using System;
using System.Collections.Generic;
using System.Linq;
using InfiCoreDojo.Domain;

namespace InfiCoreDojo.DataAccess
{
    public interface ILevelDal
    {
        // Exposing an IQueryable<T> is often considered an anti-pattern
        // and can have unexpected consequences. It is used here merely
        // because it serves us well for a demo application.
        IQueryable<Level> Query();

        Level Get(Guid id);
        void Delete(Guid id);
        void Delete(Level level);
        void Upsert(Level level);
    }
}