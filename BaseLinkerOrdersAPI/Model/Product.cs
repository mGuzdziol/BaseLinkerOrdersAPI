using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BaseLinkerOrdersAPI.Model
{
  public class Product
  {
    [JsonProperty("storage")]
    public string Storage { get; set; }

    [JsonProperty("storage_id")]
    public string StorageId { get; set; }

    [JsonProperty("order_product_id")]
    public string OrderProductId { get; set; }

    [JsonProperty("product_id")]
    public string ProductId { get; set; }

    [JsonProperty("variant_id")]
    public string VariantId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("attributes")]
    public string Attributes { get; set; }

    [JsonProperty("sku")]
    public string Sku { get; set; }

    [JsonProperty("ean")]
    public string Ean { get; set; }

    [JsonProperty("auction_id")]
    public string AuctionId { get; set; }

    [JsonProperty("price_brutto")]
    public string PriceBrutto { get; set; }

    [JsonProperty("tax_rate")]
    public string TaxRate { get; set; }

    [JsonProperty("quantity")]
    public string Quantity { get; set; }

    [JsonProperty("weight")]
    public string Weight { get; set; }

    public bool ShouldSerializeOrderProductId()
    {
      return false;
    }

    public bool ShouldSerializeAuctionId()
    {
      return false;
    }
  }
}
