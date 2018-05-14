using System;
using System.Collections.Generic;
using System.Linq;
using InfiCoreDojo.Domain;

namespace InfiCoreDojo.DataAccess
{
    public interface IPlayerDal
    {
        // Exposing an IQueryable<T> is often considered an anti-pattern
        // and can have unexpected consequences. It is used here merely
        // because it serves us well for a demo application.
        IQueryable<Player> Query();

        Player Find(Guid id);
        Player Get(Guid id);
        Player FindByName(string name);
        void Upsert(Player player);
    }
}
