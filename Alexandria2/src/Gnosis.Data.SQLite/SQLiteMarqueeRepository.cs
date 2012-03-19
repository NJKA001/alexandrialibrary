using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Gnosis.Metadata;

namespace Gnosis.Data.SQLite
{
    public class SQLiteMarqueeRepository
        : SQLiteRepositoryBase, IMarqueeRepository
    {
        public SQLiteMarqueeRepository(ILogger logger)
            : base(logger)
        {
        }

        public SQLiteMarqueeRepository(ILogger logger, IDbConnection defaultConnection)
            : base(logger, defaultConnection)
        {
        }

        private IMarquee ReadRecord(IDataRecord record, MetadataCategory category)
        {
            var location = record.GetUri("Location");
            var name = record.GetString("Name");
            var fromDate = record.GetDateTime("FromDate");
            var toDate = record.GetDateTime("ToDate");
            
            var subtitle = new StringBuilder(string.Empty);
            if (fromDate != DateTime.MinValue && fromDate != DateTime.MaxValue)
                subtitle.Append(fromDate.ToShortDateString());
            if (toDate != DateTime.MinValue && toDate != DateTime.MaxValue)
            {
                if (subtitle.Length > 0)
                    subtitle.Append(" - ");

                subtitle.Append(toDate.ToShortDateString());
            }

            return new Marquee(location, category, name, subtitle.ToString());
        }

        public IMarqueePage GetMarqueePage(MetadataCategory category, int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
                throw new ArgumentException("pageIndex cannot be less than 0");
            if (pageSize < 1)
                throw new ArgumentException("pageSize cannot be less than 1");

            var offset = pageIndex * pageSize;

            var countBuilder = new CommandBuilder();
            countBuilder.AppendFormat("select count(Location) from {0};", category);
            var numberOfPages = 1;
            var scalar = ExecuteScalar(countBuilder);
            if (scalar != null)
            {
                var result = 0;
                if (int.TryParse(scalar.ToString(), out result) && result > 0)
                {
                    numberOfPages = (int)Math.Ceiling((double)result / (double)pageSize);
                }
            }

            var builder = new CommandBuilder();
            builder.AppendFormat("select Location, Name, FromDate, ToDate from {0} order by Name limit {1}", category, pageSize);
            if (offset > 0)
                builder.AppendFormat(" offset {0};", offset);
            else
                builder.Append(";");

            var items = GetRecords<IMarquee>(builder, record => ReadRecord(record, category));

            return new MarqueePage(items, numberOfPages, pageIndex, pageSize);
        }
    }
}
