namespace ListToTableDisplay.Models
{
    /// <summary>
    /// The model for the list data. 
    /// This is used to store the original name of the property, the display name of the property, 
    /// and the length of the data in the property.
    /// </summary>
    internal class ListData
    {
        /// <summary>
        /// The original name of the property.
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// The display name of the property. This is the name that will be displayed in the table.
        /// <para>It may be the same as the original name.</para>
        /// <para>This is set by the HeaderTextStyle property in the ListToTableDisplay class.</para>
        /// </summary>
        public string DisplayName { get; set; }
        
        /// <summary>
        /// The maximum length of the data in the property.
        /// <para>This is used to determine the final width of the column.</para>
        /// </summary>
        public int DataLength { get; set; }
    }
}