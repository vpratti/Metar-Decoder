using csharp_metar_decoder.entity;

namespace csharp_metar_display.metadata
{
   public interface IMetadata
    {
        void Parse(DecodedMetar decodedMetar);
    }
}
