using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TestLibrary.ClientEdit
{
    /// <summary>
    /// результат теста
    /// </summary>
    
    [XmlRoot("ClientResult")]
    public class ClientResult
    {
        [XmlAttribute("Mark")]
        public int Mark;

        [XmlAttribute("ClientBalls")]
        public int ClientBalls;

        [XmlAttribute("AllBalls")]
        public int AllBalls;

        [XmlAttribute("Percent")]
        public float Percent;

        public ClientResult() { }
        public ClientResult(int allBalls, int clientBalls, TestSettings settings)
        {

            this.ClientBalls = clientBalls;
            this.AllBalls = allBalls;

            this.Percent = ((float)this.ClientBalls)/((float)this.AllBalls) * 100;

            if (this.Percent >= 0 && this.Percent < (float)settings.Min3) { this.Mark = 2; }
            if (this.Percent >= (float)settings.Min3 && this.Percent < (float)settings.Min4) { this.Mark = 3; }
            if (this.Percent >= (float)settings.Min4 && this.Percent < (float)settings.Min5) { this.Mark = 4; }
            if (this.Percent >= (float)settings.Min5 && this.Percent <= 100) { this.Mark = 5; }
            if (this.Percent > 100)
            {
                this.Mark = 5;
            }
        }


    }
}
