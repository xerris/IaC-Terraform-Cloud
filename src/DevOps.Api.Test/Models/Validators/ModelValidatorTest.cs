using System;
using DevOps.Api.Models;
using DevOps.Api.Models.Validators;
using FluentAssertions;
using Xerris.DotNet.Core.Validations;
using Xerris.DotNet.TestAutomation.Factory;
using Xunit;

namespace DevOps.Api.Test.Models.Validators
{
    [Collection("Test Models")]
    public class ModelValidatorTest
    {
        private readonly ModelValidator validator;

        public ModelValidatorTest()
        {
            validator = new ModelValidator();
        }
        
        [Fact]
        public void ValidBlogPost()
        {
            validator.ValidateBlogPost(FactoryGirl.Build<BlogPost>());
        }

        [Fact]
        public void NoAuthor()
        {
            Invalid(FactoryGirl.Build<BlogPost>(x => x.Author = string.Empty));
            Invalid(FactoryGirl.Build<BlogPost>(x => x.Author = null));
        }

        [Fact]
        public void NoArticle()
        {
            Invalid(FactoryGirl.Build<BlogPost>(x => x.Article = string.Empty));
            Invalid(FactoryGirl.Build<BlogPost>(x => x.Article = null));
        }

        [Fact]
        public void BadPostedDate()
        {
            Invalid(FactoryGirl.Build<BlogPost>(x => x.PostedDate = string.Empty));
            Invalid(FactoryGirl.Build<BlogPost>(x => x.PostedDate = null));
        }

        private void Invalid(BlogPost badBlogPost)
        {
            Action act = () => validator.ValidateBlogPost(badBlogPost);
            act.Should().Throw<ValidationException>();
        }
    }
}