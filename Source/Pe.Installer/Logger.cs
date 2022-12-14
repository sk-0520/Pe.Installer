using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pe.Installer
{
    public enum LogKind
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
    }

    public interface ILogger
    {
        #region function

        void LogTrace(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        void LogDebug(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        void LogInfo(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        void LogWarning(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        void LogError(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);

        #endregion
    }

    public interface ILoggerFactory
    {
        #region function

        ILogger CreateLogger(Type type);

        #endregion
    }

    internal class InternalLogger: ILogger
    {
        public InternalLogger(ListBox listBox, string name)
        {
            ListBox = listBox;
            Name = name;
        }

        #region property

        ListBox ListBox { get; }

        public string Name { get; }

        IReadOnlyDictionary<LogKind, string> LogKindMap { get; } = new Dictionary<LogKind, string>() {
            [LogKind.Trace] = Properties.Resources.String_LogKind_Trace,
            [LogKind.Debug] = Properties.Resources.String_LogKind_Debug,
            [LogKind.Warning] = Properties.Resources.String_LogKind_Warning,
            [LogKind.Information] = Properties.Resources.String_LogKind_Information,
            [LogKind.Error] = Properties.Resources.String_LogKind_Error,
        };

        #endregion

        #region function

        void Log(LogKind logKind, string message, string callerMemberName, string callerFilePath, int callerLineNumber)
        {
#if !DEBUG
            if(logKind == LogKind.Trace || logKind == LogKind.Debug) {
                return;
            }
#endif
            if(ListBox.InvokeRequired) {
                ListBox.BeginInvoke(new Action(() => {
                    ListBox.Items.Add($"[{LogKindMap[logKind]}] {message}");
                    ListBox.SelectedIndex = ListBox.Items.Count - 1;
                }));
            } else {
                ListBox.Items.Add($"[{LogKindMap[logKind]}] {message}");
                ListBox.SelectedIndex = ListBox.Items.Count - 1;
            }
        }

        #endregion

        #region ILogger

        /// <inheritdoc cref="ILogger.LogTrace(string, string, string, int)"/>
        public void LogTrace(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) => Log(LogKind.Trace, message, callerMemberName, callerFilePath, callerLineNumber);
        /// <inheritdoc cref="ILogger.LogDebug(string, string, string, int)"/>
        public void LogDebug(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) => Log(LogKind.Debug, message, callerMemberName, callerFilePath, callerLineNumber);
        /// <inheritdoc cref="ILogger.Information(string, string, string, int)"/>
        public void LogInfo(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) => Log(LogKind.Information, message, callerMemberName, callerFilePath, callerLineNumber);
        /// <inheritdoc cref="ILogger.LogWarning(string, string, string, int)"/>
        public void LogWarning(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) => Log(LogKind.Warning, message, callerMemberName, callerFilePath, callerLineNumber);
        /// <inheritdoc cref="ILogger.LogError(string, string, string, int)"/>
        public void LogError(string message, [CallerMemberName] string callerMemberName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) => Log(LogKind.Error, message, callerMemberName, callerFilePath, callerLineNumber);

        #endregion
    }

    internal class InternalLoggerFactory: ILoggerFactory
    {
        public InternalLoggerFactory(ListBox listBox)
        {
            ListBox = listBox;
        }

        #region property

        ListBox ListBox { get; }

        #endregion

        #region ILoggerFactory

        public ILogger CreateLogger(Type type)
        {
            return new InternalLogger(ListBox, type.FullName);
        }

        #endregion
    }

}
