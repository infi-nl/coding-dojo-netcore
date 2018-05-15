using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InfiCoreDojo.Domain;
using Newtonsoft.Json;

namespace InfiCoreDojo.DataAccess.JsonFiles
{
    public class JsonFilePlayerDal : IPlayerDal
    {
        private readonly string _fileName;
        private readonly string _filePath;
        private readonly List<Player> _data;

        public JsonFilePlayerDal()
        {
            _fileName = "players.json";
            _filePath = Path.Combine("c:/temp", _fileName); // Or maybe "/var/tmp/dojo" 

            if (!File.Exists(_filePath))
            {
                SeedDatabaseFilesFromEmbeddedResources();
            }

            var json = File.ReadAllText(_filePath);
            _data = JsonConvert.DeserializeObject<List<Player>>(json) ?? new List<Player>();
        }

        public IQueryable<Player> Query()
        {
            return _data.AsQueryable();
        }

        public Player Find(Guid id)
        {
            return Query().FirstOrDefault(i => i.Id == id);
        }

        public Player FindByName(string name)
        {
            return Query().FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }

        public Player Get(Guid id)
        {
            return _data.FirstOrDefault(i => i.Id == id) ?? throw new KeyNotFoundException($"Item {id} does not exist");
        }

        public void Upsert(Player item)
        {
            var existingItem = _data.FirstOrDefault(i => i.Id == item.Id);

            if (existingItem != null)
            {
                _data.Remove(existingItem);
            }

            _data.Add(item);
            Save();
        }

        public void Delete(Guid id)
        {
            var player = _data.FirstOrDefault(l => l.Id == id);
            if (player == null) return;

            _data.Remove(player);
            Save();
        }

        public void Delete(Player item)
        {
            Delete(item.Id);
        }

        private void Save()
        {
            var json = JsonConvert.SerializeObject(_data);
            File.WriteAllText(_filePath, json);
        }

        protected void SeedDatabaseFilesFromEmbeddedResources()
        {
            // Retrieve embedded resources...
            var assembly = this.GetType().Assembly;
            var resourceName = "InfiCoreDojo.DataAccess.Data." + _fileName;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                // Read json from embedded resources...
                var json = reader.ReadToEnd();

                // Write the seed data to a class instance variable `_filePath`
                File.WriteAllText(_filePath, json);
            }
        }
    }
}
