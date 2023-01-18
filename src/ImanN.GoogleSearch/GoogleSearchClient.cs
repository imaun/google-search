using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace ImanN.GoogleSearch
{
    
    /// <inheritdoc /> 
    public class GoogleSearchClient : IGoogleSearchClient
    {
        private const string _searchUrl = "https://google.com/search?q={0}&start={1}";
        private const string _mainClassName = "g";
        private const string _titleDiv = ".//a";
        
        public async Task<IReadOnlyCollection<GoogleSearchResultItem>> SearchAsync(GoogleSearchRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var searchUrl = string.Format(_searchUrl, HttpUtility.UrlEncode(request.Query), request.Start);
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(searchUrl);
            var rows = doc.GetElementsWithClass(_mainClassName);
            var results = new List<GoogleSearchResultItem>();
            
            if (rows is null || !rows.Any()) return results;

            int order = 0;
            foreach(var row in rows)
            {
                var title = row.SelectSingleNode(_titleDiv);
                var desc = row.ChildNodes[0]?.ChildNodes[0].ChildNodes[1].InnerText;

                if (title != null)
                {
                    results.Add(new GoogleSearchResultItem
                    {
                        Order = order,
                        Title = string.IsNullOrWhiteSpace(title?.InnerText) 
                            ? string.Empty 
                            : HttpUtility.HtmlDecode(title?.InnerText.Trim()),
                        Description = string.IsNullOrWhiteSpace(desc) 
                            ? string.Empty 
                            : HttpUtility.HtmlDecode(desc.Trim()),
                        Url = (title?.GetAttributeValue("href", string.Empty) ?? string.Empty)?.TrimStart('.')
                    });
                }

                order++;
            }

            return await Task.FromResult(results);
        }
    }    
}
