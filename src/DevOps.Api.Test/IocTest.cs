using DevOps.Api.Repository;
using DevOps.Api.Service;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xerris.DotNet.Core;
using Xunit;

namespace DevOps.Api.Test
{
    public class IocTest
    {
        public IocTest()
        {
            var collection = new ServiceCollection();
            new AppStartup().StartUp(collection);
        }
        
        [Fact]
        public void ApplicationConfig()
        {
           var appConfig = Has<IApplicationConfig>();
           appConfig.BlogPostTableName.Should().Be("MyBlogPosts");
        }

        [Fact]
        public void BlogService()
        {
            Has<IBlogService>();
        }

        [Fact]
        public void BlogRepository()
        {
            Has<IBlogRepository>();
        }

        [Fact]
        public void HealthService()
        {
            Has<IHealthCheckService>();
        }

        private static T Has<T>()
        {
           var found =  IoC.Resolve<T>();
           found.Should().NotBeNull();
           return found;
        }
    }
}