using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleBlazorApp.Client.Pages.Components
{
    public interface ICascadingClass
    {
        string Prop1 { get; set; }
        int Prop2 { get; set; }
    }
}
