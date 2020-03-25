using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace Models.ChessPieces
{
    public class Pawn : ChessPiece
    {
        public Pawn(string name, string text, Color textColor, Location currentLocation,
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
            // Can either move up to 2 rows forward

            bool isLegalMoveThroughRows =
                MoveAssistant.IsLegalMoveThroughRows(player, opponent, origin, destination, 2);

            // Or, if is capture, 1 col & row diagonally
            bool isCapture = MoveAssistant.IsCapture(opponent, destination);
            bool isLegalMoveDiagonally = false;

            if (isCapture)
            {
                isLegalMoveDiagonally =
                    MoveAssistant.IsLegalMoveDiagonally(player, opponent, origin, destination, 1);
            }

            // If legal through rows or diagonally, return True
            if (isLegalMoveThroughRows || isLegalMoveDiagonally)
                return true;
            else
                return false;
        }
    }
}
