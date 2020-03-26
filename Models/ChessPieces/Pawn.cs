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

        // ToDo: Handle promotion of pawns
        public override bool DidMove(
            Player player, Player opponent, Location origin, Location destination)
        {
            // Can move up to 2 rows toward opponent on 1st move
            bool isInitialMove = false;
            var piece = player.Pieces.Find(p => p.CurrentLocation == origin);

            if (piece.PreviousLocation == null)              
                isInitialMove = true;

            int limit = 1;

            if (isInitialMove)
                limit = 2;
            
            bool isLegalMoveThroughRows =
                MoveAssistant.IsLegalMoveThroughRows(player, opponent, origin, destination, limit, false);

            // If is capture, can move 1 square diagonally toward opponent
            // ToDo: Handle case of en passant capture!!!
            bool isCapture = MoveAssistant.IsCapture(opponent, destination);
            bool isLegalMoveDiagonally = false;

            if (isCapture)
            {
                isLegalMoveDiagonally =
                    MoveAssistant.IsLegalMoveDiagonally(player, opponent, origin, destination, 1, false);
            }

            // If legal through rows or diagonally, return True
            if (isLegalMoveThroughRows || isLegalMoveDiagonally)
                return true;
            else
                return false;
        }
    }
}
