using DevOps.Api.Models;
using Xerris.DotNet.Core.Extensions;
using Xerris.DotNet.Core.Validations;
using Xerris.DotNet.TestAutomation.Factory;
using Xunit;

namespace DevOps.Api.Test.Models
{
    [Collection("Test Models")]
    public class SerializationTests
    {
        [Fact]
        public void BlogPost()
        {
            var post = FactoryGirl.Build<BlogPost>();
            var json = post.ToJson();

            var fromJson = json.FromJson<BlogPost>();

            Validate.Begin()
                .IsNotNull(fromJson, "fromJson").Check()
                .IsEqual(post.Author, fromJson.Author, nameof(Api.Models.BlogPost.Author))
                .IsEqual(post.Article, fromJson.Article, nameof(Api.Models.BlogPost.Article))
                .IsEqual(post.PostedDate, fromJson.PostedDate, nameof(Api.Models.BlogPost.PostedDate))
                .Check();

        }
    }
}