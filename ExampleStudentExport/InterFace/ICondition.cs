using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ExampleStudentExport
{
    public interface ICondition
    {        
        XmlElement GetConditionElement();
    }
}
