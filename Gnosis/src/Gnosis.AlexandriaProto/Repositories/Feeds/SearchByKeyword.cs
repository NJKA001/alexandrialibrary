using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Data;
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
            whereClause.Append(" or (ftt.Scheme = @schemeDM and ftt.Value like @patternDM) or (ftt.Scheme = @schemeNH and ftt.Value like @patternNH)");
            whereClause.Append(" or (fat.Scheme = @schemeDM and fat.Value like @patternDM) or (fat.Scheme = @schemeNH and fat.Value like @patternNH)");
            whereClause.Append(" or (fct.Scheme = @schemeDM and fct.Value like @patternDM) or (fct.Scheme = @schemeNH and fct.Value like @patternNH)");
            whereClause.Append(" or (fdt.Scheme = @schemeDM and fdt.Value like @patternDM) or (fdt.Scheme = @schemeNH and fdt.Value like @patternNH)");
            return whereClause.ToString();
        }

        public static string GetJoinClause()
        {
            var joinClause = new StringBuilder();
            joinClause.Append("left outer join Feed_Categories fc on Feed.Id = fc.Parent");
            joinClause.Append(" left outer join Feed_Links fl on Feed.Id = fl.Parent");
            joinClause.Append(" left outer join Feed_Metadata fm on Feed.Id = fm.Parent");
            joinClause.Append(" left outer join Feed_TitleTags ftt on Feed.Id = ftt.Parent");
            joinClause.Append(" left outer join Feed_AuthorTags fat on Feed.Id = fat.Parent");
            joinClause.Append(" left outer join Feed_ContributorTags fct on Feed.Id = fct.Parent");
            joinClause.Append(" left outer join Feed_DescriptionTags fdt on Feed.Id = fdt.Parent");
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
