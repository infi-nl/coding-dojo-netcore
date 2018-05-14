using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfiCoreDojo.Domain;

namespace InfiCoreDojo.DataAccess.InMemory
{
    public class InMemoryPlayerDal : IPlayerDal
    {
        public InMemoryPlayerDal()
        {
        }

        public Player Get(Guid id)
        {
            return this.Find(id) ?? throw new KeyNotFoundException();
        }

        public Player Find(Guid id)
        {
            return InMemoryDatabase.Instance.Players.FirstOrDefault(i => i.Id == id);
        }

        public Player FindByName(string name)
        {
            return InMemoryDatabase.Instance.Players.FirstOrDefault(i => i.Name == name);
        }

        public Player GetByName(string name)
        {
            return InMemoryDatabase.Instance.Players.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.InvariantCultureIgnoreCase))
                ?? throw new KeyNotFoundException();
        }

        public IQueryable<Player> Query()
        {
            return InMemoryDatabase.Instance.Players.AsQueryable();
        }

        public void Upsert(Player player)
        {
            // TODO: Improve this
            var oldPlayer = this.Find(player.Id);
            if (oldPlayer != null) InMemoryDatabase.Instance.Players.Remove(oldPlayer);
            InMemoryDatabase.Instance.Players.Add(player);
        }
    }

}
