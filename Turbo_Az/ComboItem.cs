using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turbo_Az
{
   public class ComboItem
    {
        public ComboItem(string text, string value)
        {
            this.Text = text;
            this.Value = value;
        }

        public string Text { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
