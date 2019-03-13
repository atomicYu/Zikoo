using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Globalization;
using System.Threading;
using ZiKooLibrary;

namespace ZiKoo
{

    public partial class Form1 : Form
    {
        //public static Form1 form1;
        List<Format> unformattedList = new List<Format>();
        List<Format> formattedList = new List<Format>();
        List<Format> orientationReadingsList = new List<Format>();
        List<Format> calcPointsList = new List<Format>();
        List<Format> orientationsList = new List<Format>();
        List<Coordinate> cooOrientationsList = new List<Coordinate>();
        List<Coordinate> coordinatesList = new List<Coordinate>();
        //int orjListCount = -1;

        Stack<Memento> undos = new Stack<Memento>();
        Stack<Memento> redos = new Stack<Memento>();
        Originator org = new Originator();
        Memento undo;

        string row = String.Empty;
        string InputData = String.Empty;
        delegate void SetTextCallback(string text);

        public Form1()
        {
            InitializeComponent();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived);
            //form1 = this;
        }

        #region obrada i praznjenje

        private void ResetToDef()
        {
            unformattedList.Clear();
            //orjListCount = -1;
            orientationReadingsList.Clear();
            calcPointsList.Clear();
            orientationsList.Clear();
            cooOrientationsList.Clear();
            row = String.Empty;
            InputData = String.Empty;

            newStationBtn.Enabled = false;
            addPointsBtn.Enabled = false;
            calculateBtn.Enabled = false;
            formatBtn.Enabled = false;
            formatirajToolStripMenuItem.Enabled = false;
            labelReadings.Text = "";
            tbStationNumber.Clear();
            tbOrientationNumber.Clear();
            mtbStationY.Clear();
            mtbStationX.Clear();
            mtbStationZ.Clear();
            mtbOrientationY.Clear();
            mtbOrientationX.Clear();
            mtbOrientationZ.Clear();

            listView2.Items.Clear();

            openPortBtn.Text = "    Otvori port";
            openPortBtn.Image = Properties.Resources.yes;
            serialPort1.Close();
        }
        private void OpenBtn_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem.PerformClick();
        }

        private void FormatBtn_Click(object sender, EventArgs e)
        {
            formatirajToolStripMenuItem.PerformClick();
        }
        private void FormatirajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewProcessor lvP = new ListViewProcessor();
            lvP.ListViewFormatAndData(listView1, unformattedList);
            FillMemento();

            formatBtn.Enabled = false;
            formatirajToolStripMenuItem.Enabled = false;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewProcessor lvP = new ListViewProcessor();
            OpenProcessor op = new OpenProcessor();
            DialogResult dRes;
            if (listView1.Items.Count != 0 || listView2.Items.Count != 0)
            {
                dRes = MessageBox.Show("Prije nego što otvorite novi projekat, uvjerite se da ste sačuvali izmjene, u protivnom svi podaci će biti izgubljeni!\n\n" +
                    "Da li želite da otvorite novi projekat?", "Obavještenje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dRes == DialogResult.Yes)
                {
                    ResetToDef();
                    lvP.ListView8ColFormat(listView1);
                }

                if (dRes == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                op.LoadFileIntoListView(listView1);

                if (listView1.Columns.Count == 8)
                {
                    formatBtn.Enabled = true;
                    formatirajToolStripMenuItem.Enabled = true;
                }

                if (listView1.Columns.Count == 6)
                {
                    FillMemento();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProcessor.WriteRawListViewIntoFile(listView1);
        }
        private void SaveCoordinatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProcessor.WriteCooListViewIntoFile(listView2);

        }

        public void PortNames()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(ports);
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception)
            {

            }
        }
        private void RefreshPortNameBtn_Click(object sender, EventArgs e)
        {
            PortNames();           
        }
        private void OpenPortBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (openPortBtn.Text == "    Otvori port")
                {
                    openPortBtn.Text = "     Zatvori port";
                    openPortBtn.Image = Properties.Resources.no;
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                }
                else if (openPortBtn.Text == "     Zatvori port")
                {
                    openPortBtn.Image = Properties.Resources.yes;
                    openPortBtn.Text = "    Otvori port";
                    serialPort1.Close();
                    row = string.Empty;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetCOMPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComPort cp = new ComPort();
            if (openPortBtn.Text == "Zatvori port")
            {
                MessageBox.Show("Morate zatvoriti COM port!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    cp.BitPerSecound = serialPort1.BaudRate.ToString();
                    cp.DataBits = serialPort1.DataBits.ToString();
                    cp.StopBits = serialPort1.StopBits.ToString();
                    cp.FlowControl = serialPort1.Handshake.ToString();
                    cp.Parity = serialPort1.Parity.ToString();

                    cp.ShowDialog();
                    serialPort1.BaudRate = Convert.ToInt32(cp.BitPerSecound);
                    serialPort1.DataBits = Convert.ToInt32(cp.DataBits);
                    serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cp.StopBits);
                    serialPort1.Handshake = (Handshake)Enum.Parse(typeof(Handshake), cp.FlowControl);
                    serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cp.Parity);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            InputData = serialPort1.ReadExisting();
            if (InputData != String.Empty)
            {
                this.BeginInvoke(new SetTextCallback(SetText), new object[] { InputData });

            }
        }
        string redak = String.Empty;
        private void SetText(string text)
        {
            try
            {
                row += text;
                if (row.Length > 130)
                {
                    StringReader sr = new StringReader(row);
                    redak = sr.ReadLine();


                    if (redak != String.Empty)
                    {
                        List<string> red = redak.Split(new char[] { ' ' }).ToList();
                        for (int i = 0; i < red.Count; i++)
                        {
                            if (red[i] == " " || red[i] == "")
                            {
                                red.Remove(red[i]);
                            }
                        }
                        row = row.Remove(0, redak.Length + 1);
                        redak = String.Empty;
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

                            listView1.Items.Add(lv);

                        }

                    }
                    formatBtn.Enabled = true;
                    formatirajToolStripMenuItem.Enabled = true;
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CultureInfo cult = CultureInfo.CreateSpecificCulture("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cult;
            CultureInfo.DefaultThreadCurrentUICulture = cult;
            Thread.CurrentThread.CurrentCulture = cult;
            Thread.CurrentThread.CurrentUICulture = cult;

            PortNames();

            editbox.Parent = listView1;
            editbox.Hide();
            editbox.LostFocus += new EventHandler(Editbox_LostFocus);
            listView1.MouseDoubleClick += new MouseEventHandler(ListView1_MouseDoubleClick);
            changeToolStripMenuItem.Click += new EventHandler(ChangeToolStripMenuItem_Click);
            editbox.KeyDown += new KeyEventHandler(Editbox_KeyDown);
            editbox.KeyPress += new KeyPressEventHandler(Editbox_KeyPress);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = new DialogResult();
            if (listView1.Items.Count == 0 && listView2.Items.Count == 0 && calcPointsList.Count == 0)
            {
                serialPort1.Close();
            }
            else
            {
                dr = MessageBox.Show("Da li želite napustiti program?", "ZiKOO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    serialPort1.Close();
                }
                else if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0 && editbox.Focused == false)
            {
                FillMemento();
                foreach (ListViewItem lv in listView1.SelectedItems)
                {
                    lv.Remove();
                }
                
            }
        }

        private void AddOrientationReading1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                try
                {
                    orientationReadingsList.Add(new Format(Format.AngleInDec(Convert.ToDouble(listView1.SelectedItems[0].SubItems[1].Text)),
                        Convert.ToDouble(listView1.SelectedItems[0].SubItems[2].Text),
                        Convert.ToDouble(listView1.SelectedItems[0].SubItems[3].Text)));
                    labelReadings.Text += listView1.SelectedItems[0].SubItems[0].Text + ", ";
                    FillMemento();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Dodajte čitanja na orijentaciju pojedinačno!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddOrientationReading2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                try
                {
                    double ugao = Convert.ToDouble(listView1.SelectedItems[0].SubItems[1].Text) + 180;
                    if (ugao > 360)
                    {
                        ugao = ugao - 360;
                    }

                    orientationReadingsList.Add(new Format(Format.AngleInDec(ugao), Convert.ToDouble(listView1.SelectedItems[0].SubItems[2].Text),
                        Convert.ToDouble(listView1.SelectedItems[0].SubItems[3].Text)));
                    labelReadings.Text += listView1.SelectedItems[0].SubItems[0].Text + ", ";
                    FillMemento();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Dodajte čitanja na orijentaciju pojedinačno!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddOrientationBtn_Click(object sender, EventArgs e)
        {

            if (orientationReadingsList.Count == 0)
            {
                MessageBox.Show("Prvo morate pokazati opažanja na orijentacije.\nMarkirajte orijentaciju na listi, pa kliknite desni taster miša.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                double err = 0;
                DialogResult res = new DialogResult();
                for (int i = 0; i < orientationReadingsList.Count; i++)
                {
                    err = Math.Abs(orientationReadingsList[0].HUgao - orientationReadingsList[i].HUgao);
                    if (err > 0.017)
                    {
                        res = MessageBox.Show("Razlike u čitanjima su veća od 1'! \n Želite li da nastavite računanje?", "Upozorenje!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }
                }
                if (res == DialogResult.Yes || err < 0.017)
                {
                    if (mtbOrientationY.Text == "" || mtbOrientationX.Text == "" || mtbOrientationZ.Text == "")
                    {
                        MessageBox.Show("Unesite oznaku i koordinate orijentacije!");
                    }
                    else
                    {
                        try
                        {
                            //orjListCount++;
                            orientationsList.Add(Format.AverageReadings(orientationReadingsList));
                            string brojT = tbOrientationNumber.Text;
                            int i = 0;
                            if (tbOrientationNumber.Text == "")
                            {
                                i++;
                                brojT = "O" + i.ToString();
                            }
                            cooOrientationsList.Add(new Coordinate(brojT, Convert.ToDouble(mtbOrientationY.Text), Convert.ToDouble(mtbOrientationX.Text), Convert.ToDouble(mtbOrientationZ.Text)));
                            addPointsBtn.Enabled = true;
                            addOrientationBtn.Enabled = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }

                    }
                }
                else
                {
                    labelReadings.Text = "";
                    orientationReadingsList.Clear();
                }
                FillMemento();
            }
        }

        private void NewStationBtn_Click_1(object sender, EventArgs e)
        {
            //orjListCount = -1;
            orientationReadingsList.Clear();
            calcPointsList.Clear();
            orientationsList.Clear();
            cooOrientationsList.Clear();

            newStationBtn.Enabled = false;
            addPointsBtn.Enabled = false;
            calculateBtn.Enabled = false;

            labelReadings.Text = "";
            tbStationNumber.Clear();
            tbOrientationNumber.Clear();
            mtbStationY.Clear();
            mtbStationX.Clear();
            mtbStationZ.Clear();
            mtbOrientationY.Clear();
            mtbOrientationX.Clear();
            mtbOrientationZ.Clear();

            addOrientationToolStripMenuItem1.Enabled = true;
            FillMemento();
        }

        private void AddPointsBtn_Click_1(object sender, EventArgs e)
        {
            bool sw = false;
            if (listView1.SelectedItems.Count != 0)
            {

                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    try
                    {
                        calcPointsList.Add(new Format(listView1.SelectedItems[i].SubItems[0].Text, Format.AngleInDec(Convert.ToDouble(listView1.SelectedItems[i].SubItems[1].Text)).ToString(),
                       listView1.SelectedItems[i].SubItems[2].Text, listView1.SelectedItems[i].SubItems[3].Text,
                       listView1.SelectedItems[i].SubItems[4].Text, listView1.SelectedItems[i].SubItems[5].Text));

                    }
                    catch (Exception)
                    {
                        sw = true;
                        MessageBox.Show("Ulazni podaci nisu odgovarajućeg tipa!\nUvjerite se da li su podaci zapisani u obliku decimalnog broja.\nKolona 'Broj Tačke' pored brojeva može sadržavati i velika slova.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                if (sw == false)
                {
                    foreach (ListViewItem item in listView1.SelectedItems)
                    {
                        listView1.Items.Remove(item);
                    }
                    calculateBtn.Enabled = true;
                }
                FillMemento();
            }
            else
            {
                MessageBox.Show("Odaberite željene tačke sa tahimetrijske liste! ", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CalculateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                CoordinateCalculator cc = new CoordinateCalculator();
                cc.CooCalculator(listView2, calcPointsList, orientationsList,
                    mtbStationY.Text, mtbStationX.Text, mtbStationZ.Text,
                    mtbOrientationY.Text, mtbOrientationX.Text);

            }
            catch (Exception)
            {
                MessageBox.Show("Ulazni podaci nisu dobri!\nProvjerite da li ste unijeli podatke stajališta i orijentacije.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (listView2.Items.Count != 0)
            {
                saveCoordinatesToolStripMenuItem.Enabled = true;
            }
            newStationBtn.Enabled = true;
            calcPointsList.Clear();
            FillMemento();
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Selected = true;
            }
        }

        #region maskedTextBox format

        private void MaskedTextBox2_MouseClick(object sender, MouseEventArgs e)
        {
            string b = mtbStationY.Text.Remove(mtbStationY.Text.IndexOf('.'), 1);
            int a = b.TrimStart().Length;
            if (a == 0)
            {
                mtbStationY.Select(a, 0);
            }

        }

        private void MaskedTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsDigit(e.KeyChar) && e.KeyChar!= '.' && e.KeyChar!=(char)Keys.Back)
            //{
            //    e.Handled = true;
            //}
            if ((e.KeyChar == '.') && ((sender as MaskedTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

            }

        }

        private void MaskedTextBox4_TextChanged(object sender, EventArgs e)
        {
            if ((System.Text.RegularExpressions.Regex.IsMatch(mtbStationZ.Text, "[^0-9^.]")) || (mtbStationZ.Text.Length > 7))
            {
                mtbStationZ.Text = mtbStationZ.Text.Remove(mtbStationZ.Text.Length - 1);
                mtbStationZ.Select(mtbStationZ.Text.Length, 0);
            }

        }

        private void MaskedTextBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '.') && ((sender as MaskedTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void MaskedTextBox8_TextChanged(object sender, EventArgs e)
        {
            if ((System.Text.RegularExpressions.Regex.IsMatch(mtbOrientationZ.Text, "[^0-9^.]")) || (mtbOrientationZ.Text.Length > 7))
            {
                mtbOrientationZ.Text = mtbOrientationZ.Text.Remove(mtbOrientationZ.Text.Length - 1);
                mtbOrientationZ.Select(mtbOrientationZ.Text.Length, 0);
            }
        }

        private void MaskedTextBox3_MouseClick(object sender, MouseEventArgs e)
        {
            string b = mtbStationX.Text.Remove(mtbStationX.Text.IndexOf('.'), 1);
            int a = b.TrimStart().Length;
            if (a == 0)
            {
                mtbStationX.Select(a, 0);
            }
        }

        private void MaskedTextBox6_MouseClick(object sender, MouseEventArgs e)
        {
            string b = mtbOrientationY.Text.Remove(mtbOrientationY.Text.IndexOf('.'), 1);
            int a = b.TrimStart().Length;
            if (a == 0)
            {
                mtbOrientationY.Select(a, 0);
            }
        }

        private void MaskedTextBox7_MouseClick(object sender, MouseEventArgs e)
        {
            string b = mtbOrientationX.Text.Remove(mtbOrientationX.Text.IndexOf('.'), 1);
            int a = b.TrimStart().Length;
            if (a == 0)
            {
                mtbOrientationX.Select(a, 0);
            }
        }
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Down)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void MaskedTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Down)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
            else if (e.KeyData == Keys.Up)
            {
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void MaskedTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Down)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
            else if (e.KeyData == Keys.Up)
            {
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void MaskedTextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Down)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(groupBox1, true, true, true, true);
            }
            else if (e.KeyData == Keys.Up)
            {
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void TextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Down)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
            else if (e.KeyData == Keys.Up)
            {
                SelectNextControl(newStationBtn, false, true, true, true);
            }
        }

        private void MaskedTextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Down)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
            else if (e.KeyData == Keys.Up)
            {
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void MaskedTextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Down)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
            else if (e.KeyData == Keys.Up)
            {
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void MaskedTextBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, true, true, true);
            }
            else if (e.KeyData == Keys.Up)
            {
                SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        #endregion

        private ListViewHitTestInfo hitinfo;
        private TextBox editbox = new TextBox();

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MouseHit(e);
        }
        private void MouseHit(MouseEventArgs e)
        {
            hitinfo = listView1.HitTest(e.X, e.Y);
            editbox.Bounds = hitinfo.SubItem.Bounds;
            editbox.Text = hitinfo.SubItem.Text;
            if (editbox.Bounds.X == 0)
            {
                editbox.TextAlign = HorizontalAlignment.Left;
            }
            else
            {
                editbox.TextAlign = HorizontalAlignment.Right;
            }
            editbox.Focus();
            editbox.Show();
        }

        void Editbox_LostFocus(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(editbox.Text, "[^0-9^.^A-Z]") && editbox.Text[editbox.Text.Length - 1] != '.')
            {
                hitinfo.SubItem.Text = editbox.Text;
                FillMemento();
            }
            editbox.Hide();
        }
        void Editbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        void Editbox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                editbox.Hide();
            }

        }
        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            hitinfo = listView1.HitTest(e.X, e.Y);
        }

        private void ChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editbox.Bounds = hitinfo.SubItem.Bounds;
            editbox.Text = hitinfo.SubItem.Text;
            if (editbox.Bounds.X == 0)
            {
                editbox.TextAlign = HorizontalAlignment.Left;
            }
            else
            {
                editbox.TextAlign = HorizontalAlignment.Right;
            }
            editbox.Focus();
            editbox.Show();
        }

        private void AddOrientationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 1)
            {
                tbOrientationNumber.Text = listView2.SelectedItems[0].SubItems[0].Text;
                mtbOrientationY.Text = listView2.SelectedItems[0].SubItems[1].Text;
                mtbOrientationX.Text = listView2.SelectedItems[0].SubItems[2].Text;
                mtbOrientationZ.Text = listView2.SelectedItems[0].SubItems[3].Text;
                FillMemento();
            }
            else
            {
                MessageBox.Show("Morate markirati samo jednu tačku", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Delete2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count != 0)
            {
                FillMemento();
                foreach (ListViewItem item in listView2.SelectedItems)
                {
                    item.Remove();
                }
                
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewProcessor.CopyListView(listView2);
        }

        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                ListViewProcessor.CopyListView(listView1);
            }

        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Info inf = new Info();
            inf.ShowDialog();
        }

        #region UndoRedo
        private void FillMemento()
        {
            ListViewProcessor lvP = new ListViewProcessor();
            lvP.InputInFormattedList(listView1, formattedList);
            lvP.InputInCoordinateList(listView2, coordinatesList);

            undo = org.SetData(formattedList, coordinatesList, orientationReadingsList, calcPointsList, orientationsList, cooOrientationsList,
                formatBtn.Enabled, newStationBtn.Enabled, addPointsBtn.Enabled, calculateBtn.Enabled, addOrientationBtn.Enabled, labelReadings.Text);
            undos.Push(undo);
            redos.Clear();
            undoToolStripMenuItem.Enabled = true;
            redoToolStripMenuItem.Enabled = false;
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undos.Count > 1)
            {
                ListViewProcessor lvP = new ListViewProcessor();

                redos.Push(undos.Pop());
                org.Undo(undos.Peek());
                formattedList = org.GetlistView1();
                lvP.ListViewFormattedListInput(listView1, formattedList);
                coordinatesList = org.GetlistView2();
                lvP.ListViewCoordinateInput(listView2, coordinatesList);
                orientationReadingsList = org.GetOrientationReadings();
                calcPointsList = org.GetCalcPointsList();
                orientationsList = org.GetOrientations();
                cooOrientationsList = org.GetCooOrientationsList();
                formatBtn.Enabled = org.GetFormatBtn();
                formatirajToolStripMenuItem.Enabled = org.GetFormatBtn();
                newStationBtn.Enabled = org.GetNewStationBtn();
                addPointsBtn.Enabled = org.GetAddPointsBtn();
                calculateBtn.Enabled = org.GetCalculateBtn();
                addOrientationBtn.Enabled = org.GetAddOrientationBtn();
                labelReadings.Text = org.GetLabelReadings();
                redoToolStripMenuItem.Enabled = true;
            }
            else
            {
                undoToolStripMenuItem.Enabled = false;
            }
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (redos.Count > 0)
            {
                ListViewProcessor lvP = new ListViewProcessor();

                undos.Push(redos.Peek());
                org.Undo(redos.Pop());

                formattedList = org.GetlistView1();
                lvP.ListViewFormattedListInput(listView1, formattedList);
                coordinatesList = org.GetlistView2();
                lvP.ListViewCoordinateInput(listView2, coordinatesList);

                orientationReadingsList = org.GetOrientationReadings();
                calcPointsList = org.GetCalcPointsList();
                orientationsList = org.GetOrientations();
                cooOrientationsList = org.GetCooOrientationsList();
                formatBtn.Enabled = org.GetFormatBtn();
                formatirajToolStripMenuItem.Enabled = org.GetFormatBtn();
                newStationBtn.Enabled = org.GetNewStationBtn();
                addPointsBtn.Enabled = org.GetAddPointsBtn();
                calculateBtn.Enabled = org.GetCalculateBtn();
                addOrientationBtn.Enabled = org.GetAddOrientationBtn();
                labelReadings.Text = org.GetLabelReadings();
                undoToolStripMenuItem.Enabled = true;
            }
            else
            {
                redoToolStripMenuItem.Enabled = false;
            }
        }
        #endregion

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintForm pForm = new PrintForm(this.listView1, this.listView2);
            pForm.ShowDialog();
        }

        private void DefaultFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings set = new Settings();
            set.ShowDialog();
        }     
    }
}

