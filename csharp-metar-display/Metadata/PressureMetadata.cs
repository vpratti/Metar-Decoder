using csharp_metar_decoder.entity;
using NString;
using System;
using System.Text;

namespace csharp_metar_display.metadata
{
    public class PressureMetadata : Metadata
    {
        /// <inheritdoc/>
        public override void Parse(DecodedMetar decodedMetar)
        {
            var pressure = new StringBuilder();
            if (decodedMetar.Pressure != null)
            {
                pressure.Append(
                    StringTemplate.Format(
                        strings.PressureDetails,
                        new
                        {
                            mercuryInch = NumberToWords.ConvertToDigits((int)Math.Round(Math.Round(decodedMetar.Pressure.GetConvertedValue(Value.Unit.MercuryInch) * 100)))
                        }, false));
            }
            Label = strings.PressureLabel;
            Message = pressure.ToString();
        }
    }
}
