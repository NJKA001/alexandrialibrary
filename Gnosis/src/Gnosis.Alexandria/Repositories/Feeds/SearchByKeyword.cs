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
            whereClause.Append(" or (fth.Scheme = @schemeDM and fth.Value like @patternDM) or (fth.Scheme = @schemeNH and fth.Value like @patternNH)");
            whereClause.Append(" or (fah.Scheme = @schemeDM and fah.Value like @patternDM) or (fah.Scheme = @schemeNH and fah.Value like @patternNH)");
            whereClause.Append(" or (fch.Scheme = @schemeDM and fch.Value like @patternDM) or (fch.Scheme = @schemeNH and fch.Value like @patternNH)");
            whereClause.Append(" or (fdh.Scheme = @schemeDM and fdh.Value like @patternDM) or (fdh.Scheme = @schemeNH and fdh.Value like @patternNH)");
            return whereClause.ToString();
        }

        public static string GetJoinClause()
        {
            var joinClause = new StringBuilder();
            joinClause.Append("left outer join Feed_Categories fc on Feed.Id = fc.Parent");
            joinClause.Append(" left outer join Feed_Links fl on Feed.Id = fl.Parent");
            joinClause.Append(" left outer join Feed_Metadata fm on Feed.Id = fm.Parent");
            joinClause.Append(" left outer join Feed_TitleHashCodes fth on Feed.Id = fth.Parent");
            joinClause.Append(" left outer join Feed_AuthorHashCodes fah on Feed.Id = fah.Parent");
            joinClause.Append(" left outer join Feed_ContributorHashCodes fch on Feed.Id = fch.Parent");
            joinClause.Append(" left outer join Feed_DescriptionHashCodes fdh on Feed.Id = fdh.Parent");
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
