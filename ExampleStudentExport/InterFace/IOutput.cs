using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleStudentExport
{
    public interface IOutput<T>
    {
        void SetSource(ExportTable dataSource);
        T GetOutput();
        void Save(string fileName);        
    }
}
