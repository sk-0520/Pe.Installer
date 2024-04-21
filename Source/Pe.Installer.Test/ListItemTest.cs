using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pe.Installer.Test
{
    public class ListItemTest_String
    {
        [Theory]
        [InlineData("display", "value*")]
        public void ConstructorTest(string display, string value) {
            var test = new ListItem<string>(display, value);
            Assert.Equal(display, test.Display);
            Assert.Equal(value, test.Value);
        }
    }
}
