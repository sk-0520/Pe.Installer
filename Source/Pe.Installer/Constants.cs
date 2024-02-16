using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pe.Installer
{
    public static class Constants
    {
        #region property

        /// <summary>
        /// アップデートに用いる情報の記載されたファイルの URI。
        /// </summary>
        public static Uri UpdateFileUri { get; } =
#if DEBUG
             new Uri("http://localhost/api/application/version/update")
#else
             new Uri("https://peserver.site/api/application/version/update")
#endif
        ;

        public static string InstallDirectoryPath { get; } =
#if DEBUG
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "install")
#else
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pe", "application")
#endif
        ;

        public static string TemporaryDirectoryPath { get; } =
#if DEBUG
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "temp")
#else
            Environment.ExpandEnvironmentVariables(Path.Combine("%TEMP%", "Pe", "installer"))
#endif
            ;

#if DEBUG
        public static string ApplicationDataDirectoryPath { get; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data");
#endif
        public static Uri ProjectUri { get; } = new Uri("https://github.com/sk-0520/Pe.Installer");

        #endregion
    }
}
