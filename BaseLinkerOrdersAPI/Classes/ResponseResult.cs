using BaseLinkerOrdersAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseLinkerOrdersAPI.Classes
{
  public enum ResponseStatus
  {
    ERROR,
    SUCCESS
  }

  public class ResponseResult<T>
  {
    [JsonProperty]
    public ResponseStatus Status { get; set; }

    [JsonProperty]
    public T Result { get; set; }
  }
}
