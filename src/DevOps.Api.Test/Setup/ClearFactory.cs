using Xerris.DotNet.Core.Commands;
using Xerris.DotNet.TestAutomation.Factory;

namespace DevOps.Api.Test.Setup
{
    public class ClearFactory : ICommand
    {
        public void Run()
        {
            FactoryGirl.Clear();
        }
    }
}