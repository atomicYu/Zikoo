using System;
using System.Collections.Generic;
using System.Linq;

namespace ZiKooLibrary
{
    public class Format : IFormat
    {
        public int PointNumber { get; set; }
        public double HUgao { get; set; }
        public double VerUg { get; set; }
        public double KDuz { get; set; }
        public double HDuz { get; set; }
        public double DH { get; set; }
        public double VPrizme { get; set; }
        public double VInstrument { get; set; }

        public Format()
        {

        }
        public Format(string number, string ugao, string verUg, string kDuz, string hDuz, string dH, string vPrizme, string vInstrument)
        {
            this.PointNumber = Convert.ToInt32(number.Substring(8));
            HUgao = Convert.ToDouble(ugao.Substring(6)) / 100000;
            VerUg = Convert.ToDouble(verUg.Substring(6)) / 100000;
            this.KDuz = Convert.ToDouble(kDuz.Substring(6)) / 1000;
            this.HDuz = Convert.ToDouble(hDuz.Substring(6)) / 1000;
            this.DH = Convert.ToDouble(dH.Substring(6)) / 1000;
            this.VPrizme = Convert.ToDouble(vPrizme.Substring(6)) / 1000;
            this.VInstrument = Convert.ToDouble(vInstrument.Substring(6)) / 1000;
        }

        public Format(string number, string hUgao, string hDuz, string dH, string vPrizm, string vInstr)
        {
            this.PointNumber = Convert.ToInt32(number);
            this.HUgao = Convert.ToDouble(hUgao);
            this.HDuz = Convert.ToDouble(hDuz);
            this.DH = Convert.ToDouble(dH);
            VPrizme = Convert.ToDouble(vPrizm);
            VInstrument = Convert.ToDouble(vInstr);
        }

        public Format(double hUgao, double hDuz, double dH)
        {
            this.HUgao = hUgao;
            this.HDuz = hDuz;
            this.DH = dH;

        }

        public static Format AverageReadings(List<Format> avgReading)
        {
            double ug = avgReading.Average(x => x.HUgao);
            double duz = avgReading.Average(x => x.HDuz);
            double dh = avgReading.Average(x => x.DH);

            avgReading[0].HUgao = ug;
            avgReading[0].HDuz = duz;
            avgReading[0].DH = dh;

            return avgReading[0];
        }

        public static double AngleInDec(double angleInDec)
        {
            return Math.Truncate(angleInDec) + Math.Truncate((angleInDec - Math.Truncate(angleInDec)) * 100) / 60 +
                    ((((angleInDec - Math.Truncate(angleInDec)) * 100 - Math.Truncate((angleInDec - Math.Truncate(angleInDec)) * 100)) * 100)) / 3600;
        }
    }
}
