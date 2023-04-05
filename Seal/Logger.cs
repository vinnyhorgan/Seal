using System;
using System.Runtime.InteropServices;
using Raylib_cs;

namespace Seal
{
    public static unsafe class Logger
    {
        private static bool _debugEnabled;

        internal static bool DebugEnabled
        {
            get { return _debugEnabled; }
            set
            {
                Raylib.SetTraceLogLevel(value ? TraceLogLevel.LOG_ALL : TraceLogLevel.LOG_INFO);
                _debugEnabled = value;
            }
        }

        public static void Info(string message)
        {
            Raylib.TraceLog(TraceLogLevel.LOG_INFO, message);
        }

        public static void Warning(string message)
        {
            Raylib.TraceLog(TraceLogLevel.LOG_WARNING, message);
        }

        public static void Error(string message)
        {
            Raylib.TraceLog(TraceLogLevel.LOG_ERROR, message);
        }

        public static void Fatal(string message)
        {
            Raylib.TraceLog(TraceLogLevel.LOG_FATAL, message);
        }

        public static void Debug(string message)
        {
            Raylib.TraceLog(TraceLogLevel.LOG_DEBUG, message);
        }

        internal static void Init()
        {
            Raylib.SetTraceLogCallback(&LogCustom);
            Debug("Initialized Logger");
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        private static void LogCustom(int logLevel, sbyte* text, sbyte* args)
        {
            var message = Logging.GetLogMessage(new IntPtr(text), new IntPtr(args));

            Console.Write("[" + DateTime.Now.ToString("G") + "] ");

            switch ((TraceLogLevel)logLevel)
            {
                case TraceLogLevel.LOG_INFO:
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("info");
                    Console.ResetColor();
                    Console.Write("] ");
                    Console.WriteLine(message);
                    break;

                case TraceLogLevel.LOG_WARNING:
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("warning");
                    Console.ResetColor();
                    Console.Write("] ");
                    Console.WriteLine(message);
                    break;

                case TraceLogLevel.LOG_ERROR:
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("error");
                    Console.ResetColor();
                    Console.Write("] ");
                    Console.WriteLine(message);
                    break;

                case TraceLogLevel.LOG_FATAL:
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("fatal");
                    Console.ResetColor();
                    Console.Write("] ");
                    Console.WriteLine(message);
                    break;

                case TraceLogLevel.LOG_DEBUG:
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("debug");
                    Console.ResetColor();
                    Console.Write("] ");
                    Console.WriteLine(message);
                    break;
            }
        }
    }
}
