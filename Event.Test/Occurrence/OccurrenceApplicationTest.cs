using Event.Application.Dtos.Occurrence.Request;
using Event.Application.Interfaces;
using Event.Test;
using Event.Utils.Static;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Occurrence.Test.Occurrence
{
    [TestClass]
    public class OccurrenceApplicationTest
    {
        private static WebApplicationFactory<Program>? _factory = null!;
        private static IServiceScopeFactory? _scopeFactory = null!;

        [ClassInitialize]
        public static void Initialize(TestContext _)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        [TestMethod]
        public async Task CreateOccurrence_WhenSendingNullValuesOrEmpty_ValidationErrors()
        {
            using var scope = _scopeFactory!.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IOccurrenceApplication>();

            // Arrange
            var spot= "";
            var description = "";
            var state = 1;
            var expected = ReplyMessage.MESSAGE_VALIDATE;

            // Act
            var result = await context.CreateOccurrence(new OccurrenceRequestDto()
            {
                Spot = spot,
                Description = description,
                State = state
            });
            var current = result.Message;

            // Assert
            Assert.AreEqual(expected, current);
        }
        [TestMethod]
        public async Task CreateOccurrence_WhenSendingCorrectValues_ValidationErrors()
        {
            using var scope = _scopeFactory!.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IOccurrenceApplication>();

            // Arrange
            var spot = "Fake Occurrence";
            var description = "Description Fake Occurrence";
            var state = 1;
            var expected = ReplyMessage.MESSAGE_CREATE;

            // Act
            var result = await context.CreateOccurrence(new OccurrenceRequestDto()
            {
                Spot = spot,
                Description = description,
                State = state
            });
            var current = result.Message;

            // Assert
            Assert.AreEqual(expected, current);
        }
    }
}
