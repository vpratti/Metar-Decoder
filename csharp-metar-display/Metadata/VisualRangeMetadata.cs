using csharp_metar_decoder.entity;
using NString;
using System;
using System.Linq;
using System.Text;

namespace csharp_metar_display.metadata
{
    /// <inheritdoc/>
    public class VisualRangeMetadata : Metadata
    {
        public override void Parse(DecodedMetar decodedMetar)
        {
            var visualRange = new StringBuilder();
            var subWeatherMessageSb = new StringBuilder();

            if (decodedMetar.RunwaysVisualRange?.Count > 0)
            {
                visualRange.Append(string.Join(", ", decodedMetar.RunwaysVisualRange.Select(rvr =>
                {
                    var runWayNumber = rvr.Runway.Replace("L", "").Replace("C", "").Replace("R", "");
                    var runWayDirection = "";
                    if (rvr.Runway.Contains("L"))
                    {
                        runWayDirection = "left ";
                    } else if (rvr.Runway.Contains("C"))
                    {
                        runWayDirection = "center ";
                    } else if (rvr.Runway.Contains("R"))
                    {
                        runWayDirection = "right ";
                    }

                    subWeatherMessageSb.Clear();
                    
                    subWeatherMessageSb.Append(StringTemplate.Format($"{strings.VisualRangeForRunway} ", new { runway = NumberToWords.ConvertToDigits(Int32.Parse(runWayNumber)) + runWayDirection }, false));

                    if (rvr.Variable)
                    {
                        //var visualRangeMin = rvr.VisualRangeInterval?.Length > 0
                        //? (float?)Math.Round(rvr.VisualRangeInterval[0].GetConvertedValue(Value.Unit.StatuteMile))
                        //: null;
                        //if (visualRangeMin == null)
                        //{
                        //    visualRangeMin = rvr.VisualRange != null
                        //        ? (float?)Math.Round(rvr.VisualRange.GetConvertedValue(Value.Unit.StatuteMile))
                        //        : null;
                        //}

                        //var visualRangeMax = rvr.VisualRangeInterval?.Length > 1
                        //    ? (float?)Math.Round(rvr.VisualRangeInterval[1].GetConvertedValue(Value.Unit.StatuteMile))
                        //    : null;
                        //subWeatherMessageSb.Append(StringTemplate.Format(strings.VisualRangeBetween + " ", new { visualRangeMin = visualRangeMin, visualRangeMax = visualRangeMax }, false));
                        subWeatherMessageSb.Append(StringTemplate.Format(strings.VisualRangeBetween + " ", new { 
                            visualRangeMin = NumberToWords.ConvertToWords((int)rvr.VisualRangeInterval[0].ActualValue), 
                            visualRangeMax = NumberToWords.ConvertToWords((int)rvr.VisualRangeInterval[1].ActualValue),
                            unit = rvr.VisualRangeInterval[0].ActualUnit.ToString().ToLower()
                        }, false));
                        subWeatherMessageSb = subWeatherMessageSb.Replace("  ", " ");
                    }
                    else
                    {
                        subWeatherMessageSb.Append(StringTemplate.Format(strings.VisualRangeExact + " ", new { visualRangeMin = NumberToWords.ConvertToWords((int)rvr.VisualRange.ActualValue), unit = rvr.VisualRange.ActualUnit.ToString().ToLower() }, false));

                    }

                    switch (rvr.PastTendency)
                    {
                        case RunwayVisualRange.Tendency.U:
                            subWeatherMessageSb.Append($"({strings.UpwardTrend}) ");
                            break;
                        case RunwayVisualRange.Tendency.D:
                            subWeatherMessageSb.Append($"({strings.DownwardTrend}) ");
                            break;
                        case RunwayVisualRange.Tendency.NONE:
                            break;
                        case RunwayVisualRange.Tendency.N:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    return subWeatherMessageSb;
                })));
            }
            Label = strings.VisualRangeLabel;
            Message = visualRange.ToString();
        }
    }
}
