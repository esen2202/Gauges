using System.Windows;
using System.Windows.Controls;

namespace BoardNesting.CustomControls
{
    public class BorderCustom : Border
    {
        static BorderCustom()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BorderCustom), new FrameworkPropertyMetadata(typeof(BorderCustom)));
        }
    }
}
