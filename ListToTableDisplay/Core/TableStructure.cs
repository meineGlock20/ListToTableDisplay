using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ListToTableDisplay.Core
{
    public class TableStructure
    {
        // for modern table outline, see https://en.wikipedia.org/wiki/Box-drawing_character
        // │└ ┘┌ ┐┬ ┴ ┼ ╭ ─ ╮ ╯ ╰

        private static readonly char vl = '│'; // vertical line
        private static readonly char hl = '─'; // horizontal line
        private static readonly char headerbl = '└'; // header bottom left
        private static readonly char headerbr = '┘'; // header bottom right
        private static readonly char tlc = '╭'; // top right corner
        private static readonly char trc = '╮'; // top left corner
        private static readonly char blc = '╰'; // bottom left corner
        private static readonly char brc = '╯'; // bottom right corner
        private static readonly char tcs = '┬'; // top center separator
        private static readonly char bcs = '┴'; // bottom center separator
        private static readonly char mcs = '┼'; // middle center separator

        internal static string BuildHeader(Dictionary<string, int> columnsAndLengths,
                                    int padding,
                                    BorderStyle borderStyle,
                                    HeaderTextStyle headerTextStyle)
        {
            StringBuilder sb = new StringBuilder();

            // The number of columns in the table.
            int columnCount = columnsAndLengths.Count;

            // Determine the total length of the horizontal line.
            int totalLength = columnsAndLengths.Sum(x => x.Value) + (padding * 2 * columnCount) + (columnCount - 1) + 3;

            // Top border.
            sb.Append(borderStyle == BorderStyle.Modern ? tlc : trc);
            sb.Append(new string(hl, totalLength - 2));
            sb.Append(borderStyle == BorderStyle.Modern ? trc : tlc);
            sb.AppendLine();

            // Column names.
            for (int i = 0; i < columnCount; i++)
            {
                sb.Append(vl).Append(' ', padding);
                string columnName = columnsAndLengths.ElementAt(i).Key;
                int rightpadding = padding + columnsAndLengths[columnName] - columnName.Length;
                sb.Append(columnName).Append(' ', rightpadding <= 0 ? 1 : rightpadding);
            }
            sb.Append(vl);
            sb.AppendLine();

            // Header separator.
            sb.Append(headerbl);
            sb.Append(hl, totalLength - 2);
            sb.Append(headerbr);

            return sb.ToString();
        }

        internal static string BuildBody(Dictionary<string, int> columnsAndLengths,
                                    int padding,
                                    BorderStyle borderStyle,
                                    HeaderTextStyle headerTextStyle)
        {
            StringBuilder sb = new StringBuilder();




            return sb.ToString();
        }
    }
}