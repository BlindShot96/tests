using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TestLibrary
{
    /// <summary>
    /// класс теста
    /// </summary>
    [XmlRoot("Test")]
    public  class Test:TestBase
    {
        public const int ATT = 1;
        public Test()
        {
        }
    }
}
