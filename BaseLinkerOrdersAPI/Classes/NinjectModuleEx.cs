using Ninject.Modules;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLinkerOrdersAPI.Classes
{
  public abstract class NinjectModuleEx : NinjectModule
  {
    protected IBindingNamedWithOrOnSyntax<TTo> AddSingleton<TFrom, TTo>() where TTo : TFrom => Bind<TFrom>().To<TTo>().InSingletonScope();
    protected IBindingNamedWithOrOnSyntax<TSelf> AddSingleton<TSelf>()  => Bind<TSelf>().ToSelf().InSingletonScope();
    
  }
}
