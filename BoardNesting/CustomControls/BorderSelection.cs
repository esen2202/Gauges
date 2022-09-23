using System.Windows;
using System.Windows.Controls;

namespace BoardNesting.CustomControls
{
    public class BorderSelection : Border
    {
        static BorderSelection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BorderSelection), new FrameworkPropertyMetadata(typeof(BorderSelection)));
        }
    }
}
