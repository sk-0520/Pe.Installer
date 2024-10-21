using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pe.Installer
{
    public class Checker
    {
        public Checker(IProgressLogger progressLogger, ILoggerFactory loggerFactory)
        {
            ProgressLogger = progressLogger;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        #region property

        private IProgressLogger ProgressLogger { get; }
        private ILogger Logger { get; }

        #endregion

        #region function

        public Task<bool> CheckAsync(Stream stream, long archiveSize, string hashKind, string hashValue)
        {
            ProgressLogger.Wait();

            Logger.LogDebug($"{Properties.Resources.String_Check_Size_Info}: {archiveSize}");
            Logger.LogDebug($"{Properties.Resources.String_Check_Size_Data}: {stream.Length}");
            if(stream.Length != archiveSize) {
                Logger.LogWarning($"{stream.Length} != {archiveSize}");
                return Task.FromResult(false);
            }

            return Task.Run(() => {
                using(var algorithm = HashAlgorithm.Create(hashKind)) {
                    stream.Position = 0;
                    var hash = algorithm.ComputeHash(stream);
                    stream.Position = 0;
                    var value = BitConverter.ToString(hash).Replace("-", string.Empty);
                    Logger.LogDebug($"{Properties.Resources.String_Check_Hash_Info}: {hashValue}");
                    Logger.LogDebug($"{Properties.Resources.String_Check_Size_Data}: {value}");
                    return string.Equals(hashValue, value, StringComparison.OrdinalIgnoreCase);
                }
            });


        }

        #endregion
    }
}
