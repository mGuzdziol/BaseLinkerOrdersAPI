using BaseLinkerOrdersAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BaseLinkerOrdersAPI.Services
{
  public class Decoder : IDecoder
  {
    public string Decode(string value) => Decode(value, Encoding.ASCII);

    public string Decode(string value, Encoding encoding)
    {
      if (string.IsNullOrWhiteSpace(value)) return string.Empty;
      try
      {
        Guid key = new Guid("E6568E3F-B364-4DCC-A905-3610B8A29CBE");
        Guid vi = new Guid("3BA2F373-BE23-4AA9-8DC5-A758F916CA38");
        using (var rm = new RijndaelManaged
        {
          KeySize = 128,
          Key = key.ToByteArray(),
          IV = vi.ToByteArray()
        })
        {
          byte[] input = Convert.FromBase64String(value);
          byte[] encoded = rm.CreateDecryptor().TransformFinalBlock(input, 0, input.Length);
          Encoding enc = encoding;
          return enc.GetString(encoded);
        }
      }
      catch (Exception e)
      {
        throw new Exception(string.Format("[CUBIT.Common.Tools.Decoder]: {0}", e.Message), e);
      }
    }

    public string Encode(string value) => Encode(value, Encoding.ASCII);

    public string Encode(string value, Encoding encoding)
    {
      if (string.IsNullOrWhiteSpace(value)) return string.Empty;
      try
      {
        Guid key = new Guid("E6568E3F-B364-4DCC-A905-3610B8A29CBE");
        Guid vi = new Guid("3BA2F373-BE23-4AA9-8DC5-A758F916CA38");
        using (var rm = new RijndaelManaged
        {
          KeySize = 128,
          Key = key.ToByteArray(),
          IV = vi.ToByteArray()
        })
        {
          Encoding enc = encoding;
          byte[] input = enc.GetBytes(value);
          byte[] coded = rm.CreateEncryptor().TransformFinalBlock(input, 0, input.Length);
          return Convert.ToBase64String(coded);
        }
      }
      catch (Exception e)
      {
        throw new Exception(string.Format("[CUBIT.Common.Tools.Decoder]: {0}", e.Message), e);
      }
    }
  }
}
