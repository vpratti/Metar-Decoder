using CommandLine;
using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace csharp_metar_display.cmd
{
    class Program
    {
        // --Metar "CYFB 271515Z 32017KT 3SM DRSN BKN040 M29/M34 A2957 RMK SC7 SLP019"

        static void Main(string[] args)
        {
            //METAR KCGC 292135Z AUTO 00000KT 10SM - VCTSRA SCT120 23 / 22 A3012 RMK AO2 LTG DSNT ALQS P0002
            //DisplayMetar("METAR K6A2 071515Z AUTO 00000KT 10SM CLR 25 / 17 A2996 RMK AO2");
            //DisplayMetar("METAR KCKB 182253Z 00000KT 10SM CLR 06/M07 A3035 RMK AO2 SLP283 T00561067");
            //DisplayMetar("SPECI KCBK 182253Z 14021G26KT 1/4SM -SN BR BKN008 02/01 A1973");
            //DisplayMetar("METAR KCMA 251655Z 00000KT l0SM CLR 24/19 A3003 RMK AO2 SLP177 T02440189");

            //DisplayMetar("METAR PAKN 071654Z 17005KT 1/4SM R12/2000V2200FT FG VV002 07/07 A2997 RMK AO2 SLP150 T00670067");
            //DisplayMetar("SPECI PAKN 071733Z 16006KT 1/4SM R12/1000V1200FT FG VV002 08/08 A2998 RMK AO2 T00830083");
            //DisplayMetar("METAR KCGC 292135Z AUTO 00000KT 10SM -VCTSRA SCT120 23/22 A3012 RMK");
            //DisplayMetar("METAR KLVS 170553Z AUTO 10003KT 14/11 A3020 RMK AO2 SLP126 60004 T01390111 10194 20128 50002 $");
            //DisplayMetar("METAR KBFL 021954Z 10SM CLR 36/11 A2986 RMK AO2 SLP103 T03560111");
            //DisplayMetar("KAHN 120051Z AUTO 22003KT 10SM CLR 03/M03 A3015 RMK AO2 SLP212 T00281028");
            DisplayMetar("METAR KUNV 141253Z 00000KT 1 1/4SM BR BKN001 20/20 A3012");

        }

        private static void DisplayMetar(string metar)
        {
            try
            {
                Display(MetarDisplay.GetWeatherMessage(metar));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Display(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
        }
    }
}
