using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZiKooLibrary
{
    public class ListViewProcessor
    {
        public void InputInFormattedList(ListView listView, List<Format> formattedList)
        {
            try
            {
                formattedList.Clear();
                for (int i = 0; i < listView.Items.Count; i++)
                {
                    formattedList.Add(new Format(listView.Items[i].SubItems[0].Text, listView.Items[i].SubItems[1].Text,
                        listView.Items[i].SubItems[2].Text, listView.Items[i].SubItems[3].Text,
                        listView.Items[i].SubItems[4].Text, listView.Items[i].SubItems[5].Text));
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void ListViewFormattedListInput(ListView listView, List<Format> formattedList)
        {
            try
            {
                ListView6ColFormat(listView);

                InputIntoListView6Col(listView, formattedList);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void InputInUnformattedList(ListView listView1, List<Format> unformattedList)
        {
            try
            {
                unformattedList.Clear();
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    unformattedList.Add(new Format(listView1.Items[i].SubItems[0].Text, listView1.Items[i].SubItems[1].Text,
                        listView1.Items[i].SubItems[2].Text, listView1.Items[i].SubItems[3].Text,
                        listView1.Items[i].SubItems[4].Text, listView1.Items[i].SubItems[5].Text,
                        listView1.Items[i].SubItems[6].Text, listView1.Items[i].SubItems[7].Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ListViewFormatAndData(ListView listView, List<Format> unformattedList)
        {
            try
            {
                InputInUnformattedList(listView, unformattedList);

                ListView6ColFormat(listView);

                InputIntoListView6Col(listView, unformattedList);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void InputInCoordinateList(ListView listView2, List<Coordinate> coordinatesList)
        {
            try
            {
                coordinatesList.Clear();
                for (int i = 0; i < listView2.Items.Count; i++)
                {
                    coordinatesList.Add(new Coordinate(listView2.Items[i].SubItems[0].Text, Convert.ToDouble(listView2.Items[i].SubItems[1].Text),
                        Convert.ToDouble(listView2.Items[i].SubItems[2].Text), Convert.ToDouble(listView2.Items[i].SubItems[3].Text)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ListViewCoordinateInput(ListView listView2, List<Coordinate> coordinatesList)
        {
            try
            {
                listView2.Items.Clear();
                for (int i = 0; i < coordinatesList.Count; i++)
                {
                    ListViewItem lv2 = new ListViewItem(coordinatesList[i].PointNumber);
                    lv2.SubItems.Add(coordinatesList[i].Y.ToString("F2"));
                    lv2.SubItems.Add(coordinatesList[i].X.ToString("F2"));
                    lv2.SubItems.Add(coordinatesList[i].Z.ToString("F2"));
                    listView2.Items.Add(lv2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void CopyListView(ListView lv)
        {
            if (lv.SelectedItems.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ListViewItem item in lv.SelectedItems)
                {
                    ListViewItem l = item as ListViewItem;

                    foreach (ListViewItem.ListViewSubItem sub in l.SubItems)
                    {
                        sb.Append(sub.Text + "\t");
                    }

                    sb.AppendLine();

                }
                Clipboard.SetDataObject(sb.ToString());
            }
        }

        public void ListView8ColFormat(ListView listView)
        {
            listView.Clear();
            listView.Columns.Add("Broj Tačke", 105, HorizontalAlignment.Left);
            listView.Columns.Add("Horizontalni Ugao", 115, HorizontalAlignment.Right);
            listView.Columns.Add("Vertikalni Ugao", 115, HorizontalAlignment.Right);
            listView.Columns.Add("Kosa Dužina", 115, HorizontalAlignment.Right);
            listView.Columns.Add("Horizontalna Dužina", 115, HorizontalAlignment.Right);
            listView.Columns.Add("dH", 75, HorizontalAlignment.Right);
            listView.Columns.Add("Visina Prizme", 105, HorizontalAlignment.Right);
            listView.Columns.Add("Visina Instrumenta", 105, HorizontalAlignment.Right);

        }
        public void ListView6ColFormat(ListView listView)
        {
            listView.Clear();
            listView.Columns.Add("Broj Tačke", 75, HorizontalAlignment.Left);
            listView.Columns.Add("Horizontalni Ugao", 115, HorizontalAlignment.Right);
            listView.Columns.Add("Horizontalna Dužina", 115, HorizontalAlignment.Right);
            listView.Columns.Add("dH", 75, HorizontalAlignment.Right);
            listView.Columns.Add("Visina Instrumenta", 105, HorizontalAlignment.Right);
            listView.Columns.Add("Visina Prizme", 105, HorizontalAlignment.Right);
            //listView.Columns.Remove(columnHeader3);
            //listView.Columns.Remove(columnHeader4);
            //columnHeader7.Text = "Visina Instrumenta";
            //columnHeader8.Text = "Visina Prizme";
        }

        private void InputIntoListView6Col(ListView listView, List<Format> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem lv = new ListViewItem(list[i].PointNumber.ToString());
                lv.SubItems.Add(list[i].HUgao.ToString("F4"));
                lv.SubItems.Add(list[i].HDuz.ToString("F3"));
                lv.SubItems.Add(list[i].DH.ToString("F3"));
                lv.SubItems.Add(list[i].VInstrument.ToString("F3"));
                lv.SubItems.Add(list[i].VPrizme.ToString("F3"));
                listView.Items.Add(lv);
            }
        }
    }
}
