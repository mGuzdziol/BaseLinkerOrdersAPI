using BaseLinkerOrdersAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLinkerOrdersAPI.Services
{
  public class ConsoleOutput : IOutput
  {
    private readonly Dictionary<int, string> _messageLevel = new Dictionary<int, string>
    {
      [0] = "[DEBUG]",
      [1] = "[INFO]",
      [2] = "[WARNING]",
      [3] = "[ERROR]",
      [4] = "[FATAL]"
    };

    private string AppendLevel(string message, int level)
    {
      return _messageLevel[level] + " " + message;
    }

    public void WriteLine(int level, string message)
    {
      message = AppendLevel(message, level);
      Console.WriteLine(message);
    }

    public void WriteLine(int level, Exception ex, string message)
    {
      message = AppendLevel(message, level);
      message += "\r\nWyjątek: " + ex.ToString();
      Console.WriteLine(message);
    }
  }
}
