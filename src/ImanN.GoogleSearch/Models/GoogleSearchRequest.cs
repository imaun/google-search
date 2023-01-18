using System;

namespace ImanN.GoogleSearch.Models
{
    public class GoogleSearchRequest
    {
        public GoogleSearchRequest(
            string query, int start = 0)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));
            
            Query = query;
            Start = start;
        }
        
        public string Query { get; set; }
        public int Start { get; set; }
    }
}