using System.Drawing;

namespace Models.ChessPieces
{
    public abstract class ChessPiece
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Color TextColor { get; set; }
        public Location CurrentLocation { get; set; }
        public Location PreviousLocation { get; set; }
        public bool CanJump { get; set; }
        public bool IsCaptured { get; set; }

        public abstract bool DidMove(
            Player player, Player opponent, Location origin, Location destination); 
    }
}
