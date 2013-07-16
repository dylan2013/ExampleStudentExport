using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA;
using FISCA.Presentation;

namespace ExampleStudentExport
{
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {

            RibbonBarButton rbItemExport = K12.Presentation.NLDPanels.Student.RibbonBarItems["資料統計"]["匯出"];
            rbItemExport["學籍相關匯出"]["匯出學生基本資料_改"].Click += delegate
            {
                ExportWizard export = new ExportWizard();
                export.ShowDialog();
            };
        }
    }
}
