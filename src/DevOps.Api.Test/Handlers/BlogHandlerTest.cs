using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using DevOps.Api.Handlers;
using DevOps.Api.Models;
using DevOps.Api.Service;
using Moq;
using Xerris.DotNet.Core.Extensions;
using Xerris.DotNet.Core.Validations;
using Xerris.DotNet.TestAutomation;
using Xerris.DotNet.TestAutomation.Factory;
using Xunit;

namespace DevOps.Api.Test.Handlers
{
    [Collection("Test Models")]
    public class BlogHandlerTest : MockBase
    {
        private readonly Mock<IBlogService> blogService;
        private readonly BlogHandler handler;

        public BlogHandlerTest()
        {
            blogService = Strict<IBlogService>();

            handler = new BlogHandler(blogService.Object);
        }

        [Fact]
        public async Task PostBLogPost()
        {
            var blogPost = FactoryGirl.Build<BlogPost>();

            blogService.Setup(x => x.PostBlog(It.Is<BlogPost>(x => x.Matches(blogPost))))
                       .ReturnsAsync(blogPost);

            var request = new APIGatewayProxyRequest {Body = blogPost.ToJson()};
            
            var response = await handler.PostBlog(request);
            Validate.Begin()
                    .IsNotNull(response, "response").Check()
                    .IsNotEmpty(response.Body, nameof(response.Body)).Check()
                    .IsNotNull(response.Body.FromJson<BlogPost>(), "blogPost")
                    .Check();
            
        }

        [Fact]
        public async Task GetAllPosts()
        {
            var results = new[] {FactoryGirl.Build<BlogPost>(), FactoryGirl.Build<BlogPost>()};
            blogService.Setup(x => x.GetAllPosts())
                       .ReturnsAsync(results);
            
            var response = await handler.GetAllBlogPosts(new APIGatewayProxyRequest());

            Validate.Begin()
                .IsNotNull(response, "response").Check();

            var items = response.Body.FromJson<IEnumerable<BlogPost>>();
            
            Validate.Begin()    
                .HasExactly(items, 2, "has 2 BlogPosts")
                .Check();
        }
    }
}