namespace Models
{
    public class Location
    {
        public string Coordinate { get; set; }

        /// <summary>
        /// Initialize the location, setting its Coordinate property, e.g. A1.
        /// </summary>
        /// <param name="coordinate">string containing:{ColumnLetter}{RowNumber}</param>
        public Location(string coordinate)
        {
            Coordinate = coordinate;
        }

    }
}
