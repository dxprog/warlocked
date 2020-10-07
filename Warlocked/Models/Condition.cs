using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlocked.Models
{
    public class Condition
    {
        // TODO Convert to enum, probably
        public string Entity { get; set; }
        public string Comparator { get; set; }
        public int Value { get; set; }

        public bool ConditionMet()
        {
            return false;
        }
    }
}
