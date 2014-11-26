using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareGraphs
{
    class IncorrectDataException : Exception
    {
        public IncorrectDataException()
        {
            _message = "Incorrect data.";
        }
        public IncorrectDataException(string message)
        {
            _message = message;
        }
        private readonly string _message;
        public override string Message
        {
            get{return _message;}
        }
    }
}
