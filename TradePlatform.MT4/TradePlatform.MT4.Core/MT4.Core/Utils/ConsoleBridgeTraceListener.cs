using System;

namespace TradePlatform.MT4.Core.Utils
{
    public class ConsoleBridgeTraceListener : BridgeTraceListener
    {
        public override void Output(TraceInfo info)
        {
            BridgeTraceErrorType type = info.Type;
            switch (type)
            {
                case BridgeTraceErrorType.Execption:
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(info.Message);
                        return;
                    }
                case BridgeTraceErrorType.HandlerExecutionError:
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(info.Message);
                        return;
                    }
                case BridgeTraceErrorType.MqlError:
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(info.Message);
                        return;
                    }
                case BridgeTraceErrorType.HostInfo:
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(info.Message);
                        return;
                    }
                case BridgeTraceErrorType.CommunicationWorkflow:
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(info.Message);
                        return;
                    }
                case BridgeTraceErrorType.Service:
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(info.Message);
                        return;
                    }
                case BridgeTraceErrorType.Debug:
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(info.Message);
                        return;
                    }
            }
            throw new ArgumentOutOfRangeException("Type");
        }
    }
}