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
            ListView b = o as ListView;
            if (b == null) return;

            var listViewItems = b.Items;
            if (listViewItems.Count == 0) return;
            ListViewItem item = listViewItems[listViewItems.Count - 1] as ListViewItem;

            //ListViewItem item = (ListViewItem)((ListView)o).ItemContainerGenerator.ContainerFromItem(((ListView)o).Items);
            if (item != null) item.BringIntoView();
        }
    }
}
