using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebApiTestsIntegration;

public class PostCustomerTests_RealWorld : IClassFixture<CustomerApiFactory>
{
    private readonly CustomerApiFactory _appFactory;
    private readonly HttpClient _httpClient;

    
    public PostCustomerTests_RealWorld(CustomerApiFactory appFactory)
    {
        _appFactory = appFactory;
        _httpClient = appFactory.CreateClient();
    }
}
