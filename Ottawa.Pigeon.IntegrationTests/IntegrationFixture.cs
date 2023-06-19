using Ottawa.Pigeon.Infrastructure.Persistence;

using MediatR;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace Ottawa.Pigeon.IntegrationTests
{
    public class IntegrationFixture : IDisposable
    {
        private static WebApplicationFactory<Program> _factory = null!;
        private static IServiceScopeFactory _scopeFactory = null!;

        public IntegrationFixture()
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        public  async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }

        public  async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
       where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<OttawaPigeonDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public  async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<OttawaPigeonDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public  async Task<int> CountAsync<TEntity>() where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<OttawaPigeonDbContext>();

            return await context.Set<TEntity>().CountAsync();
        }

        public void AuthenticateCurrentUser(int userId)
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

            context.HttpContext = new DefaultHttpContext();
            context.HttpContext.Items["UserId"] = userId;
        }

        public void Dispose()
        {
           GC.SuppressFinalize(this);
        }
    }
}
