using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using SmartSchool.Common;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Formater;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Connector;
using SmartSchool.StudentRelated.RibbonBars.Export.ResponseHandler.Output;
using SmartSchool.StudentRelated;

namespace ExampleStudentExport
{
    public partial class ExportWizard : FISCA.Presentation.Controls.BaseForm
    {
        public ExportWizard()
        {
            InitializeComponent();


            #region 設定Wizard會跟著Style跑
            this.wizard1.HeaderStyle.ApplyStyle((GlobalManager.Renderer as Office2007Renderer).ColorTable.GetClass(ElementStyleClassKeys.RibbonFileMenuBottomContainerKey));
            this.wizard1.FooterStyle.BackColorGradientAngle = -90;
            this.wizard1.FooterStyle.BackColorGradientType = eGradientType.Linear;
            this.wizard1.FooterStyle.BackColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.wizard1.FooterStyle.BackColor2 = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.End;
            this.wizard1.BackColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.Start;
            this.wizard1.BackgroundImage = null;
            for (int i = 0; i < 5; i++)
            {
                (this.wizard1.Controls[1].Controls[i] as ButtonX).ColorTable = eButtonColor.OrangeWithBackground;
            }
            (this.wizard1.Controls[0].Controls[1] as System.Windows.Forms.Label).ForeColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.MouseOver.TitleText;
            (this.wizard1.Controls[0].Controls[2] as System.Windows.Forms.Label).ForeColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TitleText;
            #endregion
        }

        /// <summary>
        /// 畫面載入
        /// </summary>
        private void ExportWizard_Load(object sender, EventArgs e)
        {
            XmlElement element = StudentBulkProcess.GetExportDescription();
            BaseFieldFormater formater = new BaseFieldFormater();
            FieldCollection collection = formater.Format(element);

            foreach (Field field in collection)
            {
                ListViewItem item = listView.Items.Add(field.DisplayText);
                item.Tag = field;
                item.Checked = true;
            }
        }

        private void wizard1_CancelButtonClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void wizard1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            if (GetSelectedFields().Count == 0)
            {
                MsgBox.Show("必須至少選擇一項匯出欄位!", "欄位空白", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog1.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";
            saveFileDialog1.FileName = "匯出學生基本資料";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExportStudentConnector ec = new ExportStudentConnector();
                //取得所選學生的ID清單
                foreach (BriefStudentData student in SmartSchool.StudentRelated.Student.Instance.SelectionStudents)
                {
                    ec.AddCondition(student.ID);
                }

                ec.SetSelectedFields(GetSelectedFields());
                ExportTable table = ec.Export();

                ExportOutput output = new ExportOutput();
                output.SetSource(table);
                try
                {
                    output.Save(saveFileDialog1.FileName);
                }
                catch (Exception)
                {
                    MsgBox.Show("檔案儲存失敗, 檔案目前可能已經開啟。", "儲存失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (MsgBox.Show("檔案存檔完成，是否開啟該檔案", "是否開啟", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("開啟檔案發生失敗:" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                this.Close();
            }
        }

        /// <summary>
        /// 取得目前被選擇所要匯出的資料
        /// </summary>
        private FieldCollection GetSelectedFields()
        {
            FieldCollection collection = new FieldCollection();
            foreach (ListViewItem item in listView.CheckedItems)
            {
                Field field = item.Tag as Field;
                collection.Add(field);
            }
            return collection;
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.Items)
            {
                item.Checked = chkSelect.Checked;
            }
        }
    }
}