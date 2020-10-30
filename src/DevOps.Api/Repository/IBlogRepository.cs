using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using DevOps.Api.Models;
using Xerris.DotNet.Core.Aws.IoC;
using Xerris.DotNet.Core.Aws.Repositories.DynamoDb;

namespace DevOps.Api.Repository
{
    public interface IBlogRepository
    {
        Task<BlogPost> Save(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllPosts();
        Task<BlogPost> FindById(string blogId);
    }

    public class BlogRepository : BaseRepository<BlogPost>, IBlogRepository
    {
        public BlogRepository(IApplicationConfig config, ILazyProvider<IAmazonDynamoDB> clientProvider)
            : base(clientProvider, config.BlogPostTableName)
        {
        }

        public async Task<BlogPost> Save(BlogPost blogPost)
        {
            blogPost.Id = Guid.NewGuid().ToString("N");
            await SaveAsync(blogPost);
            return blogPost;
        }

        public Task<IEnumerable<BlogPost>> GetAllPosts()
        {
            var all = FindAllAsync<BlogPost>();
            return all;
        }

        public async Task<BlogPost> FindById(string blogId)
        {
            var found = await base.FindById(blogId);
            return found;
        }
    }
}