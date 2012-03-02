using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using GnosisTests.Entities;

namespace GnosisTests
{
    public class Repository<T>
        : IRepository<T>
        where T : IEntity
    {
        public Repository(ISerializer<T> serializer)
        {
            if (serializer == null)
                throw new ArgumentNullException("serializer");

            this.type = typeof(T).Name;
            this.cache = new EntityCache<T>();
            this.serializer = serializer;
            this.createdLog = string.Format(createdLogFormat, type, string.Empty);
            this.deletedLog = string.Format(deletedLogFormat, type, string.Empty);
            this.updatedLog = string.Format(updatedLogFormat, type, string.Empty);
        }

        private readonly string type;
        private readonly EntityCache<T> cache;
        private readonly ISerializer<T> serializer;
        private readonly string createdLog;
        private readonly string updatedLog;
        private readonly string deletedLog;

        private bool isInitialized;
        private bool isCompacting;

        private const int batchSize = 50;
        private const string createdLogFormat = "{0}-Created{1}.txt";
        private const string deletedLogFormat = "{0}-Deleted{1}.txt";
        private const string updatedLogFormat = "{0}-Updated{1}.txt";
        private const string compactLogFormat = "{0}-Compact{1}.txt";

        private string[] GetData(string line)
        {
            return line.Split(new string[] { "\t" }, StringSplitOptions.None);
        }

        private string GetTimestamp()
        {
            return DateTime.Now.ToString("o").Replace(":", string.Empty).Replace(".", string.Empty);
        }

        private string GetCompactLogName(string timestamp)
        {
            return string.Format(compactLogFormat, type, timestamp);
        }

        private string GetCreatedLogBackupName(string timestamp)
        {
            return string.Format(createdLogFormat, type, timestamp);
        }

        private string GetUpdatedLogBackupName(string timestamp)
        {
            return string.Format(updatedLogFormat, type, timestamp);
        }

        private string GetDeletedLogBackupName(string timestamp)
        {
            return string.Format(deletedLogFormat, type, timestamp);
        }

        private void ProcessLog(string path, Action<string> action)
        {
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    var line = string.Empty;
                    while (line != null)
                    {
                        line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;

                        action(line);
                    }
                }
            }
        }

        private void ProcessCreatedEntity(string line)
        {
            var data = GetData(line);
            if (data == null || data.Length < 3)
                return;
            
            var entity = serializer.Deserialize(data);
            cache.Add(entity);
        }

        private void ProcessDeletedEntity(string line)
        {
            var id = uint.Parse(line.Trim());
            cache.Remove(id);
        }

        private void ProcessUpdatedEntity(string line)
        {
            var data = GetData(line);
            if (data == null || data.Length < 3)
                return;
            
            var id = uint.Parse(data[0]);
            var entity = cache.Get(id);
            if (entity == null)
                return;

            var field = data[1];
            var value = data[2] ?? string.Empty;

            serializer.ApplyUpdate(entity, field, value);
        }

        private void ProcessCreatedLog()
        {
            ProcessLog(createdLog, line => ProcessCreatedEntity(line));
        }

        private void ProcessDeletedLog()
        {
            ProcessLog(deletedLog, line => ProcessDeletedEntity(line));
        }

        private void ProcessUpdatedLog()
        {
            ProcessLog(updatedLog, line => ProcessUpdatedEntity(line));
        }

        private void LogCreate(IEnumerable<T> entities)
        {
            using (var writer = File.AppendText(createdLog))
            {
                foreach (var entity in entities)
                {
                    writer.WriteLine(serializer.Serialize(entity));
                }
                writer.Flush();
            }
        }

        private void LogDelete(IEnumerable<uint> ids)
        {
            using (var writer = File.AppendText(deletedLog))
            {
                foreach (var id in ids)
                {
                    writer.WriteLine(id);
                }
                writer.Flush();
            }
        }

        private void LogUpdate(IEnumerable<T> entities, string field, string value)
        {
            using (var writer = File.AppendText(updatedLog))
            {
                foreach (var entity in entities)
                {
                    writer.WriteLine(serializer.SerializeUpdate(entity, field, value));
                }
                writer.Flush();
            }
        }

        private void LogCompaction(string compactLog)
        {
            using (var writer = File.CreateText(compactLog))
            {
                var i = 0;
                foreach (var entity in cache.Entities)
                {
                    i++;
                    writer.WriteLine(serializer.Serialize(entity));

                    if (i % batchSize == 0)
                        writer.Flush();
                }
                writer.Flush();
            }
        }

        public IEnumerable<T> Entities
        {
            get { return cache.Entities; }
        }

        public T Get(uint id)
        {
            return cache.Get(id);
        }

        public void Initialize()
        {
            if (isCompacting)
                throw new InvalidOperationException("Repository is being compacted");

            ProcessCreatedLog();
            ProcessDeletedLog();
            ProcessUpdatedLog();

            isInitialized = true;
            isCompacting = false;
        }

        public void Create(T entity)
        {
            Create(new List<T> { entity });
        }

        public void Create(IEnumerable<T> entities)
        {
            if (!isInitialized)
                throw new InvalidOperationException("Repository is not initialized");

            if (isCompacting)
                throw new InvalidOperationException("Repository is being compacted");

            LogCreate(entities);
            cache.Add(entities);
        }

        public void Update(T entity, string field, string value)
        {
            Update(new List<T> { entity }, field, value);
        }

        public void Update(IEnumerable<T> entities, string field, string value)
        {
            if (!isInitialized)
                throw new InvalidOperationException("Repository is not initialized");

            if (isCompacting)
                throw new InvalidOperationException("Repository is being compacted");

            LogUpdate(entities, field, value);
            foreach (var entity in entities)
            {
                serializer.ApplyUpdate(entity, field, value);
            }
        }

        public void Delete(uint id)
        {
            Delete(new List<uint> { id });
        }

        public void Delete(IEnumerable<uint> ids)
        {
            if (!isInitialized)
                throw new InvalidOperationException("Repository is not initialized");

            if (isCompacting)
                throw new InvalidOperationException("Repository is being compacted");

            LogDelete(ids);
            cache.Remove(ids);
        }

        public void Compact()
        {
            if (!isInitialized)
                throw new InvalidOperationException("Repository is not initialized");

            if (isCompacting)
                throw new InvalidOperationException("Repository is being compacted");

            isCompacting = true;

            var timestamp = GetTimestamp();
            var compactLog = GetCompactLogName(timestamp);

            LogCompaction(compactLog);
            File.Replace(compactLog, createdLog, GetCreatedLogBackupName(timestamp));
            File.Move(updatedLog, GetUpdatedLogBackupName(timestamp));
            File.Move(deletedLog, GetDeletedLogBackupName(timestamp));

            isCompacting = false;
        }
    }
}
