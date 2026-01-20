
namespace BitcoinApp.Client.Models
{
    public class GridDataAttributes
    {
        public int? RowNum { get; set; }
        public bool Checked { get; set; }
        public bool Disabled { get; set; }
        public bool CheckDisabled { get; set; }

        public static GridDataAttributes? CheckIsEnabled(bool isEnabled)
        {
            return isEnabled ? null : new GridDataAttributes
            {
                CheckDisabled = true
            };
        }
    }
}