using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.PageData
{
    /// <summary>
    /// This little class is used to store the introduction data.
    /// This data gets used as a Template-class for the introduction-page.
    /// </summary>
    public class IntroductionData
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageUri { get; set; }
    }
}
