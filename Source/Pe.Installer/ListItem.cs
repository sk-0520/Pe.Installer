using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pe.Installer
{
    public class ListItem<T>
    {
        public ListItem(string display, T value)
        {
            Display = display;
            Value = value;
        }

        #region property

        public string Display { get; }

        public T Value { get; }

        #endregion
    }

    public class PlatformListItem: ListItem<string>
    {
        public PlatformListItem(string display, string value)
            : base(display, value)
        { }
    }
}
