using System.Text.Json.Serialization;

namespace companyApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CompanyRating
    {
        FiveStar = 1,
        FourStar = 2,
        ThreeStar = 3
    }
}