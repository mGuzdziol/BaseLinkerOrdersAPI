using BaseLinkerOrdersAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLinkerOrdersAPI
{
  public class Processor
  {
    private readonly IOutput output;
    public Processor(IOutput _output)
    {
      output = _output;
    }

    public void Run()
    {
      output.WriteLine(1, "Program strted");


    }
  }
}
