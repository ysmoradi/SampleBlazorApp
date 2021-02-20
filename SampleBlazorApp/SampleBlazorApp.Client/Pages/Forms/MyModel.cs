using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleBlazorApp.Client.Pages.Forms
{
    public class MyModel
    {
        [Required]
        public string Name { get; set; }

        public string Title { get; set; }

        public MySubModel SubModel { get; set; } = new MySubModel();
    }

    public class MySubModel
    {
        [Required]
        public string Address { get; set; }
    }
}