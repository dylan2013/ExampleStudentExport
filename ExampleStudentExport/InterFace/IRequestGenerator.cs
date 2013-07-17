using System;
using System.Collections.Generic;
using System.Text;
using FISCA.DSAUtil;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Generator.Condition;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler.Generator.Orders;

namespace ExampleStudentExport
{
    public interface IRequestGenerator
    {
        void AddCondition(ICondition condition);
        void AddOrder(Order order);
        void SetSelectedFields(FieldCollection selectedFields);
        DSRequest Generate();
    }
}
