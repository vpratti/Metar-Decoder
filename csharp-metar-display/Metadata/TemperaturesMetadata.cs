using csharp_metar_decoder.entity;
using NString;
using System.Text;

namespace csharp_metar_display.metadata
{
    public class TemperaturesMetadata : Metadata
    {
        /// <inheritdoc/>
        public override void Parse(DecodedMetar decodedMetar)
        {
            var temperatures = new StringBuilder();
            if (decodedMetar.AirTemperature != null)
            {
                // For temperatures, original unit is degree Celsius
                if (decodedMetar.AirTemperature == null || decodedMetar.DewPointTemperature == null)
                {
                    Message = "Temperatures cannot be decoded";
                } else
                {
                    temperatures.Append(
                    StringTemplate.Format(
                        strings.TemperaturesDetails,
                        new
                        {
                            airTemperatureC = NumberToWords.ConvertToDigits((int)decodedMetar.AirTemperature.ActualValue),
                            dewTemperatureC = NumberToWords.ConvertToDigits((int)decodedMetar.DewPointTemperature.ActualValue)
                        }, false));
                    Message = temperatures.ToString();
                }

            }
            Label = strings.TemperaturesLabel;
        }
    }
}
