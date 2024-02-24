using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi;

namespace WebApiTestsIntegration;

[CollectionDefinition("HttpServerCollection")]
public class TestCollection : ICollectionFixture<WebApplicationFactory<IApiMarker>>{ }
