using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ZiKooLibrary
{
    public class OpenProcessor
    {
        public void LoadFileIntoListView(ListView listView)
        {
            ListViewProcessor lvP = new ListViewProcessor();

            OpenFileDialog ofg = new OpenFileDialog
            {
                Title = "Otvori",
                Filter = "DAT File|*.dat|ASC File|*.asc|Text File|*.txt"
            };

            if (ofg.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofg.FileName);
                int j = 0;

                while (sr.Peek() != -1)
                {
                    string raw = sr.ReadLine();

                    while (raw.Contains("  ") || raw.Contains("\t"))
                    {
                        raw = System.Text.RegularExpressions.Regex.Replace(raw, "  ", " ");
                        raw = System.Text.RegularExpressions.Regex.Replace(raw, "\t", " ");
                    }

                    List<string> red = raw.Split(new char[] { ' ' }).ToList();
                    for (int i = 0; i < red.Count; i++)
                    {
                        if (red[i] == " " || red[i] == "")
                        {
                            red.Remove(red[i]);
                        }
                    }

                    if (red.Count == 8)
                    {
                        ListViewItem lv = new ListViewItem(red[0]);
                        lv.SubItems.Add(red[1]);
                        lv.SubItems.Add(red[2]);
                        lv.SubItems.Add(red[3]);
                        lv.SubItems.Add(red[4]);
                        lv.SubItems.Add(red[5]);
                        lv.SubItems.Add(red[6]);
                        lv.SubItems.Add(red[7]);

                        listView.Items.Add(lv);

                        int a = listView.Columns.Count;

                    }
                    else if (red.Count == 6)
                    {

                        if (j == 0)
                        {
                            lvP.ListView6ColFormat(listView);
                        }

                        ListViewItem lv = new ListViewItem(red[0]);
                        lv.SubItems.Add(red[1]);
                        lv.SubItems.Add(red[2]);
                        lv.SubItems.Add(red[3]);
                        lv.SubItems.Add(red[4]);
                        lv.SubItems.Add(red[5]);

                        listView.Items.Add(lv);
                        j++;
                    }

                    //else
                    //{
                    //    MessageBox.Show("Ulazni podaci nisu odgovarajućeg tipa!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
                sr.Close();
            }
        }
    }
}
