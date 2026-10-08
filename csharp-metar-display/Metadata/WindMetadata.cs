using csharp_metar_decoder.entity;
using NString;
using System;
using System.Text;

namespace csharp_metar_display.metadata
{
    /// <inheritdoc/>
    public class WindMetadata : Metadata
    {
        public override void Parse(DecodedMetar decodedMetar)
        {
            var wind = new StringBuilder();

            if (decodedMetar.SurfaceWind != null)
            {
                if (decodedMetar.SurfaceWind.MeanDirection != null)
                {
                    if (decodedMetar.SurfaceWind.MeanDirection.ActualValue == 0 && decodedMetar.SurfaceWind.MeanSpeed.ActualValue == 0)
                    {
                        wind.Append(strings.WindCalm);
                    }
                    else
                    {
                        wind.Append(StringTemplate.Format(strings.WindDetails,
                            new
                            {
                                meanDirectionDegrees = ((int)decodedMetar.SurfaceWind.MeanDirection.ActualValue < 100 ? "zero " : "") + NumberToWords.ConvertToDigits((int)decodedMetar.SurfaceWind.MeanDirection.ActualValue),
                                speedKnot = NumberToWords.ConvertToDigits((int)Math.Round(decodedMetar.SurfaceWind.MeanSpeed.GetConvertedValue(Value.Unit.Knot))),
                            }));
                    }
                }

                // SurfaceWind.SpeedVariations property contains gust wind speed
                if (decodedMetar.SurfaceWind.SpeedVariations != null)
                {
                    if (decodedMetar.SurfaceWind.MeanDirection != null)
                    {
                        wind.Append(" ");
                    }
                    wind.Append(StringTemplate.Format(strings.WindGusts,
                        new { windGustsKnot = NumberToWords.ConvertToDigits((int)Math.Round(decodedMetar.SurfaceWind.SpeedVariations.GetConvertedValue(Value.Unit.Knot))) },
                        false));
                }

                // SurfaceWind.VariableDirection property contains variable wind direction flag information (VRB)
                if (decodedMetar.SurfaceWind.VariableDirection)
                {
                    wind.Append(StringTemplate.Format(strings.WindVariableDirection,
                        new
                        {
                            speedKnot = NumberToWords.ConvertToDigits((int)Math.Round(decodedMetar.SurfaceWind.MeanSpeed.GetConvertedValue(Value.Unit.Knot))),
                        }));
                }
                else if (decodedMetar.SurfaceWind.DirectionVariations?.Length > 1 && decodedMetar.SurfaceWind.DirectionVariations[0] != null && decodedMetar.SurfaceWind.DirectionVariations[1] != null)
                {
                    var variableWindDirection = StringTemplate.Format(strings.WindDirectionVariations,
                    new
                    {
                        windDirectionVariationMinDegrees = ((int)decodedMetar.SurfaceWind.DirectionVariations[0].ActualValue < 100 ? "zero " : "") + NumberToWords.ConvertToDigits((int)decodedMetar.SurfaceWind.DirectionVariations[0].ActualValue),
                        windDirectionVariationMinCardinal = decodedMetar.SurfaceWind.DirectionVariations[0].ActualValue.ConvertDegreesToCardinal(false).ToLowerInvariant(),
                        windDirectionVariationMaxDegrees = ((int)decodedMetar.SurfaceWind.DirectionVariations[1].ActualValue < 100 ? "zero " : "") + NumberToWords.ConvertToDigits((int)decodedMetar.SurfaceWind.DirectionVariations[1].ActualValue),
                        windDirectionVariationMaxCardinal = decodedMetar.SurfaceWind.DirectionVariations[1].ActualValue.ConvertDegreesToCardinal(false).ToLowerInvariant(),
                    }, false);
                    if (wind.Length > 0)
                    {
                        wind.Append(", ");
                    }
                    wind.Append(variableWindDirection);
                }
            }
            Label = strings.WindLabel;
            Message = wind.ToString();
        }
    }
}
