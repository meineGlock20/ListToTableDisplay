using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ListToTableDisplay
{
    public class ListToTableDisplay
    {
        public string DisplayTable(List<object> list)
        {
            if (list.Count == 0) return "No data found!";

            // Determine how many properties the list has.
            // TODO: This works - needs to be refactored.
            int columnCount = list.First().GetType().GetProperties().Count();
            Console.WriteLine($"columnCount: {columnCount}");

            // For each property, determine the longest string length.
            // Dictionary<string, int> columnWidths = new Dictionary<string, int>();
            // foreach (var obj in list)
            // {
            //     Type type = obj.GetType();
            //     PropertyInfo[] properties = type.GetProperties();

            //     foreach (var property in properties)
            //     {
            //         string name = property.Name;
            //         object value = property.GetValue(obj);
            //         int length = value.ToString().Length;

            //         if (columnWidths.ContainsKey(name))
            //         {
            //             if (length > columnWidths[name])
            //             {
            //                 columnWidths[name] = length;
            //             }
            //         }
            //         else
            //         {
            //             columnWidths.Add(name, length);
            //         }
            //     }
            // }

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
