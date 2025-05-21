using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestfulApiExtension.Models
{
    public class ProductObject
    {
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        [Required]
        public ProductData Data { get; set; } = new();
    }

    public class ProductData
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("price")]
        [Range(0.01, double.MaxValue)]
        public double Price { get; set; }

        [JsonPropertyName("CPU model")]
        public string CpuModel { get; set; } = string.Empty;

        [JsonPropertyName("Hard disk size")]
        public string HardDiskSize { get; set; } = string.Empty;
    }
}