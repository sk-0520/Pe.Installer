using System;
using System.Collections.Generic;
using System.Globalization;
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
                    Content = new StringContent(@"{
                        ""items"": [
                            {
                                ""version"": ""1.20.003"",
                                ""minimum_version"": ""0.91.000"",
                                ""release"": ""2345-12-31T00:00:00Z"",
                                ""revision"": ""REVISION"",
                                ""platform"": ""x86"",
                                ""note_uri"": ""http://localhost.invalid/note"",
                                ""archive_uri"": ""http://localhost.invalid/archive"",
                                ""archive_size"": 9223372036854775807,
                                ""archive_kind"": ""7z"",
                                ""archive_hash_kind"": ""SHA256"",
                                ""archive_hash_value"": ""ARCHIVE_HASH_VALUE""
                            }
                        ]
                    }"),
                })
                .Verifiable()
            ;
            var progressLoggerMock = new Mock<IProgressLogger>();
            var loggerFactoryMock = new Mock<ILoggerFactory>();
            loggerFactoryMock
                .Setup((m) => m.CreateLogger(It.IsAny<Type>()))
                .Returns(new Mock<ILogger>().Object)
            ;


            var downloader = new Downloader(
                new HttpClient(handlerMock.Object, false),
                progressLoggerMock.Object,
                loggerFactoryMock.Object
            );

            var actual_x86 = await downloader.GetUpdateItemDataAsync(new Uri("http://localhost.invalid/update"), "x86", CancellationToken.None);

            Assert.Equal(new Version("1.20.003"), actual_x86.Version);
            Assert.Equal(new Version("0.91.000"), actual_x86.MinimumVersion);
            Assert.Equal(DateTimeOffset.Parse("2345-12-31T00:00:00Z", CultureInfo.InvariantCulture), actual_x86.Release);
            Assert.Equal("REVISION", actual_x86.Revision);
            Assert.Equal("x86", actual_x86.Platform);
            Assert.Equal(new Uri("http://localhost.invalid/note"), actual_x86.NoteUri);
            Assert.Equal(new Uri("http://localhost.invalid/archive"), actual_x86.ArchiveUri);
            Assert.Equal(9223372036854775807L, actual_x86.ArchiveSize);
            Assert.Equal("7z", actual_x86.ArchiveKind);
            Assert.Equal("SHA256", actual_x86.ArchiveHashKind);
            Assert.Equal("ARCHIVE_HASH_VALUE", actual_x86.ArchiveHashValue);
        }

        #endregion
    }
}
