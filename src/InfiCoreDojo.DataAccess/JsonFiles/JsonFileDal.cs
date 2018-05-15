using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InfiCoreDojo.Domain;
using Newtonsoft.Json;

namespace InfiCoreDojo.DataAccess.JsonFiles
{
    // TODO: Fix scoping, concurrency, and thread-safety issues.
    //
    // The current implementation will have problems and weird
    // bugs regardless of whether it's registered as a singleton
    // or as a scoped (per request) dependency.
    //
    // Turns out writing a database is hard :-)
    //
    // Finding and fixing these issues is left as an excercise for
    // the (advanced) reader.

    public abstract class JsonFileDal<T> where T : class, IWithId
    {
        private readonly string _fileName;
        private readonly string _filePath;
        private readonly List<T> _data;

        protected JsonFileDal(string fileName)
        {
            _fileName = fileName;
            _filePath = Path.Combine("c:/temp", _fileName); // Or maybe "/var/tmp/dojo" 

            if (!File.Exists(_filePath))
            {
                SeedDatabaseFilesFromEmbeddedResources();                
            }

            var json = File.ReadAllText(_filePath);
            _data = JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }

        public IQueryable<T> Query()
        {
            return _data.AsQueryable();
        }

        public T Get(Guid id)
        {
            return _data.FirstOrDefault(i => i.Id == id)
                   ?? throw new KeyNotFoundException($"Item {id} does not exist");
        }

        public void Upsert(T item)
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
            var level = _data.FirstOrDefault(l => l.Id == id);
            if (level == null) return;

            _data.Remove(level);
            Save();
        }

        public void Delete(T item)
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
            var assembly = this.GetType().Assembly;
            var resourceName = "InfiCoreDojo.DataAccess.Data." + _fileName;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                File.WriteAllText(_filePath, json);
            }
        }
    }
}
