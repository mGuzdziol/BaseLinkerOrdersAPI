using BaseLinkerOrdersAPI.Classes;
using BaseLinkerOrdersAPI.Interfaces;
using BaseLinkerOrdersAPI.Services;
using Ninject;
using System;
using System.Configuration;

namespace BaseLinkerOrdersAPI
{
  enum LoggingType
  {
    CONSOLEnFILE = 0,
    CONSOLE = 1,
    FILE = 2
  }

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
      AddSingleton<IDecoder,Decoder>();

      Bind<IBaseLinker>().To<BaseLinker>().WithConstructorArgument("_token", ConfigurationManager.AppSettings["BaseLinkerToken"])
        .WithConstructorArgument("_url", ConfigurationManager.AppSettings["BaseLinkerUrl"]);

      switch (Convert.ToInt32(ConfigurationManager.AppSettings["LoggingType"]))
      {
        case (int)LoggingType.CONSOLE:
          Bind<IOutput>().To<ConsoleOutput>().InSingletonScope();
          break;
        case (int)LoggingType.FILE:
          Bind<IOutput>().ToConstant(new LogOutput().Setup("SellIntegroProgram", ConfigurationManager.AppSettings["LogFilePath"])).InSingletonScope();
          break;
        default: // Console and File
          Bind<IOutput>().To<Output>().When(r => r.Target?.Member.DeclaringType != typeof(Output)).InSingletonScope();
          Bind<IOutput>().To<ConsoleOutput>().WhenInjectedInto<Output>().InSingletonScope();
          Bind<IOutput>().ToConstant(new LogOutput().Setup("SellIntegroProgram", ConfigurationManager.AppSettings["LogFilePath"])).WhenInjectedInto<Output>().InSingletonScope();
          break;
      }
    }
  }
}
