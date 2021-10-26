using BaseLinkerOrdersAPI.Interfaces;
using BaseLinkerOrdersAPI.Model;
using BaseLinkerOrdersAPI.Model.Order;
using Newtonsoft.Json;
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
    private readonly IDecoder decoder;
    private readonly IBaseLinker baseLinker;
    public Processor(IOutput _output, IDecoder _decoder, IBaseLinker _baseLinker)
    {
      output = _output;
      decoder = _decoder;
      baseLinker = _baseLinker;
    }

    public void Run()
    {
      output.WriteLine(1, "Program strted");
      //var str = decoder.Encode("string to encode");

      GetFirstOrderAndAddProduct();
    }

    private void GetFirstOrderAndAddProduct()
    {
      var list = baseLinker.GetOrders<OrderGetModel>();
    
      if (list?.Count == 0 || list == null)
      {
        output.WriteLine(3, "No orders in BaseLinker founded.");
        return;
      }

      output.WriteLine(1,"The orders have benn downloaded from the BaseLinker");
      
      var getOrder = list.ElementAt(0);
      var orderPost = new OrderPostModel();

      foreach(var prop in typeof(OrderGetModel).GetProperties())
      {
        orderPost.GetType().GetProperty(prop.Name)?.SetValue(orderPost, getOrder.GetType().GetProperty(prop.Name).GetValue(getOrder,null));
      }
      output.WriteLine(1, "Downloaded orderd has been copied");

      orderPost.ExtraField1 = $"Zamówienie utworzone na podstawie {getOrder.OrderId}";
      output.WriteLine(1, "Comment added to new order");

      orderPost.Products.Add(new Product {Name ="Gratis", Quantity = "100", PriceBrutto = "1" });
      output.WriteLine(1, "Product added to new order");

      int newOrderId;
      if((newOrderId = baseLinker.AddOrder<OrderPostModel>(orderPost)) != default(int))
      {
        output.WriteLine(1, $"Order has been sent to BaseLinker. New order ID: {newOrderId}");
      }
      else
      {
        output.WriteLine(3, "Program couldn't sent order to BaseLinker");
        return;
      }
    }
  }
}
