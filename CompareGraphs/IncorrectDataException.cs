using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareGraphs
{
    class IncorrectDataException : Exception
    {
        public override string Message
        {
            get
            {
                return "Incorrect data.";
            }
        }
    }
}
