using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestLibrary
{
    /// <summary>
    /// маска для  выставления оценки
    /// </summary>
    public class ResultMask
    {
        /// <summary>
        /// словарь оценка - макс процент
        /// </summary>
        public List<MP> Mask { get;set; }

        public void AddMask(int Mark, float Percent)
        {
            if (Percent > 100 || Percent < 0) { throw new ArgumentException(); }

        }

        public void SortByProcent()
        {
            for (int i = 0; i < Mask.Count; i++)
            {
                for (int j = i; j < Mask.Count; j++)
                {
                    if (Mask[i].Percent <= Mask[j].Percent)
                    {
                        MP buf = Mask[i];
                        Mask[i] = Mask[j];
                        Mask[j] = buf;
                    }
                }
            }
        }
        public void SortByMark()
        {
            for (int i = 0; i < Mask.Count; i++)
            {
                for (int j = i; j < Mask.Count; j++)
                {
                    if (Mask[i].Mark <= Mask[j].Mark)
                    {
                        MP buf = Mask[i];
                        Mask[i] = Mask[j];
                        Mask[j] = buf;
                    }
                }
            }
        }
    }

    public class MP
    {
        public float Percent;
        public int Mark;    
    }
}
