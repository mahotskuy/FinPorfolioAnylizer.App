using System.ComponentModel;
using System.Text.Json.Serialization;
using FinPortfolioAnalyzer.Data.Enums;

namespace FinPortfolioAnalyzer.Data.Models
{
    public class Asset
    {
        [JsonPropertyName("name")]
        [JsonInclude]
        [Description("Name of the asset.")]
        public string Name { get; set; }

        [JsonPropertyName("asset_type")]
        [JsonInclude]
        [Description("Type of the asset.")]
        public AssetType AssetType { get; set; }

        [JsonPropertyName("term_of_asset")]
        [JsonInclude]
        [Description("Term of the asset.")]
        public TermOfAsset TermOfAsset { get; set; }

        [JsonPropertyName("region")]
        [JsonInclude]
        [Description("Region where the asset is located.")]
        public Region Region { get; set; }

        [JsonPropertyName("amount")]
        [JsonInclude]
        [Description("Value of the asset.")]
        public MoneyValue Amount { get; set; } = new();

        public Asset(string name, AssetType assetType, TermOfAsset termOfAsset, Region region, MoneyValue amount)
        {
            Name = name;
            AssetType = assetType;
            TermOfAsset = termOfAsset;
            Region = region;
            Amount = amount;
        }
    }
 ;
}
