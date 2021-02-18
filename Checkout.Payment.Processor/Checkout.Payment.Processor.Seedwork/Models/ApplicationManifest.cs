using System.Reflection;

namespace Checkout.Payment.Processor.Seedwork.Models
{
    public class ApplicationManifest
    {
        public string Name { get; set; }
        public string Version => Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}
