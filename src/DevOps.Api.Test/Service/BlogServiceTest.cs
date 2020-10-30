using System.Threading.Tasks;
using DevOps.Api.Models;
using DevOps.Api.Models.Validators;
using DevOps.Api.Repository;
using DevOps.Api.Service;
using Moq;
using Xerris.DotNet.Core.Validations;
using Xerris.DotNet.TestAutomation;
using Xerris.DotNet.TestAutomation.Factory;
using Xunit;

namespace DevOps.Api.Test.Service
{
    [Collection("Test Models")]
    public class BlogServiceTest : MockBase
    {
        private Mock<IBlogRepository> repository;
        private Mock<IModelValidator> validator;
        private BlogService service;

        public BlogServiceTest()
        {
            repository = Strict<IBlogRepository>();
            validator = Strict<IModelValidator>();
            service = new BlogService(repository.Object, validator.Object);
        }

        [Fact]
        public async Task SavePost()
        {
            var blogPost = FactoryGirl.Build<BlogPost>();

            validator.Setup(x => x.ValidateBlogPost(blogPost));
            repository.Setup(x => x.Save(blogPost))
                      .ReturnsAsync(blogPost);

            await service.PostBlog(blogPost);
        }

        [Fact]
        public async Task GetAllPosts()
        {
            var blogPosts = new[] {FactoryGirl.Build<BlogPost>(), FactoryGirl.Build<BlogPost>()};
            repository.Setup(x => x.GetAllPosts()).ReturnsAsync(blogPosts);

            var posts = await service.GetAllPosts();
            Validate.Begin()
                    .IsNotNull(posts, "posts").Check()
                    .HasExactly(posts, 2, "has 2 poss")
                    .Check();
        }
    }
}