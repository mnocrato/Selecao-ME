using Newtonsoft.Json;

namespace Core.DTO
{
    public struct ItemDTO
    {
        public string Descricao { get; set; }
        public decimal PrecoUnitario { get; set; }
        [JsonProperty("qtd")]
        public int Quantidade { get; set; }
    }
}
