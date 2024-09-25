using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ListToTableDisplay
{
    public class ListToTableDisplay
    {
        private int _padding = 1;
        private readonly char borderChar = '|';
        private readonly char headerChar = '-';

        /// <summary>
        /// The padding on each side of the string value in the table cell. 
        /// <para>The default is 1, the minimum is 1, and the maximum is 10.</para>
        /// </summary>
        public int Padding
        {
            private get => _padding;
            set => _padding = value > 10 ? 10 : value < 1 ? 1 : value;
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
            Console.WriteLine($"columnCount: {columnCount}");

            // Get a dictionary of property names and their maximum string lengths.
            Dictionary<string, int> columnsAndLengths = list
                    .SelectMany(obj => obj.GetType().GetProperties(), (obj, prop) => new { prop.Name, Value = prop.GetValue(obj)?.ToString() ?? string.Empty })
                    .GroupBy(x => x.Name)
                    .ToDictionary(g => g.Key, g => g.Max(x => x.Value.Length));

            // Print each dictionary key and value.
            foreach (var item in columnsAndLengths)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }

            // Determine the lenght of the horizontal line.
            int totalLength = columnsAndLengths.Sum(x => x.Value) + (Padding * 2 * columnCount) + (columnCount - 1) + 2;
            Console.WriteLine($"totalLength: {totalLength}");


            StringBuilder tableBuilder = new StringBuilder();

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

            return tableBuilder.ToString();
        }
    }
}
