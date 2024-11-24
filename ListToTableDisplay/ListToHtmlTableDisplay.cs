using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ListToTableDisplay.Core;

namespace ListToTableDisplay
{
    public class ListToHtmlTableDisplay
    {
        public static string DisplayTable(
            List<object> list,
            HeaderTextStyle headerTextStyle,
            bool minify = false,
            string tableId = null,
            string tableClass = null)
        {
            if (list.Count == 0) return "🚩 No data found!";

            List<string> headers = new List<string>();

            // Use reflection to get a list of properties and extract the property name.
            var obj = list.First();
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (var property in properties)
            {
                string name = property.Name;
                headers.Add(name);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<table" + (!string.IsNullOrWhiteSpace(tableId) ? $" id=\"{tableId}\"" : "") + (!string.IsNullOrWhiteSpace(tableClass) ? $" class=\"{tableClass}\"" : "") + ">");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            foreach (var header in headers)
            {
                string name = header;
                switch (headerTextStyle)
                {
                    case HeaderTextStyle.SplitPascalCase:
                        name = System.Text.RegularExpressions.Regex.Replace(name, "(\\B[A-Z])", " $1");
                        break;
                    case HeaderTextStyle.SplitUnderline:
                        name = name.Replace("_", " ");
                        break;
                    default:
                        break;
                }
                sb.AppendLine($"<th>{name}</th>");
            }
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");
            foreach (var item in list)
            {
                sb.AppendLine("<tr>");
                foreach (var header in headers)
                {
                    var value = item.GetType().GetProperty(header).GetValue(item);
                    sb.AppendLine($"<td>{value}</td>");
                }
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");

            return minify ? sb.ToString().Replace("\n", "").Replace("\r", "") : sb.ToString();
        }
    }
}
