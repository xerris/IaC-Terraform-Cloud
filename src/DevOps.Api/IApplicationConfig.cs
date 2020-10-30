using Amazon.Extensions.NETCore.Setup;
using Xerris.DotNet.Core;
using Xerris.DotNet.Core.Aws.Secrets;

namespace DevOps.Api
{
    public interface IApplicationConfig
    {
        string BlogPostTableName { get; }
        SecretConfigCollection SecretConfigurations { get; }
    }

    public class ApplicationConfig : IApplicationConfig, IApplicationConfigBase
    {
        public SecretConfigCollection SecretConfigurations { get; set; }
        public AWSOptions AwsOptions { get; set; }
        public string BlogPostTableName { get; set; }
    }
}