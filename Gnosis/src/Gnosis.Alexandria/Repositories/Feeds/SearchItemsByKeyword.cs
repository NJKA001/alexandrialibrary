using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchItemsByKeyword
        : EntitySearchBase<IFeedItem>
    {
        public SearchItemsByKeyword()
            : base(GetWhereClause(), GetJoinClause(), feedItem => feedItem.Authors, feedItem => feedItem.PublishedDate, feedItem => feedItem.Title)
        {
        }

        private static string GetWhereClause()
        {
            var whereClause = new StringBuilder();
            whereClause.Append("FeedItem.Title like @pattern");
            whereClause.Append(" or FeedItem.Authors like @pattern");
            whereClause.Append(" or FeedItem.Contributors like @pattern");
            whereClause.Append(" or FeedItem.Summary like @pattern");
            whereClause.Append(" or fic.Name like @pattern");
            whereClause.Append(" or fim.Content like @pattern or fim.Name like @pattern");
            whereClause.Append(" or (fith.Scheme = @schemeDM and fith.Value like @patternDM) or (fith.Scheme = @schemeNH and fith.Value like @patternNH)");
            whereClause.Append(" or (fiah.Scheme = @schemeDM and fiah.Value like @patternDM) or (fiah.Scheme = @schemeNH and fiah.Value like @patternNH)");
            whereClause.Append(" or (fich.Scheme = @schemeDM and fich.Value like @patternDM) or (fich.Scheme = @schemeNH and fich.Value like @patternNH)");
            whereClause.Append(" or (fish.Scheme = @schemeDM and fish.Value like @patternDM) or (fish.Scheme = @schemeNH and fish.Value like @patternNH)");
            return whereClause.ToString();
        }

        private static string GetJoinClause()
        {
            var joinClause = new StringBuilder();
            joinClause.Append("left outer join FeedItem_Categories fic on FeedItem.Id = fic.Parent");
            joinClause.Append(" left outer join FeedItem_Links fil on FeedItem.Id = fil.Parent");
            joinClause.Append(" left outer join FeedItem_Metadata fim on FeedItem.Id = fim.Parent");
            joinClause.Append(" left outer join FeedItem_TitleHashCodes fith on FeedItem.Id = fith.Parent");
            joinClause.Append(" left outer join FeedItem_AuthorHashCodes fiah on FeedItem.Id = fiah.Parent");
            joinClause.Append(" left outer join FeedItem_ContributorHashCodes fich on FeedItem.Id = fich.Parent");
            joinClause.Append(" left outer join FeedItem_SummaryHashCodes fish on FeedItem.Id = fish.Parent");
            return joinClause.ToString();
        }

        public IFilter GetFilter(string keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");

            var parameters = new Dictionary<string, object>();
            parameters.Add("@pattern", '%' + keyword + '%');
            parameters.Add("@patternDM", '%' + keyword.AsDoubleMetaphone() + '%');
            parameters.Add("@patternNH", '%' + keyword.AsNameHash() + '%');
            parameters.Add("@schemeDM", HashCode.SchemeDoubleMetaphone.ToString());
            parameters.Add("@schemeNH", HashCode.SchemeNameHash.ToString());

            return GetFilter(parameters);
        }
    }
}
