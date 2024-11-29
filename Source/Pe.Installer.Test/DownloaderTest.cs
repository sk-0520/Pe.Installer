using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace Pe.Installer.Test
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2699:Tests should include assertions", Justification = "<保留中>")]
    public class DownloaderTest
    {
        #region function

        [Fact]
        public async Task GetUpdateItemDataAsyncTest()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage() {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{'key':123}]"),
                })
                .Verifiable()
            ;
            var progressLoggerMock = new Mock<IProgressLogger>();
            var loggerFactoryMock = new Mock<ILoggerFactory>();


            var downloader = new Downloader(
                new HttpClient(handlerMock.Object, false),
                progressLoggerMock.Object,
                loggerFactoryMock.Object
            );
            await downloader.GetUpdateItemDataAsync(new Uri("/update"), "x86", CancellationToken.None);

        }

        #endregion
    }
}
