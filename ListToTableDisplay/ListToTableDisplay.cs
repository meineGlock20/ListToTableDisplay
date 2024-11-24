using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ListToTableDisplay.Core;

namespace ListToTableDisplay
{
    public class ListToTableDisplay
    {
        private int _padding = 1;
        private BorderStyle _borderStyle;
        private HeaderTextStyle _headerTextStyle;

        /// <summary>
        /// Sets the padding on each side of the string value in the table cell. 
        /// <para>The default is 1, the minimum is 1, and the maximum is 10.</para>
        /// </summary>
        public int Padding
        {
            private get => _padding;
            set => _padding = value > 10 ? 10 : value < 1 ? 1 : value;
        }

        /// <summary>
        /// Sets the border style of the table.
        /// <para>Modern will use the Unicode box-drawing characters, and Classic will use the ASCII characters.</para>
        /// <para>Modern is the default.</para>
        /// </summary>
        public BorderStyle BorderStyle
        {
            private get => _borderStyle;
            set => _borderStyle = value;
        }

        /// <summary>
        /// Sets the header text style of the table.
        /// <para>This will improve the readability of the header text. IE: SplitPascalCase will change LastName => Last Name</para>
        /// <para>The default is None.</para>
        /// </summary>
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
            // Set the default values. REM: Padding is set in the property and defaults to 1.
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

                // Optionally, the user can set the header text style to improve readability.
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
            // This is used to determine the final width of the column.
            foreach (var item in listDataDictionary)
            {
                int maxLength = list.Select(o => o.GetType().GetProperty(item.Key).GetValue(o)?.ToString()?.Length ?? 0).Max();
                if (maxLength > item.Value.DataLength) item.Value.DataLength = maxLength;
            }

            return TableStructure.BuildTable(listDataDictionary, Padding, BorderStyle, list);
        }
    }
}