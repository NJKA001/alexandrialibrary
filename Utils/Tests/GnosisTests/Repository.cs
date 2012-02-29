using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using GnosisTests.Entities;

namespace GnosisTests
{
    public class Repository<T>
        where T : IEntity
    {
        public Repository(ISerializer<T> serializer)
        {
            if (serializer == null)
                throw new ArgumentNullException("serializer");

            this.type = typeof(T).Name;
            this.cache = new EntityCache<T>();
            this.serializer = serializer;
        }

        private readonly string type;
        private readonly EntityCache<T> cache;
        private readonly ISerializer<T> serializer;

        private const string createdLogFormat = "{0}-Created.txt";
        private const string deletedLogFormat = "{0}-Deleted.txt";
        private const string updatedLogFormat = "{0}-Updated.txt";

        private void ProcessCreatedLog()
        {
            var createdLog = string.Format(createdLogFormat, type);

            if (File.Exists(createdLog))
            {
                using (var reader = new StreamReader(createdLog))
                {
                    var line = string.Empty;
                    while (line != null)
                    {
                        line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            var data = line.Split(new string[] { "\t" }, StringSplitOptions.None);
                            if (data == null || data.Length < 3)
                                continue;

                            var entity = serializer.Deserialize(data);
                            cache.Add(entity);
                        }
                    }
                }
            }
        }

        private void ProcessDeletedLog()
        {
            var deletedLog = string.Format(deletedLogFormat, type);

            if (File.Exists(deletedLog))
            {
                using (var reader = new StreamReader(deletedLog))
                {
                    var line = string.Empty;
                    while (line != null)
                    {
                        line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            var id = uint.Parse(line.Trim());
                            cache.Remove(id);
                        }
                    }
                }
            }
        }

        public void Initialize()
        {
            ProcessCreatedLog();
            
        }

        public void Add(T entity)
        {
            cache.Add(entity);
        }

        public void Remove(uint id)
        {
            cache.Remove(id);
        }
    }
}
