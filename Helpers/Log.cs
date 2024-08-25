using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIM_Tool.Helpers
{
    public static class Log
    {
        private const int MaxLogLines = 1000; // Maximale Anzahl an Zeilen in der Logdatei
        private static bool isLogInitialized = false;

        public static void InitializeLog()
        {
            string logFilePath = $"{Properties.Settings.Default.pfadDeskOK}\\MIM_LogFile.txt";
            EnsureLogFileSize(logFilePath);
            isLogInitialized = true;
        }
        public static void err(string message, Exception ex = null, bool alarmBox = false)
        {
            if (!isLogInitialized) InitializeLog();
            string callingMethod = GetCallingMethod();
            string callingClass = GetCallingClass();
            if (Directory.Exists(Properties.Settings.Default.pfadDeskOK))
            {
                using (StreamWriter writer = new StreamWriter($"{Properties.Settings.Default.pfadDeskOK}\\MIM_LogFile.txt", true))
                {
                    writer.WriteLine($"{DateTime.Now}: ERROR in {callingClass}-{callingMethod} : {message}");
                    if (ex != null)
                    {
                        writer.WriteLine($"Trace : {ex.Message}");
                        writer.WriteLine(ex.StackTrace);
                    }
                }
                SystemSounds.Exclamation.Play();
                Thread.Sleep(300);
                SystemSounds.Exclamation.Play();

                Properties.Settings.Default.LetzterFehler = $"{callingClass}-{callingMethod}: \n{message}  \n(TimeStamp of Error: {DateTime.Now})";
                Properties.Settings.Default.Save();

                if (alarmBox)
                {
                    MessageBox.Show($"{callingClass}-{callingMethod}: \n{message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void war(string message, int timeout = 2000)
        {
            if (!isLogInitialized) InitializeLog();
            if (Directory.Exists(Properties.Settings.Default.pfadDeskOK))
            {
                string callingMethod = GetCallingMethod();
                string callingClass = GetCallingClass();
                using (StreamWriter writer = new StreamWriter($"{Properties.Settings.Default.pfadDeskOK}\\MIM_LogFile.txt", true))
                {
                    writer.WriteLine($"{DateTime.Now}: WARNING in {callingClass}-{callingMethod} : {message}");
                }
                SystemSounds.Exclamation.Play();
                WarnMSGBox.Show(message, timeout);
            }
        }
        public static void inf(string message)
        {
            if (!isLogInitialized) InitializeLog();
            if (Directory.Exists(Properties.Settings.Default.pfadDeskOK) && Properties.Settings.Default.AdminMode)
            {
                string callingMethod = GetCallingMethod();
                string callingClass = GetCallingClass();
                using (StreamWriter writer = new StreamWriter($"{Properties.Settings.Default.pfadDeskOK}\\MIM_LogFile.txt", true))
                {
                    writer.WriteLine($"{DateTime.Now}: Info in {callingClass}-{callingMethod} : {message}");
                }
            }
        }
        private static void EnsureLogFileSize(string logFilePath)
        {
            if (File.Exists(logFilePath))
            {
                var lines = File.ReadAllLines(logFilePath).ToList();
                if (lines.Count > MaxLogLines)
                {
                    lines = lines.Skip(lines.Count - MaxLogLines).ToList();
                    File.WriteAllLines(logFilePath, lines);
                }
            }
        }
        private static string GetCallingMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame frame = stackTrace.GetFrame(2); // 0 = GetCallingMethod, 1 = Log method, 2 = calling method
            return frame.GetMethod().Name;
        }
        private static string GetCallingClass()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame frame = stackTrace.GetFrame(2); // 0 = GetCallingInfo, 1 = Log method, 2 = calling method
            var method = frame.GetMethod();
            if (method.DeclaringType == null)
            {
                return "Unknown";
            }
            return method.DeclaringType.Name;
             
        }
    }
}
