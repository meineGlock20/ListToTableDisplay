using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ListToTableDisplay.Core
{
    /// <summary>
    /// Contains the methods to build the table structure.
    /// </summary>
    /// <remarks>
    /// For modern table outline, see https://en.wikipedia.org/wiki/Box-drawing_character.
    /// </remarks>
    public class TableStructure
    {
        private static readonly char vl = '│'; // vertical line
        private static readonly char cvl = '|'; // classic vertical line
        private static readonly char hl = '─'; // horizontal line
        private static readonly char chl = '-'; // classic horizontal line
        private static readonly char headerbl = '├'; // header bottom left
        private static readonly char headerbr = '┤'; // header bottom right
        private static readonly char tlc = '╭'; // top right corner
        private static readonly char trc = '╮'; // top left corner
        private static readonly char blc = '╰'; // bottom left corner
        private static readonly char brc = '╯'; // bottom right corner
        private static readonly char tcs = '┬'; // top center separator
        private static readonly char bcs = '┴'; // bottom center separator
        private static readonly char mcs = '┼'; // middle center separator

        internal static string BuildTable(Dictionary<string, Models.ListData> listDataDictionary,
            int padding, BorderStyle borderStyle, List<object> list)
        {
            StringBuilder sb = new StringBuilder();

            // The number of columns in the table.
            int columnCount = listDataDictionary.Count;

            // Determine the total length of the horizontal line using the DataLength property of the model in the dictionary.
            int totalLength = listDataDictionary.Sum(x => x.Value.DataLength) + (padding * 2 * columnCount) + (columnCount - 1) + 2;

            // Top border.
            sb.Append(borderStyle == BorderStyle.Modern ? tlc : trc);
            sb.Append(new string(hl, totalLength - 2));
            sb.Append(borderStyle == BorderStyle.Modern ? trc : tlc);
            sb.AppendLine();

            foreach (var item in listDataDictionary)
            {
                sb.Append(vl);
                sb.Append(' ', padding);
                sb.Append(item.Value.DisplayName);
                int rightpadding = padding + item.Value.DataLength - item.Value.DisplayName.Length;
                sb.Append(' ', rightpadding <= 0 ? 1 : rightpadding);
            }
            sb.Append(vl);

            // Header separator.
            sb.AppendLine();
            sb.Append(headerbl);
            sb.Append(hl, totalLength - 2);
            sb.Append(headerbr);

            // Rows.
            sb.AppendLine();
            foreach (var obj in list)
            {
                Type type = obj.GetType();
                PropertyInfo[] properties = type.GetProperties();

                sb.Append(vl);
                foreach (var property in properties)
                {
                    string name = property.Name;
                    object value = property.GetValue(obj);

                    sb.Append(' ', padding);
                    sb.Append(value);
                    sb.Append(' ', padding + listDataDictionary[name].DataLength - value.ToString().Length);
                    sb.Append(vl);
                }
                sb.AppendLine();
            }

            // Bottom border.
            sb.Append(borderStyle == BorderStyle.Modern ? blc : brc);
            sb.Append(new string(hl, totalLength - 2));
            sb.Append(borderStyle == BorderStyle.Modern ? brc : blc);

            return sb.ToString();
        }
    }
}