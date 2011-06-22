using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Models.Feeds;

namespace Gnosis.Alexandria.Repositories.Feeds
{
    public class SearchByKeyword
        : EntitySearchBase<IFeed>
    {
        public SearchByKeyword()
            : base(GetWhereClause(), GetJoinClause(), feed => feed.Authors, feed => feed.PublishedDate, feed => feed.Title)
        {
        }

        public static string GetWhereClause()
        {
            var whereClause = new StringBuilder();
            whereClause.Append("Feed.Title like @pattern");
            whereClause.Append(" or Feed.Authors like @pattern");
            whereClause.Append(" or Feed.Contributors like @pattern");
            whereClause.Append(" or Feed.Description like @pattern");
            whereClause.Append(" or fc.Name like @pattern");
            whereClause.Append(" or fm.Content like @pattern or fm.Name like @pattern");
            whereClause.Append(" or fi.Title like @pattern or fi.Authors like @pattern or fi.Contributors like @pattern or fi.Summary like @pattern");
            whereClause.Append(" or fic.Name like @pattern");
            whereClause.Append(" or fim.Content like @pattern or fim.Name like @pattern");
            whereClause.Append(" or (fth.Scheme = @schemeDM and fth.Value = @patternDM) or (fth.Scheme = @schemeNH and fth.Value = @patternNH)");
            whereClause.Append(" or (fah.Scheme = @schemeDM and fah.Value = @patternDM) or (fah.Scheme = @schemeNH and fah.Value = @patternNH)");
            whereClause.Append(" or (fch.Scheme = @schemeDM and fch.Value = @patternDM) or (fch.Scheme = @schemeNH and fch.Value = @patternNH)");
            whereClause.Append(" or (fdh.Scheme = @schemeDM and fdh.Value = @patternDM) or (fdh.Scheme = @schemeNH and fdh.Value = @patternNH)");
            whereClause.Append(" or (fith.Scheme = @schemeDM and fith.Value = @patternDM) or (fith.Scheme = @schemeNH and fith.Value = @patternNH)");
            whereClause.Append(" or (fiah.Scheme = @schemeDM and fiah.Value = @patternDM) or (fiah.Scheme = @schemeNH and fiah.Value = @patternNH)");
            whereClause.Append(" or (fich.Scheme = @schemeDM and fich.Value = @patternDM) or (fich.Scheme = @schemeNH and fich.Value = @patternNH)");
            whereClause.Append(" or (fish.Scheme = @schemeDM and fish.Value = @patternDM) or (fish.Scheme = @schemeNH and fish.Value = @patternNH)");
            return whereClause.ToString();
        }

        public static string GetJoinClause()
        {
            var joinClause = new StringBuilder();
            joinClause.Append("left outer join Feed_Categories fc on Feed.Id = fc.Parent");
            joinClause.Append(" left outer join Feed_Links fl on Feed.Id = fl.Parent");
            joinClause.Append(" left outer join Feed_Metadata fm on Feed.Id = fm.Parent");
            joinClause.Append(" left outer join FeedItem fi on Feed.Id = fi.Parent");
            joinClause.Append(" left outer join FeedItem_Categories fic on fi.Id = fic.Parent");
            joinClause.Append(" left outer join FeedItem_Links fil on fi.Id = fil.Parent");
            joinClause.Append(" left outer join FeedItem_Metadata fim on fi.Id = fim.Parent");
            joinClause.Append(" left outer join Feed_TitleHashCodes fth on Feed.Id = fth.Parent");
            joinClause.Append(" left outer join Feed_AuthorHashCodes fah on Feed.Id = fah.Parent");
            joinClause.Append(" left outer join Feed_ContributorHashCodes fch on Feed.Id = fch.Parent");
            joinClause.Append(" left outer join Feed_DescriptionHashCodes fdh on Feed.Id = fdh.Parent");
            joinClause.Append(" left outer join FeedItem_TitleHashCodes fith on fi.Id = fith.Parent");
            joinClause.Append(" left outer join FeedItem_AuthorHashCodes fiah on fi.Id = fiah.Parent");
            joinClause.Append(" left outer join FeedItem_ContributorHashCodes fich on fi.Id = fich.Parent");
            joinClause.Append(" left outer join FeedItem_SummaryHashCodes fish on fi.Id = fish.Parent");
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
