using Mikv.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests
{
    public class HealthzTest
    {
        private readonly Mock<ILogger<HealthzController>> logger = new Mock<ILogger<HealthzController>>();
        private readonly HealthzController c;

        public HealthzTest()
        {
            // create the controller
            c = new HealthzController(logger.Object);
        }

        [Fact]
        public void GetHealthz()
        {
            // call the controller
            var res = (ObjectResult)c.Healthz();

            // assert 200 OK
            OkObjectResult ok = res as OkObjectResult;
            Assert.NotNull(ok);

            // assert the value matches the expected value
            Assert.Equal(AssertValues.HealthzResult, ok.Value.ToString());
        }
    }
}
