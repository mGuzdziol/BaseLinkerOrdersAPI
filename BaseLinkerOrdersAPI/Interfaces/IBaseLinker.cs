using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLinkerOrdersAPI.Interfaces
{
  public interface IBaseLinker
  {
    public T GetOrder<T>(int orderId);
    public List<T> GetOrders<T>();
    public T GetProduct<T>(int productId);
    public List<T> GetProducts<T>();
    public int AddOrder<T>(T order);
  }
}
