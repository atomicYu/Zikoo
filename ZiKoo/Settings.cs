using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ZiKoo
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            try
            {
                XmlReader xmlReader = XmlReader.Create("config.xml");
                while (xmlReader.Read())
                {
                    if ((xmlReader.NodeType == XmlNodeType.Element))
                    {
                        if (xmlReader.Name == "path1")
                        {
                            textBox1.Text = xmlReader.ReadElementContentAsString();
                        }
                        if (xmlReader.Name == "path2")
                        {
                            textBox2.Text = xmlReader.ReadElementContentAsString();
                        }
                    }
                }
                xmlReader.Close();
            }
            catch
            {
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            try
            {
                XmlWriter xmlWriter = XmlWriter.Create("config.xml");
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("paths");
                xmlWriter.WriteStartElement("path1");
                xmlWriter.WriteString(textBox1.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("path2");
                xmlWriter.WriteString(textBox2.Text);
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = fbd.SelectedPath;
            }
        }


    }
}
