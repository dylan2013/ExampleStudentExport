using System;
using System.Collections.Generic;
using System.Text;
using SmartSchool.StudentRelated.RibbonBars.Export.RequestHandler;
using System.Xml;

namespace ExampleStudentExport
{
    public interface IResponseFormater
    {
        ExportFieldCollection Format(XmlElement element);
    }
}
