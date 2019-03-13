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

    public partial class ComPort : Form
    {

        public ComPort()
        {
            InitializeComponent();
        }

        public string BitPerSecound
        {
            get { return bitPerSecondCombo.Text; }
            set { bitPerSecondCombo.Text = value; }
        }
        public string DataBits
        {
            get { return dataBitsCombo.Text; }
            set { dataBitsCombo.Text = value; }
        }
        public string Parity
        {
            get { return parityCombo.Text; }
            set { parityCombo.Text = value; }
        }
        public string StopBits
        {
            get { return stopBitsCombo.Text; }
            set { stopBitsCombo.Text = value; }
        }
        public string FlowControl
        {
            get { return flowControlCombo.Text; }
            set { flowControlCombo.Text = value; }
        }



        private void ComPort_Load(object sender, EventArgs e)
        {
            bitPerSecondCombo.Items.Add(300);
            bitPerSecondCombo.Items.Add(600);
            bitPerSecondCombo.Items.Add(1200);
            bitPerSecondCombo.Items.Add(2400);
            bitPerSecondCombo.Items.Add(4800);
            bitPerSecondCombo.Items.Add(9600);
            bitPerSecondCombo.Items.Add(14400);
            bitPerSecondCombo.Items.Add(38400);
            bitPerSecondCombo.Items.Add(57600);
            bitPerSecondCombo.Items.Add(115200);
            
            dataBitsCombo.Items.Add(5);
            dataBitsCombo.Items.Add(6);
            dataBitsCombo.Items.Add(7);
            dataBitsCombo.Items.Add(8);

            parityCombo.Items.Add("None");
            parityCombo.Items.Add("Even");
            parityCombo.Items.Add("Mark");
            parityCombo.Items.Add("Odd");
            parityCombo.Items.Add("Space");

            stopBitsCombo.Items.Add("One");
            stopBitsCombo.Items.Add("OnePointFive");
            stopBitsCombo.Items.Add("Two");

            flowControlCombo.Items.Add("None");
            flowControlCombo.Items.Add("XOnXOff");
            flowControlCombo.Items.Add("RequestToSend");
            flowControlCombo.Items.Add("RequestToSendXOnXOff");

            try
            {
                XmlReader xmlReader = XmlReader.Create("comPort_config.dat");

                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {

                        if (xmlReader.Name == "bitPerSecond")
                            bitPerSecondCombo.Text = xmlReader.ReadElementContentAsString();

                        if (xmlReader.Name == "dataBits")
                            dataBitsCombo.Text = xmlReader.ReadElementContentAsString();

                        if (xmlReader.Name == "parity")
                            parityCombo.Text = xmlReader.ReadElementContentAsString();

                        if (xmlReader.Name == "stopBit")
                            stopBitsCombo.Text = xmlReader.ReadElementContentAsString();

                        if (xmlReader.Name == "flowControl")
                            flowControlCombo.Text = xmlReader.ReadElementContentAsString();
                    }
                }
                xmlReader.Close();
            }
            catch
            {
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bitPerSecondCombo.Text = bitPerSecondCombo.Items[5].ToString();
            dataBitsCombo.Text = dataBitsCombo.Items[3].ToString();
            parityCombo.Text = parityCombo.Items[0].ToString();
            stopBitsCombo.Text = stopBitsCombo.Items[0].ToString();
            flowControlCombo.Text = flowControlCombo.Items[0].ToString();
        }

        private void comPortOkBtn_Click(object sender, EventArgs e)
        {
            try
            {
                XmlWriter xmlWriter = XmlWriter.Create("comPort_config.dat");
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("comPort");
                xmlWriter.WriteStartElement("bitPerSecond");
                xmlWriter.WriteString(bitPerSecondCombo.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("dataBits");
                xmlWriter.WriteString(dataBitsCombo.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("parity");
                xmlWriter.WriteString(parityCombo.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("stopBit");
                xmlWriter.WriteString(stopBitsCombo.Text);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("flowControl");
                xmlWriter.WriteString(flowControlCombo.Text);
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
            catch
            {
            }

            this.Close();
        }

    }
}
