using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ListToTableDisplay.Core;

namespace ListToTableDisplay
{
    // for modern table outline, see https://en.wikipedia.org/wiki/Box-drawing_character
    // │└ ┘┌ ┐┬ ┴ ┼ ╭ ─ ╮ ╯ ╰

    public enum BorderStyle { Classic, Modern }
    public enum HeaderTextStyle { None, SplitPascalCase, SplitUnderline }

    public class ListToTableDisplay
    {
        private int _padding = 1;
        private BorderStyle _borderStyle;
        private HeaderTextStyle _headerTextStyle;

        private readonly char borderChar = '|';
        private readonly char headerChar = '┈';

        /// <summary>
        /// The padding on each side of the string value in the table cell. 
        /// <para>The default is 1, the minimum is 1, and the maximum is 10.</para>
        /// </summary>
        public int Padding
        {
            private get => _padding;
            set => _padding = value > 10 ? 10 : value < 1 ? 1 : value;
        }

        public BorderStyle BorderStyle
        {
            private get => _borderStyle;
            set => _borderStyle = value;
        }

        public HeaderTextStyle HeaderTextStyle
        {
            private get => _headerTextStyle;
            set => _headerTextStyle = value;
        }

        /// <summary>
        /// Constructs a new instance of the ListToTableDisplay class.
        /// </summary>
        public ListToTableDisplay()
        {
            BorderStyle = BorderStyle.Modern;
            HeaderTextStyle = HeaderTextStyle.None;
        }

        /// <summary>
        /// Display a list of objects in a table format.
        /// <para>📌 It is highly recommended that you use a monospaced font in order to ensure proper formatting of the table!</para>
        /// </summary>
        /// <param name="list">
        /// The generic list containing the data to display.
        /// 👉 Cast your list to an object: MyList.Cast<object>().ToList()
        /// </param>
        /// <returns>String.</returns>
        public string DisplayTable(List<object> list)
        {
            if (list.Count == 0) return "🚩 No data found!";

            Dictionary<string, Models.ListData> listDataDictionary = new Dictionary<string, Models.ListData>();

            // Use reflection to get a list of properties and extract the property name (key)
            // and build the model for each property.
            var obj = list.First();
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                string name = property.Name;

                Models.ListData ld = new Models.ListData();
                switch (HeaderTextStyle)
                {
                    case HeaderTextStyle.SplitPascalCase:
                        ld.DisplayName = System.Text.RegularExpressions.Regex.Replace(name, "(\\B[A-Z])", " $1");
                        break;
                    case HeaderTextStyle.SplitUnderline:
                        ld.DisplayName = name.Replace("_", " ");
                        break;
                    default:
                        ld.DisplayName = name;
                        break;
                }

                ld.OriginalName = name;
                ld.DataLength = ld.DisplayName.ToString().Length;

                listDataDictionary.Add(name, ld);
            }

            // Now for each property in the dictionary, we need to query the list of objects to find the maximum data length.
            // This is used to determine the width of the column.
            foreach (var item in listDataDictionary)
            {
                int maxLength = list.Select(o => o.GetType().GetProperty(item.Key).GetValue(o).ToString().Length).Max();
                if (maxLength > item.Value.DataLength) item.Value.DataLength = maxLength;
            }

            return TableStructure.BuildTable(listDataDictionary, Padding, BorderStyle, list);
        }
    }
}
