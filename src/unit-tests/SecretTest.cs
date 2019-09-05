using Mikv.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace UnitTests
{
    public class SecretTest
    {
        private readonly Mock<ILogger<SecretController>> logger = new Mock<ILogger<SecretController>>();
        private readonly Mock<IConfiguration> config = new Mock<IConfiguration>();
        private readonly SecretController c;

        public SecretTest()
        {
            // Mock the secret
            config.SetupGet(x => x[Mikv.Constants.KeyVaultSecretName]).Returns(AssertValues.SecretResult);

            // create the controller
            c = new SecretController(logger.Object, config.Object);
        }

        [Fact]
        public void GetSecret()
        {
            // call the controller
            var res = (ObjectResult)c.GetSecret();

            // assert return 200 OK
            OkObjectResult ok = res as OkObjectResult;
            Assert.NotNull(ok);

            // assert the secret matches the assert value
            Assert.Equal(AssertValues.SecretResult, ok.Value.ToString());
        }
    }
}
