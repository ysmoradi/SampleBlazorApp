using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleBlazorApp.Client.Pages.Components
{
    public class CascadingClass: ICascadingClass
    {
        public string Prop1 { get; set; }
        public int Prop2 { get; set; }
    }
}
