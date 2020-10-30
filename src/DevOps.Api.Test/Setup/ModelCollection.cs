using Xunit;

namespace DevOps.Api.Test.Setup
{
    [CollectionDefinition("Test Models")]
    public class ModelCollection : ICollectionFixture<ModelFixture>
    {
    }
}