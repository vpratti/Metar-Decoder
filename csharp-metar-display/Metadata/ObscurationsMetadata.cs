using csharp_metar_decoder.entity;
using csharp_metar_display.Metadata.Data;
using System.Linq;
using System.Text;

namespace csharp_metar_display.metadata
{
    public class ObscurationsMetadata : Metadata
    {
        /// <inheritdoc/>
        public override void Parse(DecodedMetar decodedMetar)
        {
            var obscuratations = new StringBuilder();
            if (decodedMetar.PresentWeather != null && decodedMetar.PresentWeather.Count > 0)
            {

                var obscurationLookups = decodedMetar.PresentWeather.Select(p => p.IntensityProximity + p.Characteristics + (p.Types.Count > 0 ? p.Types[0] : "")).ToList();
                foreach(var obscuration in obscurationLookups)
                {                    
                    if (!string.IsNullOrEmpty(obscuration))
                    {
                        if (obscuratations.Length > 0)
                        {
                            obscuratations.Append(", ");
                        }
                        obscuratations.Append(MetarMatrix.Metarmappings.ContainsKey(obscuration) ? MetarMatrix.Metarmappings[obscuration] : string.Empty);
                    }
                }
            }
            Label = strings.ObscurationsLabel;
            Message = obscuratations.ToString();
        }
    }
}
