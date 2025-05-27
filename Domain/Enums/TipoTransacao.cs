using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TipoTransacao
    {
        [Description("P")]
        P = 1,

        [Description("C")]
        C = 2,

        [Description("D")]
        D = 3
    }
}