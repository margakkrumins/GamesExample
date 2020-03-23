using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Models
{
    public static class MoveAssistant
    {
        public static bool IsLegalMoveAcrossColumns(
            Player player, Player opponent, Location origin, Location destination)
        {
            // Check if is in same row
            if (GetRowNumber(origin.Coordinate) != GetRowNumber(destination.Coordinate))
                return false;

            // Check if can move through the number of columns
            int originCol = GetColumnNumber(origin.Coordinate);
            int destCol = GetColumnNumber(destination.Coordinate);
            bool isIncremented = IsIncremented(originCol, destCol);
            int maxSquares = CalculateMaxSquares(originCol, destCol, isIncremented);
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

        public static bool IsLegalMoveThroughRows(
            Player player, Player opponent, Location origin, Location destination)
        {
            // Check if is in same column
            if (GetColumnNumber(origin.Coordinate) != GetColumnNumber(destination.Coordinate))
                return false;

            // Check if can move through the number of rows
            int originRow = GetRowNumber(origin.Coordinate);
            int destRow = GetRowNumber(destination.Coordinate);
            bool isRowIncremented = IsIncremented(opponent);
            int maxSquares = CalculateMaxSquares(originRow, destRow, isRowIncremented);
            string letter = GetColumnLetter(origin.Coordinate);

            // Check if there are any pieces obstructing the path prior to destination.
            // Note: the destination was already checked for own piece at the outset.
            if (isRowIncremented)
            {
                //Check all the rows traversed, except for origin and destination
                for (int i = originRow; i < (originRow + maxSquares); i++)
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


        #region Helper Methods

        private static bool IsOccupied(Player player, Player opponent, string coordinate)
        {
            var isPlayerOccupied = player.Pieces
                .Exists(p => p.CurrentLocation.Coordinate == coordinate);
            var isOpponentOccupied = opponent.Pieces
                .Exists(p => p.CurrentLocation.Coordinate == coordinate);

            if (isPlayerOccupied || isOpponentOccupied)
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

        private static bool IsIncremented (Player otherPlayer)
        {
            if (otherPlayer.PlayerColor == Color.Black)
                // Moving up into smaller rows
                return false;
            else
                // Moving down into larger rows
                return true;
        }

        private static bool IsIncremented(int originCol, int destCol)
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
