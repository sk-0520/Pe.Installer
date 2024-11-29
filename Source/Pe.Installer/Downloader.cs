using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Pe.Installer
{
    public class Downloader
    {
        public Downloader(HttpClient httpClient, IProgressLogger progressLogger, ILoggerFactory loggerFactory)
        {
            HttpClient = httpClient;
            ProgressLogger = progressLogger;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region property

        private IProgressLogger ProgressLogger { get; }
        private ILogger Logger { get; }
        private HttpClient HttpClient { get; }

        #endregion

        #region function

        public async Task<UpdateItemData> GetUpdateItemDataAsync(Uri updateInfoUri, string platform, CancellationToken cancellationToken)
        {
            Logger.LogInfo($"{Properties.Resources.String_Download_UpdateInfoUri}: {updateInfoUri}");

            ProgressLogger.Wait();

            var updateInfoText = await HttpClient.GetStringAsync(updateInfoUri);

            Logger.LogTrace(updateInfoText);

            var updateInfo = JsonConvert.DeserializeObject<UpdateData>(updateInfoText);

            Logger.LogDebug(JsonConvert.SerializeObject(updateInfo));

            var updateItemData = updateInfo.Items
                .Where(i => i.Platform == platform)
                .OrderByDescending(i => i.Version)
                .First()
            ;

            Logger.LogInfo($"{Properties.Resources.String_Download_Version}: {updateItemData.Version}");

            return updateItemData;
        }

        public async Task<Stream> GetArchiveAsync(Uri archiveUri, CancellationToken cancellationToken)
        {
            Logger.LogInfo($"{Properties.Resources.String_Download_ArchiveUri}: {archiveUri}");
            ProgressLogger.Wait();

            var response = await HttpClient.GetAsync(archiveUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            var contentLength = response.Content.Headers.First(h => h.Key.Equals("Content-Length")).Value.FirstOrDefault();
            if(!int.TryParse(contentLength, out var length)) {
                length = -1;
            }
            var knownSize = length != -1;

            var result = knownSize
                ? new MemoryStream(length)
                : new MemoryStream(60 * 1024 * 1024)
            ;
            var prevPercent = 0;

            using(var stream = await response.Content.ReadAsStreamAsync()) {
                if(knownSize) {
                    ProgressLogger.Reset(1);
                } else {
                    ProgressLogger.Wait();
                }

                var buffer = new byte[1024 * 4];
                int totalReadSize = 0;
                int readSize = 0;

                do {
                    readSize = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                    totalReadSize += readSize;
                    if(knownSize) {
                        var percent = (int)((totalReadSize / (double)length) * 100.0);
                        if(prevPercent != percent) {
                            prevPercent = percent;
                            Logger.LogDebug($"{totalReadSize} / {length}");

                            if(percent % 10 == 0) {
                                Logger.LogInfo($"{percent / 100.0:P0} - {totalReadSize:#,0} / {length:#,0}");
                            }
                        }
                        ProgressLogger.Set(percent);
                    }
                    if(0 < readSize) {
                        await result.WriteAsync(buffer, 0, readSize);
                    }
                } while(readSize != 0);
            }

            result.Position = 0;

            return result;
        }

        #endregion
    }
}
