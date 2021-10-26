using BaseLinkerOrdersAPI.Classes;
using BaseLinkerOrdersAPI.Interfaces;
using BaseLinkerOrdersAPI.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseLinkerOrdersAPI.Services
{
  class BaseLinker : IBaseLinker
  {
    private readonly string token;
    private readonly string url;
    private readonly IDecoder decoder;

    public BaseLinker(string _token, string _url, IDecoder _decoder)
    {
      decoder = _decoder;
      token = decoder.Decode(_token);
      url = _url;
    }

    private ResponseResult<T> ResponseGet<T>(string endpoint, Dictionary<string, string> parameters = null)
    {
      try
      {
        using (WebClient webClient = new WebClient())
        {
          webClient.QueryString.Add("token", token);
          webClient.QueryString.Add("method", endpoint);
          if(parameters != null) 
            webClient.QueryString.Add("parameters", JsonConvert.SerializeObject(parameters));

          var data = webClient.UploadValues(url, "POST", webClient.QueryString);
          var responseString = UnicodeEncoding.UTF8.GetString(data);
          JObject json = JObject.Parse(responseString);
          var status = json.First.ToObject<string>() == "ERROR" ? ResponseStatus.ERROR : ResponseStatus.SUCCESS;
          return new ResponseResult<T>() 
          { Status = status, 
            Result = status == ResponseStatus.SUCCESS ? json.Last.First.ToObject<T>() : default(T) };
        }
      }
      catch (WebException ex)
      {
        throw ex;
      }
    }

    private ResponseResult<T> ResponsePost<T>(string endpoint, string parameters)
    {
      try
      {
        using (WebClient webClient = new WebClient())
        {
          webClient.QueryString.Add("token", token);
          webClient.QueryString.Add("method", endpoint);
          if (parameters != null)
            webClient.QueryString.Add("parameters", parameters);

          var data = webClient.UploadValues(url, "POST", webClient.QueryString);
          var responseString = UnicodeEncoding.UTF8.GetString(data);
          JObject json = JObject.Parse(responseString);
          var status = json.First.ToObject<string>() == "ERROR" ? ResponseStatus.ERROR : ResponseStatus.SUCCESS;
          return new ResponseResult<T>()
          {
            Status = status,
            Result = status == ResponseStatus.SUCCESS ? json.Last.First.ToObject<T>() : default(T)
          };
        }
      }
      catch (WebException ex)
      {
        throw ex;
      }
    }

    public T GetOrder<T>(int orderId)
    {
      return ResponseGet<List<T>>("getOrders", new Dictionary<string, string> { { "order_id", orderId.ToString() } }).Result.FirstOrDefault();
    }

    public List<T> GetOrders<T>() => ResponseGet<List<T>>("getOrders").Result;
    

    public T GetProduct<T>(int productId)
    {
      return ResponseGet<T>("getProduct", new Dictionary<string, string> { { "product_id", productId.ToString() } }).Result;
    }

    public List<T> GetProducts<T>() => ResponseGet<List<T>>("getProducts").Result;

    private string GetToken()
    {
      return token;
    }

    public int AddOrder<T>(T order)
    {
      return ResponsePost<int>("addOrder", JsonConvert.SerializeObject(order)).Result;
    }
  }
}
