using csharp_metar_decoder;
using csharp_metar_decoder.entity;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using csharp_metar_display.metadata;

namespace csharp_metar_display
{
    public static class MetarDisplay
    {
        /// <summary>
        /// Retrieves a beautified string output of a METAR message
        /// </summary>
        /// <param name="rawMetar">The raw METAR.</param>
        /// <param name="strictMode">METAR strict decoding mode</param>
        /// <returns></returns>
        public static string GetWeatherMessage(string rawMetar, OutputType outputType = OutputType.Text, bool strictMode = true, bool showRawMetar = false, string htmlClass = "")
        {
            var decodedMetar = MetarDecoder.ParseWithMode(rawMetar, strictMode);
            return decodedMetar.GetWeatherMessage(outputType, showRawMetar, htmlClass).Replace("  ", " ").Replace(" ,", ",");
        }

        private static ReadOnlyCollection<metadata.Metadata> _defaultMetadataToDisplay = new ReadOnlyCollection<metadata.Metadata>(new List<metadata.Metadata>()
        {
            new ObservationDateMetadata(),
            new WindMetadata(),
            new VisibilityMetadata(),
            new VisualRangeMetadata(),            
            new WindShearMetadata(),
            new ObscurationsMetadata(),
            new CloudsMetadata(),
            new TemperaturesMetadata(),
            new PressureMetadata(),
        });

        /// <summary>
        /// Retrieves a beautified string output of a DecodedMetar
        /// </summary>
        /// <param name="decodedMetar">The decoded METAR.</param>
        public static string GetWeatherMessage(this DecodedMetar decodedMetar, OutputType outputType = OutputType.Text, bool showRawMetar = false, string htmlClass = "")
        {
            var weatherMessageSb = new StringBuilder();

            if (!decodedMetar.IsValid)
            {
                weatherMessageSb.Append($"{decodedMetar.RawMetar}, ");
                weatherMessageSb.Append($"{strings.MetarIsNotValid}:, ");

                foreach (var metarChunkDecodedEx in decodedMetar.DecodingExceptions)
                {
                    weatherMessageSb.Append($"{metarChunkDecodedEx.Message}, ");
                }
            }
            else
            {
                switch (outputType)
                {
                    case OutputType.Text:
                        weatherMessageSb.Append(decodedMetar.OutputText(showRawMetar));
                        break;
                    case OutputType.HtmlTable:
                        weatherMessageSb.Append(decodedMetar.OutputHtmlTable(htmlClass, showRawMetar));
                        break;
                    case OutputType.HtmlDiv:
                        weatherMessageSb.Append(decodedMetar.OutputHtmlText(htmlClass, showRawMetar));
                        break;
                }
            }

            return weatherMessageSb.ToString();
        }
        private static string OutputText(this DecodedMetar decodedMetar, bool showRawMetar = false)
        {
            var weatherMessageSb = new StringBuilder();
            if (showRawMetar)
            {
                var rawMetarMetadata = new RawMetarMetadata();
                rawMetarMetadata.Parse(decodedMetar);
                weatherMessageSb.Append(rawMetarMetadata.ToString());
            }

            foreach (var metadata in _defaultMetadataToDisplay)
            {
                metadata.Parse(decodedMetar);
                weatherMessageSb.Append(metadata.ToString());
            }
            return weatherMessageSb.ToString().TrimEnd().TrimEnd(',');
        }
        private static string OutputHtmlTable(this DecodedMetar decodedMetar, string tableClass = "", bool showRawMetar = false)
        {
            var weatherMessageSb = new StringBuilder();
            if (string.IsNullOrEmpty(tableClass))
            {
                weatherMessageSb.Append($"<table>,");
            }
            else
            {
                weatherMessageSb.Append($"<table class=\"{tableClass}\">, ");
            }
            if (showRawMetar)
            {
                var rawMetarMetadata = new RawMetarMetadata();
                rawMetarMetadata.Parse(decodedMetar);
                weatherMessageSb.Append(rawMetarMetadata.ToHtmlTableRow());
            }

            foreach (var metadata in _defaultMetadataToDisplay)
            {
                metadata.Parse(decodedMetar);
                weatherMessageSb.Append(metadata.ToHtmlTableRow());
            }

            weatherMessageSb.Append("</table>");
            return weatherMessageSb.ToString();
        }
        private static string OutputHtmlText(this DecodedMetar decodedMetar, string divClass = "", bool showRawMetar = false)
        {
            var weatherMessageSb = new StringBuilder();
            if (string.IsNullOrEmpty(divClass))
            {
                weatherMessageSb.Append($"<div>, ");
            }
            else
            {
                weatherMessageSb.Append($"<div class=\"{divClass}\">, ");
            }
            if (showRawMetar)
            {
                var rawMetarMetadata = new RawMetarMetadata();
                rawMetarMetadata.Parse(decodedMetar);
                weatherMessageSb.Append(rawMetarMetadata.ToHtmlLine());
            }
            foreach (var metadata in _defaultMetadataToDisplay)
            {
                metadata.Parse(decodedMetar);
                weatherMessageSb.Append(metadata.ToHtmlLine());
            }
            weatherMessageSb.Append("</div>");
            return weatherMessageSb.ToString();
        }
    }
}
