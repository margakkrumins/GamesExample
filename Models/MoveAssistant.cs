﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Models
{
    public static class MoveAssistant
    {
        public static bool IsLegalMoveAcrossColumns(
            Player player, Player opponent, Location origin, Location destination, int limit = 7)
        {
            // Check if is in same row
            if (GetRowNumber(origin.Coordinate) != GetRowNumber(destination.Coordinate))
                return false;

            // Check if can move through the number of columns
            int originCol = GetColumnNumber(origin.Coordinate);
            int destCol = GetColumnNumber(destination.Coordinate);
            bool isIncremented = IsColumnIncremented(originCol, destCol);
            int maxSquares = CalculateMaxSquares(originCol, destCol, isIncremented);

            // Check if move exceeds limits
            if (limit < 7 && maxSquares > limit)
                return false; 
            
            int row = GetRowNumber(origin.Coordinate);            
            
            // Check if there are any pieces obstructing the path prior to destination.
            // Note: the destination was already checked for own piece at the outset.
            if (isIncremented)
            {
                //Check all the columns traversed, except for origin and destination
                for (int i = originCol + 1; i < (originCol + maxSquares); i++)
                {
                    string currentCoord = $"{GetColumnLetter(i)}{row}";

                    if (IsOccupied(player, opponent, currentCoord))
                        return false;
                }
            }
            else
            {
                // Example: originCol = 5; destCol = 2; maxSquares = 3
                //Check all the columns traversed, except for origin and destination
                for (int i = originCol - 1; i > destCol + 1; i--)
                {
                    string currentCoord = $"{GetColumnLetter(i)}{row}";

                    if (IsOccupied(player, opponent, currentCoord))
                        return false;
                }
            }

            // If got here, piece can move to destination
            return true;
        }

        public static bool IsLegalMoveThroughRows(Player player, Player opponent, 
            Location origin, Location destination, int limit = 7, bool canRetreat = true)
        {
            // Check if is in same column
            if (GetColumnNumber(origin.Coordinate) != GetColumnNumber(destination.Coordinate))
                return false;

            // Check if can move through the number of rows
            int originRow = GetRowNumber(origin.Coordinate);
            int destRow = GetRowNumber(destination.Coordinate);
            bool isRowIncremented = IsRowIncremented(originRow, destRow);

            // Handle pieces that cannot retreat
            if (canRetreat == false && isRowIncremented == false && player.PlayerColor == Color.Black)
                return false;

            if (canRetreat == false && isRowIncremented == true && player.PlayerColor == Color.BurlyWood)
                return false;

            int maxSquares = CalculateMaxSquares(originRow, destRow, isRowIncremented);

            // Check if move exceeds limit
            if (limit < 7 && maxSquares > limit)
                return false;

            string letter = GetColumnLetter(origin.Coordinate);

            // Check if there are any pieces obstructing the path prior to destination.
            // Note: the destination was already checked for own piece at the outset.
            if (isRowIncremented)
            {
                //Check all the rows traversed, except for origin and destination
                for (int i = originRow + 1; i < (originRow + maxSquares); i++)
                {
                    string currentCoord = $"{letter}{i}";

                    if (IsOccupied(player, opponent, currentCoord))
                        return false;
                }
            }
            else
            {
                // Example: originRow = 5; destRow = 2; maxSquares = 3
                // Check all the rows traversed, except for origin and destination
                for (int i = originRow - 1; i > destRow + 1; i--)
                {
                    string currentCoord = $"{letter}{i}";

                    if (IsOccupied(player, opponent, currentCoord))
                        return false;
                }
            }

            // If got here, piece can move to destination
            return true;
        }

        public static bool IsLegalMoveDiagonally(
            Player player, Player opponent, Location origin, Location destination, int limit = 7, bool canRetreat = true)
        {
            // Check if is diagonal move

            // Possibilities:
            //  Both col & row increment by 1
            //  Both col & row decrement by 1
            //   Col increment by 1 & row decrement by 1
            //  Col decrement by 1 & row increment by 1

            // Get row and column numbers
            int originRow = GetRowNumber(origin.Coordinate);
            int destRow = GetRowNumber(destination.Coordinate);

            bool isRowIncremented = IsRowIncremented(originRow, destRow);

            if (canRetreat == false && isRowIncremented == false && player.PlayerColor == Color.Black)
                return false;

            if (canRetreat == false && isRowIncremented == true && player.PlayerColor == Color.BurlyWood)
                return false;

            int maxSquaresForRows = CalculateMaxSquares(originRow, destRow, isRowIncremented);

            // Check if move exceeds row limits
            if (limit < 7 && maxSquaresForRows > limit)
                return false;

            int originCol = GetColumnNumber(origin.Coordinate);
            int destCol = GetColumnNumber(destination.Coordinate);
            bool isColIncremented = IsColumnIncremented(originCol, destCol);
            int maxSquaresForColumns = CalculateMaxSquares(originCol, destCol, isColIncremented);

            // Check if move exceeds column limits
            if (limit < 7 && maxSquaresForColumns > limit)
                return false;

            // Build two <int, int> seqNo & column or row number dictionaries, using the
            // increment/decrement logic of each dimension, then for each seqNo of ColumnDictionary
            // form coordinate by concatenating column value and row value for same seqNo.
            Dictionary<int, int> columns = new Dictionary<int, int>(limit);
            Dictionary<int, int> rows = new Dictionary<int, int>(limit);
            
            columns = GetColunnDictionary(originCol, destCol, isColIncremented, maxSquaresForColumns, limit);
            rows = GetRowDictionary(originRow, destRow, isRowIncremented, maxSquaresForRows, limit);

            // Both the dictionaries should have the same sequence numbers, 
            // since are on the same diagonal trajectory
            foreach (var kvp in columns)
            {
                string letter = GetColumnLetter(kvp.Value);
                int rowNumber = rows[kvp.Key];

                string currentCoord = $"{letter}{rowNumber}";

                if (IsOccupied(player, opponent, currentCoord))
                    return false;
            }

            // If got here, piece can move to destination
            return true;
        }

        public static bool IsLegalMoveForKnight(
            Player player, Player opponent, Location origin, Location destination, int limit = 7, bool canRetreat = true)
        {            
            // Get row and column numbers
            int originRow = GetRowNumber(origin.Coordinate);
            int destRow = GetRowNumber(destination.Coordinate);
            bool isRowIncremented = IsRowIncremented(originRow, destRow);
            int maxSquaresForRows = CalculateMaxSquares(originRow, destRow, isRowIncremented);

            int originCol = GetColumnNumber(origin.Coordinate);
            int destCol = GetColumnNumber(destination.Coordinate);
            bool isColIncremented = IsColumnIncremented(originCol, destCol);
            int maxSquaresForColumns = CalculateMaxSquares(originCol, destCol, isColIncremented);

             // Check if is L-Shaped (2 & 1 or 1 & 2)
            if (maxSquaresForRows > maxSquaresForColumns)
            {
                // Is attempt at 2 through rows and 1 through columns
                if (maxSquaresForRows != limit || maxSquaresForColumns != 1)
                    return false;                
            }
            else
            {
                // Is attempt at 1 through rows and 2 through columns
                if (maxSquaresForRows != 1 || maxSquaresForColumns != limit)
                    return false;                
            }

            // If got here, piece can move to destination
            return true;
        }

        public static bool IsCapture(Player opponent, Location destination)
        {
            return opponent.Pieces
                           .Exists(x => x.CurrentLocation.Coordinate == destination.Coordinate);
        }

        public static bool IsCapture(Player opponent, string coordinate)
        {
            return opponent.Pieces
                           .Exists(x => x.CurrentLocation.Coordinate == coordinate);
        }

        #region Helper Methods

        private static Dictionary<int, int> GetColunnDictionary(int originCol, 
            int destCol, bool isColIncremented, int maxSquaresForColumns, int limit)
        {
            // Load ColumnDictionary with column numbers moved to, in sequence
            Dictionary<int, int> columns = new Dictionary<int, int>(limit);
            int seqNo = 0;

            if (isColIncremented)
            {
                for (int colNo = originCol + 1; colNo < (originCol + maxSquaresForColumns + 1); colNo++)
                {
                    seqNo++;
                    columns.Add(seqNo, colNo);
                }
            }
            else
            {
                for (int colNo = originCol - 1; colNo > destCol - 1; colNo--)
                {
                    seqNo++;
                    columns.Add(seqNo, colNo);
                }
            }

            return columns;
        }

        private static Dictionary<int, int> GetRowDictionary(int originRow, int destRow, 
            bool isRowIncremented, int maxSquaresForRows, int limit)
        {
            //Load RowDictionary with row numbers moved to, in sequence.
            Dictionary<int, int> rows = new Dictionary<int, int>(limit);
            int seqNo = 0;

            if (isRowIncremented)
            {
                for (int rowNo = originRow + 1; rowNo < (originRow + maxSquaresForRows + 1); rowNo++)
                {
                    seqNo++;
                    rows.Add(seqNo, rowNo);
                }
            }
            else
            {
                for (int rowNo = originRow - 1; rowNo > destRow - 1; rowNo--)
                {
                    seqNo++;
                    rows.Add(seqNo, rowNo);
                }
            }

            return rows;
        }

        private static bool IsOccupied(Player player, Player opponent, string coordinate)
        {
            var isPlayerOccupied = player.Pieces
                .Exists(p => p.CurrentLocation.Coordinate == coordinate);
            var isOpponentOccupied = opponent.Pieces
                .Exists(p => p.CurrentLocation.Coordinate == coordinate);

            if (isPlayerOccupied || (isOpponentOccupied && !IsCapture(opponent, coordinate)))
                return true;
            else
                return false;
        }

        private static int GetColumnNumber(string coordinate)
        {
            char c = char.Parse(coordinate.Substring(0, 1));
            int colNumber = char.ToUpper(c) - 64;  // Ascii value of 'A' is 65
            return colNumber;
        }

        private static string GetColumnLetter(string coordinate)
        {
            string letter = coordinate.Substring(0, 1);
            return letter;
        }

        private static string GetColumnLetter(int columnNumber)
        {
            char c = Convert.ToChar(columnNumber + 64); // Ascii value of 'A' is 65
            return c.ToString();
        }

        private static int GetRowNumber(string coordinate)
        {
            return int.Parse(coordinate.Substring(1, 1));
        }

        private static bool IsRowIncremented (int originRow, int destRow)
        {
            if (originRow > destRow)
                // Moving up into smaller rows
                return false;
            else
                // Moving down into larger rows
                return true;
        }

        private static bool IsColumnIncremented(int originCol, int destCol)
        {
            if (originCol > destCol)
                // Moving left into smaller columns
                return false;
            else
                // Moving right into larger columns
                return true;
        }

        private static int CalculateMaxSquares(int origin, int dest, bool isIncremented)
        {
            if (isIncremented)
                return dest - origin;
            else
                return Math.Abs(origin - dest);
        }

        #endregion

    }
}
