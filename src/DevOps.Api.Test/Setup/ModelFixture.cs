using System;
using Xerris.DotNet.Core.Extensions;
using Xerris.DotNet.TestAutomation.Factory;

namespace DevOps.Api.Test.Setup
{
    public class ModelFixture : IDisposable
    {
        public ModelFixture()
        {
            new ClearFactory().Then(new ModelFactory()).Run();
        }
        
        public void Dispose()
        {
            FactoryGirl.Clear();
        }
    }
}