using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ZiKooLibrary
{
    public class CoordinateCalculator
    {
        public void CooCalculator(ListView listView2, List<Format> calcPointsList, List<Format> orientationsList,
            string stationY, string stationX, string stationZ, string orientationY, string orientationX)
        {
            double dX, dY, omega, kvadrant, direkcioni, dirKvad = 0;

            dY = Convert.ToDouble(orientationY) - Convert.ToDouble(stationY);
            dX = Convert.ToDouble(orientationX) - Convert.ToDouble(stationX);
            double ralikaYX = dY / dX;

            if ((dY > 0 && dX > 0) || (dY < 0 && dX < 0))
            {
                kvadrant = ralikaYX;
            }
            else
            {
                kvadrant = -1 / ralikaYX;
            }

            omega = Math.Atan(kvadrant) * 180 / Math.PI;

            if (dY > 0 && dX > 0)
            {
                dirKvad = 0;
            }
            else if (dY > 0 && dX < 0)
            {
                dirKvad = 90;
            }
            else if (dY < 0 && dX > 0)
            {
                dirKvad = 270;
            }
            else
            {
                dirKvad = 180;
            }

            direkcioni = omega + dirKvad;

            List<double> dirDetaljnih = new List<double>();
            List<double> polarUg = new List<double>();

            for (int i = 0; i < calcPointsList.Count; i++)
            {
                polarUg.Add(calcPointsList[i].HUgao - orientationsList[0].HUgao);
            }

            for (int i = 0; i < calcPointsList.Count; i++)
            {
                dirDetaljnih.Add(direkcioni + polarUg[i]);
            }
            List<double> dYdet = new List<double>();
            List<double> dXdet = new List<double>();

            for (int i = 0; i < calcPointsList.Count; i++)
            {
                dYdet.Add(calcPointsList[i].HDuz * Math.Sin(dirDetaljnih[i] * Math.PI / 180));
                dXdet.Add(calcPointsList[i].HDuz * Math.Cos(dirDetaljnih[i] * Math.PI / 180));
            }

            List<Coordinate> detaljne = new List<Coordinate>();

            for (int i = 0; i < calcPointsList.Count; i++)
            {
                detaljne.Add(new Coordinate(calcPointsList[i].PointNumber.ToString(), (Convert.ToDouble(stationY) + dYdet[i]), (Convert.ToDouble(stationX) + dXdet[i]), (Convert.ToDouble(stationZ) + calcPointsList[i].DH)));
            }

            for (int i = 0; i < detaljne.Count; i++)
            {

                ListViewItem lv2 = new ListViewItem(detaljne[i].PointNumber);
                lv2.SubItems.Add(detaljne[i].Y.ToString("F2"));
                lv2.SubItems.Add(detaljne[i].X.ToString("F2"));
                lv2.SubItems.Add(detaljne[i].Z.ToString("F2"));
                listView2.Items.Add(lv2);

            }
        }
    }

}
