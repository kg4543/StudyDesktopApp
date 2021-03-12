using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyNotePadApp
{
    public partial class FrmMain : Form
    {
        public bool IsModify { get; set; }

        private const string firstFileName = "noname.txt";

        private string curFileName = firstFileName;


        public FrmMain()
        {
            InitializeComponent();
        }

        private void MnuNewFile_Click(object sender, EventArgs e)
        {
            // 만약 작업중인 파일이 있으면 저장 처리
            ProcessSaveFileBeforeClose();
            TxtMain.Text = "";
            IsModify = false;
            curFileName = firstFileName;
            this.Text = $"{curFileName} - 내 메모장";
        }

        private void MnuOpen_Click(object sender, EventArgs e)
        {
            ProcessSaveFileBeforeClose();
            DlgOpenText.ShowDialog();

            curFileName = DlgOpenText.FileName;
            this.Text = $"{curFileName}* - 내메모장";

            try
            {
                StreamReader sr = File.OpenText(curFileName);
                TxtMain.Text = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MnuSave_Click(object sender, EventArgs e)
        {
            if (curFileName == firstFileName)
            {
                if (DlgSaveText.ShowDialog() == DialogResult.OK)
                    curFileName = DlgSaveText.FileName;
            }
            StreamWriter sw = File.CreateText(curFileName);
            sw.WriteLine(TxtMain.Text);

            IsModify = false;
            sw.Close();
            this.Text = $"{curFileName}";
        }

        private void MnuExite_Click(object sender, EventArgs e)
        {
            ProcessSaveFileBeforeClose();
            Environment.Exit(0);
        }

        private void MnuCopy_Click(object sender, EventArgs e)
        {
            var contents = ActiveControl as RichTextBox;
            if (contents != null)
            {
                Clipboard.SetDataObject(contents.SelectedText);
                MessageBox.Show(contents.SelectedText);
            }
        }

        private void ProcessSaveFileBeforeClose()
        {
            if(IsModify==true)
            {
                DialogResult answer = MessageBox.Show("변경사항이 있습니다. 저장하시겠습니까?" , "저장",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(answer == DialogResult.Yes)
                {
                    if(curFileName == firstFileName)
                    {
                        if(DlgSaveText.ShowDialog() == DialogResult.OK)
                        {
                            StreamWriter sw = File.CreateText(DlgSaveText.FileName);
                            sw.WriteLine(TxtMain.Text);
                            sw.Close();
                        }
                        else
                        {
                            StreamWriter sw = File.CreateText(curFileName);
                            sw.WriteLine(TxtMain.Text);
                            sw.Close();
                        }
                    }
                }
            }
        }

        private void MnuPaste_Click(object sender, EventArgs e)
        {
            var contents = ActiveControl as RichTextBox;
            if (contents != null)
            {
                IDataObject data = Clipboard.GetDataObject();
                contents.SelectedText = data.GetData(DataFormats.Text).ToString();
                IsModify = true;
            }
        }

        private void MnuAbout_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("메모장 v1.0입니다.");
            var form = new AboutThis();
            form.ShowDialog();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = $"{curFileName} - 내 메모장";
            IsModify = false;
            DlgSaveText.Filter = DlgOpenText.Filter = "Text file (*.txt)|*.txt|Log file (*.log)|*.log";
        }

        private void TxtMain_TextChanged(object sender, EventArgs e)
        {
            IsModify = true;
            this.Text = $"{curFileName}* - 내메모장";
        }
    }
}
