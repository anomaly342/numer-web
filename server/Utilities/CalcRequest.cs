using System.ComponentModel;
using System.Configuration;

namespace server.Utilities
{
    public class CalcRequest
    {
        [DefaultValue(38.0)]
        public required double findee { get; set; }
        [DefaultValue(2)]
        public required int n { get; set; }
        [DefaultValue(2.0)]
        public required double a { get; set; }
        [DefaultValue(100)]
        public required double b { get; set; }
        [DefaultValue(100000)]
        public required int maxIteration { get; set; }
        public void Deconstruct(out double findee, out int n, out double a, out double b, out int maxIteration)
        {
            findee = this.findee;
            n = this.n;
            a = this.a;
            b = this.b;
            maxIteration = this.maxIteration;


        }
    }
}