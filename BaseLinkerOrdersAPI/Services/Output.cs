using BaseLinkerOrdersAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLinkerOrdersAPI.Services
{
  class Output : IOutput
  {
    private readonly IOutput[] outputs;
    public Output(IOutput[] _outputs)
    {
      outputs = _outputs;
    }
    public void WriteLine(int level, string message)
    {
      Array.ForEach(outputs, i => i.WriteLine(level, message));
    }

    public void WriteLine(int level, Exception ex, string message)
    {
      Array.ForEach(outputs, i => i.WriteLine(level, ex, message));
    }
  }
}
