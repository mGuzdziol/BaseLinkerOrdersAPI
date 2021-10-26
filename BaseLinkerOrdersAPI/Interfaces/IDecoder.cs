using System;
using System.Text;

namespace BaseLinkerOrdersAPI.Interfaces
{
  public interface IDecoder
  {
    string Encode(string value);
    string Encode(string value, Encoding encoding);
    string Decode(string value);
    string Decode(string value, Encoding encoding);
  }
}
