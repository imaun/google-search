using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ImanN.GoogleSearch;

namespace GoogleSearch.Tests;

public class Tests
{
    private IGoogleSearchClient _googleClient;
    
    [SetUp]
    public void Setup()
    {
        _googleClient = new GoogleSearchClient();
    }

    [Test]
    public async Task google_search_for_microsoft_is_always_return_official_microsoft_website()
    {
        var request = new GoogleSearchRequest("microsoft");
        var results = await _googleClient.SearchAsync(request);
        var firstResult = results.FirstOrDefault();
        Assert.That(firstResult.Url.ToLower() == "https://microsoft.com");
    }
}