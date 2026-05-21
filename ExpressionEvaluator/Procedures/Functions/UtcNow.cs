using System;
using Vanderbilt.Biostatistics.Wfccm2;

namespace ExpressionEvaluator.Procedures.Functions
{
    internal class UtcNow : Function
    {
        public UtcNow(int precedance)
            : base("utcnow", precedance, 0, false)
        {
            _name2 = "UtcNow";
            Datetime = () => DateTime.UtcNow;
        }
    }
}
