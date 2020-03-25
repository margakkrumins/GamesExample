using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Models.ChessPieces
{
    public class Rook : ChessPiece
    {
        public Rook(
            string name, string text, Color textColor, Location currentLocation, 
            Location previousLocation, bool canJump)
        {
            Name = name;
            Text = text;
            TextColor = textColor;
            CurrentLocation = currentLocation;
            PreviousLocation = previousLocation;
            CanJump = canJump;
            IsCaptured = false;
        }

        public override bool DidMove(
            Player player, Player opponent, Location origin, Location destination)
        {
            bool isLegalMoveThroughRows = 
                MoveAssistant.IsLegalMoveThroughRows(player, opponent, origin, destination);

            bool isLegalMoveAcrossColumns = 
                MoveAssistant.IsLegalMoveAcrossColumns(player, opponent, origin, destination);

            // ToDo: Handle special case of Castling

            // If legal through rows or across columns, return True
            if (isLegalMoveThroughRows || isLegalMoveAcrossColumns)
                return true;
            else
                return false;

        }
    }
}
