using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLinkerOrdersAPI.Interfaces
{
  public interface IOutput
  {
    void WriteLine(int level, string message);
    void WriteLine(int level, Exception ex, string message);
  }
}
