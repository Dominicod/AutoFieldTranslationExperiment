using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Web.Test;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly IApplicationDbContext _context;
    protected readonly HttpClient _client;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        // Fetch and assign scopes for use in integration tests
        _scope = factory.Services.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
}
