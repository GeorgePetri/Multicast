using Microsoft.AspNetCore.Mvc;
using Moq;
using Multicast.Domain.Models;
using Multicast.Domain.Services;
using Multicast.Web.Controllers;

namespace Multicast.Web.Tests.Controllers
{
    public class WebhookControllerTests
    {
        [Fact]
        public async void Get_ReturnsSubscription_IfFound()
        {
            // Arrange
            var url = "http://localhost:5000/webhook";
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            subscriptionServiceMock.Setup(x => x.GetAsync(url))
                .ReturnsAsync(new Subscription { Url = url });

            var controller = new WebhookController(subscriptionServiceMock.Object, null!);

            // Act
            var result = await controller.Get(url);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Subscription>>(result);
            Assert.Equal(url, actionResult.Value.Url);
        }

        [Fact]
        public async void Get_ReturnsNotFound_IfNotFound()
        {
            // Arrange
            var url = "http://localhost:5000/webhook";
            var subscriptionServiceMock = new Mock<ISubscriptionService>();
            subscriptionServiceMock.Setup(x => x.GetAsync(url))
                .ReturnsAsync(null as Subscription?);

            var controller = new WebhookController(subscriptionServiceMock.Object, null!);

            // Act
            var result = await controller.Get(url);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Subscription>>(result);
            Assert.Equal(404, (actionResult.Result as NotFoundResult)?.StatusCode);
        }
    }
}