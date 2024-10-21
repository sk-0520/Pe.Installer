using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SevenZipExtractor;

namespace Pe.Installer
{
    public class Extractor
    {
        public Extractor(IProgressLogger progressLogger, ILoggerFactory loggerFactory)
        {
            ProgressLogger = progressLogger;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region property

        private IProgressLogger ProgressLogger { get; }
        private ILogger Logger { get; }

        #endregion

        #region function

        private void Extract7z(Stream stream, DirectoryInfo extractDirectory)
        {
            var dllPath = Path.Combine(Constants.TemporaryDirectoryPath, "7z.dll");
            if(!File.Exists(dllPath)) {
                Directory.CreateDirectory(Constants.TemporaryDirectoryPath);
                File.WriteAllBytes(dllPath, Properties.Resources.File_SevenZipDll);
            }

            using(var archive = new ArchiveFile(stream, null,dllPath)) {
                var totalExtractItemCount = archive.Entries.Count;
                var extractedItemCount = 0;
                archive.Extract(e => {
                    var expandPath = Path.Combine(extractDirectory.FullName, e.FileName);

                    Logger.LogInfo($"{Properties.Resources.String_Extract_Extract}: {expandPath}");
                    extractedItemCount += 1;
                    ProgressLogger.Set((int)(extractedItemCount / (double)totalExtractItemCount));

                    if(extractedItemCount == totalExtractItemCount) {
                        ProgressLogger.Wait();
                    }

                    return expandPath;
                });
            }

        }

        public Task ExtractAsync(Stream stream, DirectoryInfo directory, string archive)
        {
            ProgressLogger.Reset(1);
            return Task.Run(() => {
                switch(archive) {
                    //case "zip":
                    //    ExtractZip(stream, directory);
                    //    break;

                    case "7z":
                        Extract7z(stream, directory);
                        break;

                    default:
                        throw new NotSupportedException($"kind: {archive}");
                }
            });
        }

        #endregion
    }
}
