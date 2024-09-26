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
    public enum HeaderTextStyle { None, SplitCamelCase, SplitUnderline }

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
        /// </summary>
        /// <param name="list">The generic list containing the data to display.</param>
        /// <returns>String.</returns>
        public string DisplayTable(List<object> list)
        {
            if (list.Count == 0) return "No data found!";

            // Determine how many properties the list has.
            int columnCount = list.First().GetType().GetProperties().Count();

            // Get a dictionary of property names and their maximum string lengths.
            Dictionary<string, int> columnsAndLengths = list
                    .SelectMany(obj => obj.GetType().GetProperties(), (obj, prop) => new { prop.Name, Value = prop.GetValue(obj)?.ToString() ?? string.Empty })
                    .GroupBy(x => x.Name)
                    .ToDictionary(g => g.Key, g => g.Max(x => x.Value.Length));

            // Print each dictionary key and value.
            // foreach (var item in columnsAndLengths)
            // {
            //     Console.WriteLine($"{item.Key} - {item.Value}");
            // }

            // Determine the total length of the horizontal line.
            int totalLength = columnsAndLengths.Sum(x => x.Value) + (Padding * 2 * columnCount) + (columnCount - 1) + 2;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(TableStructure.BuildHeader(columnsAndLengths, Padding, BorderStyle, HeaderTextStyle));
            sb.AppendLine(TableStructure.BuildBody(columnsAndLengths, Padding, BorderStyle, HeaderTextStyle, list));


            // Console.WriteLine($"totalLength: {totalLength}");

            // string horizontalLine = new string(headerChar, totalLength);
            // // Replace the first char with ┌
            // horizontalLine = horizontalLine.Remove(0, 1).Insert(0, "╭");
            // horizontalLine = horizontalLine.Remove(horizontalLine.Length - 1, 1).Insert(horizontalLine.Length - 1, "╮");

            // StringBuilder sb = new StringBuilder();
            // sb.AppendLine(horizontalLine);

            // string test = new string(' ', totalLength);
            // test = test.Remove(0, 1).Insert(0, "│");
            // test = test.Remove(test.Length - 1, 1).Insert(test.Length - 1, "│");

            // sb.AppendLine(test);


            // foreach (var obj in list)
            // {
            //     Type type = obj.GetType();
            //     PropertyInfo[] properties = type.GetProperties();

            //     foreach (var property in properties)
            //     {
            //         string name = property.Name;
            //         object value = property.GetValue(obj);
            //         tableBuilder.AppendLine($"{name}: {value}");
            //     }
            //     tableBuilder.AppendLine(); // Add a blank line between objects
            // }

            // sb.AppendLine(horizontalLine);

            return sb.ToString();
        }
    }
}
