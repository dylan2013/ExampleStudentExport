using System;
using System.Collections.Generic;
using System.Text;
using FISCA.DSAUtil;

namespace ExampleStudentExport
{
    public interface IExportConnector
    {      
        void SetSelectedFields(FieldCollection exportFields);
        void AddCondition(string argument);        
        ExportTable Export();
    }
}
