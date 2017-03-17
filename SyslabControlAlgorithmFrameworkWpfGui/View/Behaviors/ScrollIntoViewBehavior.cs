using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace SyslabControlAlgorithmFrameworkWpfGui.View.Behaviors
{
    public sealed class ScrollIntoViewBehavior : Behavior<ListView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += ScrollIntoView;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= ScrollIntoView;
            base.OnDetaching();
        }

        private void ScrollIntoView(object o, SelectionChangedEventArgs e)
        {
            if (o is ListView)
            {
                var listViewItems = (o as ListView).Items;
                if (listViewItems.Count == 0) return;

                //ListViewItem item = (ListViewItem)((ListView)o).ItemContainerGenerator.ContainerFromItem(((ListView)o).Items);
                if (listViewItems[listViewItems.Count - 1] is ListViewItem item) item.BringIntoView();
            }
        }
    }
}
