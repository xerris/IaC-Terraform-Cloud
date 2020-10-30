using DevOps.Api.Models;
using Xerris.DotNet.Core.Validations;

namespace DevOps.Api.Test
{
    public static class AssertionExtensions
    {
        public static bool Matches(this BlogPost actual, BlogPost expected)
        {
            Validate.Begin()
                    .IsNotNull(actual, "actual").Check()
                    .IsNotNull(expected, "expected").Check()
                    .IsEqual(actual.Author, expected.Author, nameof(BlogPost.Author))
                    .IsEqual(actual.Article, expected.Article, nameof(BlogPost.Article))
                    .IsEqual(actual.PostedDate, expected.PostedDate, nameof(BlogPost.PostedDate))
                    .Check();
            return true;
        }
    }
}