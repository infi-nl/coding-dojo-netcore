using System;
using System.Collections.Generic;
using System.Linq;
using InfiCoreDojo.Domain;

namespace InfiCoreDojo.DataAccess.JsonFiles
{
    public class JsonFilePlayerDal : JsonFileDal<Player>, IPlayerDal
    {
        public JsonFilePlayerDal() : base(@"players.json") {}

        public Player Find(Guid id)
        {
            return Query().FirstOrDefault(i => i.Id == id);
        }

        public Player FindByName(string name)
        {
            return Query().FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
