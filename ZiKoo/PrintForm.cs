using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace ZiKoo
{
    public partial class PrintForm : Form
    {
        ListView listView1 = new ListView();
        ListView listView2 = new ListView();

        public PrintForm(ListView lv1, ListView lv2)
        {
            InitializeComponent();
            listView1 = lv1;
            listView2 = lv2;
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            radioButton5.Checked = true;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 1)
                this.UpdatePrintPreview(null, null);
        }
        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            this.printPreviewControl1.Zoom = 2.0;
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            this.printPreviewControl1.Zoom = 1.0;
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            this.printPreviewControl1.Zoom = 0.5;
        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {
            this.printPreviewControl1.Zoom = 1.0;
            this.printPreviewControl1.AutoZoom = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.listViewPrinter1.PageSetup();
            this.UpdatePrintPreview(null, null);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.listViewPrinter1.PrintWithDialog();
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            int pages = (int)this.numericUpDown1.Value;

            switch (pages)
            {
                case 1:
                case 2:
                case 3:
                    this.printPreviewControl1.Rows = 1;
                    this.printPreviewControl1.Columns = pages;
                    break;
                default:
                    this.printPreviewControl1.Rows = 2;
                    this.printPreviewControl1.Columns = ((pages - 1) / 2) + 1;
                    break;
            }
        }

        private void UpdatePrintPreview(object sender, EventArgs e)
        {
            try
            {
                this.listViewPrinter1.Header = this.tbHeader.Text.Replace("\\t", "\t");
                this.listViewPrinter1.Footer = this.tbFooter.Text.Replace("\\t", "\t");
                this.listViewPrinter1.IsShrinkToFit = this.cbShrinkToFit.Checked;
                //this.listViewPrinter1.IsTextOnly = !this.cbListHeaderOnEveryPage.Checked;
                this.listViewPrinter1.IsListHeaderOnEachPage = this.cbListHeaderOnEveryPage.Checked;
                this.listViewPrinter1.IsPrintSelectionOnly = this.cbPrintSelection.Checked;

                this.ApplyMinimalFormatting();

                if (this.cbUseGridLines.Checked == false)
                    this.listViewPrinter1.ListGridPen = null;
                else
                {
                    this.listViewPrinter1.ListGridPen.Color = Color.Gray;
                }
                this.printPreviewControl1.InvalidatePreview();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Give the report a minimal set of default formatting values.
        /// </summary>
        public void ApplyMinimalFormatting()
        {
            this.listViewPrinter1.CellFormat = null;
            this.listViewPrinter1.ListFont = new Font("Tahoma", 9);

            this.listViewPrinter1.HeaderFormat = BlockFormat.Header();
            this.listViewPrinter1.HeaderFormat.TextBrush = Brushes.Black;
            this.listViewPrinter1.HeaderFormat.BackgroundBrush = null;
            this.listViewPrinter1.HeaderFormat.SetBorderPen(Sides.Bottom, new Pen(Color.Black, 0.5f));

            this.listViewPrinter1.FooterFormat = BlockFormat.Footer();
            this.listViewPrinter1.GroupHeaderFormat = BlockFormat.GroupHeader();
            Brush brush = new LinearGradientBrush(new Point(0, 0), new Point(200, 0), Color.Gray, Color.White);
            this.listViewPrinter1.GroupHeaderFormat.SetBorder(Sides.Bottom, 2, brush);

            this.listViewPrinter1.ListHeaderFormat = BlockFormat.ListHeader();
            this.listViewPrinter1.ListHeaderFormat.RightBorderColor = Color.Black;
            this.listViewPrinter1.ListHeaderFormat.RightBorderWidth = 1;
            this.listViewPrinter1.ListHeaderFormat.LeftBorderColor = Color.Black;
            this.listViewPrinter1.ListHeaderFormat.LeftBorderWidth = 1;
            this.listViewPrinter1.ListHeaderFormat.TopBorderColor = Color.Black;
            this.listViewPrinter1.ListHeaderFormat.TopBorderWidth = 1;
            this.listViewPrinter1.ListHeaderFormat.BottomBorderColor = Color.Black;
            this.listViewPrinter1.ListHeaderFormat.BottomBorderWidth = 1;
            this.listViewPrinter1.ListHeaderFormat.BackgroundBrush = null;
        }

        private void tbHeader_Leave(object sender, EventArgs e)
        {
            this.UpdatePrintPreview(null, null);
        }

        private void tbHeader_KeyDown(object sender, KeyEventArgs e)
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

        private void tbFooter_Leave(object sender, EventArgs e)
        {
            this.UpdatePrintPreview(null, null);
        }

        private void tbFooter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Up)
            {
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, false, true, true, true);
            }
            else if (e.KeyData == Keys.Down)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            listViewPrinter1.ListView = listView2;
            this.UpdatePrintPreview(null, null);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            listViewPrinter1.ListView = listView1;
            this.UpdatePrintPreview(null, null);
        }

        private void printPreviewControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        printPreviewControl1.StartPage++;
                        break;
                    case MouseButtons.Right:
                        if (printPreviewControl1.StartPage > 0)
                        {
                            printPreviewControl1.StartPage--;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void PrintForm_Resize(object sender, EventArgs e)
        {
            this.printPreviewControl1.AutoZoom = true;
            radioButton4.Checked = true;
        }
    }
}

