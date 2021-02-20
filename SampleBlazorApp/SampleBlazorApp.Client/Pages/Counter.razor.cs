using Microsoft.AspNetCore.Components;

namespace SampleBlazorApp.Client.Pages
{
    public partial class Counter
    {
        public int CurrentCount { get; set; }

        public void IncrementCount()
        {
            CurrentCount++;
        }
    }
}
