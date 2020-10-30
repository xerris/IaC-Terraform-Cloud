using System;
using DevOps.Api.Models;
using Xerris.DotNet.Core.Commands;
using Xerris.DotNet.TestAutomation.Factory;

namespace DevOps.Api.Test.Setup
{
    public class ModelFactory : ICommand
    {
        public void Run()
        {
            FactoryGirl.Define(() => new BlogPost
            {
                Author = "Albert Einstein",
                Article = "E = mc2",
                PostedDate = new DateTime(1905, 11, 21).ToString("yyyy-MM-dd")
            });
        }
    }
}