using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace Models.ChessPieces
{
    public class Knight : ChessPiece
    {
        public Knight(string name, string text, Color textColor, Location currentLocation,
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
            var piece = player.Pieces.Find(p => p.CurrentLocation == origin);

            // Can move through L-Shaped Knight's Move
            bool isLegalMoveForKnight = 
                MoveAssistant.IsLegalMoveForKnight(player, opponent, origin, destination, 2);

            if (isLegalMoveForKnight)
                return true;
            else
                return false;
        }
    }
}
