using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfiCoreDojo.Domain;

namespace InfiCoreDojo.DataAccess.InMemory
{
    public class InMemoryPlayerDal : IPlayerDal
    {
        private readonly InMemoryDatabase _database;

        public InMemoryPlayerDal(InMemoryDatabase database)
        {
            this._database = database;
        }

        public Player Get(Guid id)
        {
            return this.Find(id) ?? throw new KeyNotFoundException();
        }

        public Player Find(Guid id)
        {
            return this._database.Players.FirstOrDefault(i => i.Id == id);
        }

        public Player FindByName(string name)
        {
            return this._database.Players.FirstOrDefault(i => i.Name == name);
        }

        public Player GetByName(string name)
        {
            return this._database.Players.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.InvariantCultureIgnoreCase))
                ?? throw new KeyNotFoundException();
        }

        public IQueryable<Player> Query()
        {
            return this._database.Players.AsQueryable();
        }

        public void Upsert(Player player)
        {
            // TODO: Improve this
            var oldPlayer = this.Find(player.Id);
            if (oldPlayer != null) this._database.Players.Remove(oldPlayer);
            this._database.Players.Add(player);
        }
    }

}
