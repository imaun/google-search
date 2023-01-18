using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImanN.GoogleSearch
{
    /// <summary>
    /// GoogleSearchClient uses HtmlAgilityPack to crawl Google search results for a specific phrase.
    /// more info : https://github.com/imaun/google-search
    /// </summary>
    public interface IGoogleSearchClient
    {

        /// <summary>
        /// Send search request to Google with the specified <see cref="GoogleSearchRequest"/>.
        /// </summary>
        /// <param name="request">
        ///     Request includes the query and starting index of the search results.
        /// </param>
        /// <returns>
        ///     The crawled links with Title and Description.
        /// </returns>
        Task<IReadOnlyCollection<GoogleSearchResultItem>> SearchAsync(GoogleSearchRequest request);
    }
}