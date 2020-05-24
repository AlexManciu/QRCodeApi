using System.Collections.Generic;

namespace QRCodeData
{
    public class QRResponse
    {
        public string Type { get; set; }
        public List<Symbol> Symbol { get; set; }
    }
}
