using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ZiKooLibrary
{
    public class SaveProcessor
    {
        public static void WriteRawListViewIntoFile(ListView listView)
        {
            string path1 = null;

            ReadFromConfig(path1);

            SaveFileDialog sf = new SaveFileDialog();

            string filename = "";
            sf.InitialDirectory = path1;
            sf.Title = "Sačuvaj";
            try
            {
                if (listView.Items.Count != 0)
                {


                    if (listView.Items[0].SubItems.Count == 6)
                    {
                        sf.Filter = "ASC File|*.asc|Text File|*.txt";
                        if (sf.ShowDialog() == DialogResult.OK)
                        {
                            filename = sf.FileName;

                            if (filename != "")
                            {
                                StreamWriter sw = new StreamWriter(filename);
                                {

                                    foreach (ListViewItem item in listView.Items)
                                    {
                                        sw.WriteLine(item.SubItems[0].Text.PadLeft(4) + "    " + item.SubItems[1].Text.PadLeft(8) + string.Empty.PadLeft(4) +
                                            item.SubItems[2].Text.PadLeft(7) + "    " + item.SubItems[3].Text.PadLeft(7) + "    "
                                            + item.SubItems[4].Text.PadLeft(5) + "    " + item.SubItems[5].Text.PadLeft(6));

                                    }
                                }
                                sw.Close();
                            }
                        }

                    }
                    else if (listView.Items[0].SubItems.Count == 8)
                    {
                        sf.Filter = "DAT File|*.dat|Text File|*.txt";
                        if (sf.ShowDialog() == DialogResult.OK)
                        {
                            filename = sf.FileName;

                            if (filename != "")
                            {
                                StreamWriter sw = new StreamWriter(filename);
                                {

                                    foreach (ListViewItem item in listView.Items)
                                    {
                                        sw.WriteLine(item.SubItems[0].Text + " " + item.SubItems[1].Text + " " + item.SubItems[2].Text + " " +
                                            item.SubItems[3].Text + " " + item.SubItems[4].Text + " " + item.SubItems[5].Text + " "
                                            + item.SubItems[6].Text + " " + item.SubItems[7].Text);

                                    }
                                }
                                sw.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void WriteCooListViewIntoFile(ListView listView)
        {
            string path2 = null;
            ReadFromConfig(path2);

            SaveFileDialog sf = new SaveFileDialog();

            string filename = "";
            sf.InitialDirectory = path2;
            sf.Filter = "KOO File|*.koo";
            sf.Title = "Sačuvaj";
            try
            {
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    filename = sf.FileName;

                    if (filename != "")
                    {
                        StreamWriter sw = new StreamWriter(filename);
                        {
                            foreach (ListViewItem item in listView.Items)
                            {
                                sw.WriteLine(item.SubItems[0].Text.PadLeft(4) + "    " + item.SubItems[1].Text.PadLeft(8) + string.Empty.PadLeft(4) +
                                    item.SubItems[2].Text + "    " + item.SubItems[3].Text.PadLeft(7));

                            }
                            sw.Close();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void ReadFromConfig(string path)
        {
            try
            {
                XmlReader xmlReader = XmlReader.Create("config.xml");
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlReader.Name == "path")
                        {
                            path = xmlReader.ReadElementContentAsString();
                        }
                    }
                }
                xmlReader.Close();
            }
            catch
            {
            }
        }
    }
}
