using csharp_metar_decoder.entity;
using NString;
using System;
using System.Text;

namespace csharp_metar_display.metadata
{
    /// <inheritdoc/>
    public class VisibilityMetadata : Metadata
    {
        public override void Parse(DecodedMetar decodedMetar)
        {
            var visibility = new StringBuilder();
            if (decodedMetar.Cavok)
            {
                visibility.Append($"{strings.VisibilityCAVOK}");
            }
            else if (decodedMetar.Visibility?.PrevailingVisibility != null)
            {
                var prevailingVisibility = decodedMetar.Visibility.PrevailingVisibility;
                var statuteMiles = prevailingVisibility.GetConvertedValue(Value.Unit.StatuteMile);
                // only US (statute mile) reports carry fractions; metric reports of a mile or more keep whole miles
                if (prevailingVisibility.ActualUnit != Value.Unit.StatuteMile && statuteMiles >= 1)
                {
                    statuteMiles = (float)Math.Floor(statuteMiles);
                }
                visibility.Append(
                    StringTemplate.Format(
                        strings.PrevailingVisibility,
                        new
                        {  
                            prevailingVisibility = (decodedMetar.Visibility.LessThan ? "less than " : string.Empty) +
                                NumberToWords.ConvertVisibilityToText(statuteMiles)
                        }, false));
            }
            Label = strings.VisibilityLabel; Message = visibility.ToString();
        }
    }
}
