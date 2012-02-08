using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;
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
            whereClause.Append(" or (fitt.Scheme = @schemeDM and fitt.Value like @patternDM) or (fitt.Scheme = @schemeNH and fitt.Value like @patternNH)");
            whereClause.Append(" or (fiat.Scheme = @schemeDM and fiat.Value like @patternDM) or (fiat.Scheme = @schemeNH and fiat.Value like @patternNH)");
            whereClause.Append(" or (fict.Scheme = @schemeDM and fict.Value like @patternDM) or (fict.Scheme = @schemeNH and fict.Value like @patternNH)");
            whereClause.Append(" or (fist.Scheme = @schemeDM and fist.Value like @patternDM) or (fist.Scheme = @schemeNH and fist.Value like @patternNH)");
            return whereClause.ToString();
        }

        private static string GetJoinClause()
        {
            var joinClause = new StringBuilder();
            joinClause.Append("left outer join FeedItem_Categories fic on FeedItem.Id = fic.Parent");
            joinClause.Append(" left outer join FeedItem_Links fil on FeedItem.Id = fil.Parent");
            joinClause.Append(" left outer join FeedItem_Metadata fim on FeedItem.Id = fim.Parent");
            joinClause.Append(" left outer join FeedItem_TitleTags fitt on FeedItem.Id = fitt.Parent");
            joinClause.Append(" left outer join FeedItem_AuthorTags fiat on FeedItem.Id = fiat.Parent");
            joinClause.Append(" left outer join FeedItem_ContributorTags fict on FeedItem.Id = fict.Parent");
            joinClause.Append(" left outer join FeedItem_SummaryTags fist on FeedItem.Id = fist.Parent");
            return joinClause.ToString();
        }

        public IFilter GetFilter(string keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");

            var parameters = new Dictionary<string, object>();
            parameters.Add("@pattern", '%' + keyword + '%');
            parameters.Add("@patternDM", '%' + keyword.ToDoubleMetaphoneString() + '%');
            parameters.Add("@patternNH", '%' + keyword.ToAmericanizedString() + '%');
            parameters.Add("@schemeDM", Models.Tag.SchemeDoubleMetaphone.ToString());
            parameters.Add("@schemeNH", Models.Tag.SchemeAmericanizedGraph.ToString());

            return GetFilter(parameters);
        }
    }
}
