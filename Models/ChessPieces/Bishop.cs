using System.Drawing;


namespace Models.ChessPieces
{
    public class Bishop : ChessPiece
    {
        public Bishop(string name, string text, Color textColor, Location currentLocation,
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
            bool isLegalMoveDiagonally = MoveAssistant.IsLegalMoveDiagonally(
                player, opponent, origin, destination, 7, false);

            return isLegalMoveDiagonally;
        }
    }
}
