using BaseLinkerOrdersAPI.Classes;
using BaseLinkerOrdersAPI.Interfaces;
using BaseLinkerOrdersAPI.Services;
using Ninject;
using System;
using System.Configuration;

namespace BaseLinkerOrdersAPI
{
  class Program
  {
    static void Main(string[] args)
    {
      IKernel kernel = new StandardKernel(new MainModule());
      var processor = kernel.Get<Processor>();
      processor.Run();
    }
  }

  public class MainModule : NinjectModuleEx
  {
    public override void Load()
    {
      AddSingleton<Processor>();

      Bind<IOutput>().To<Output>().When(r => r.Target?.Member.DeclaringType != typeof(Output)).InSingletonScope();
      Bind<IOutput>().To<ConsoleOutput>().WhenInjectedInto<Output>().InSingletonScope();
      Bind<IOutput>().ToConstant(new LogOutput().Setup("SellIntegroProgram", ConfigurationManager.AppSettings["LogFilePath"])).WhenInjectedInto<Output>().InSingletonScope();
      
    }
  }
}
