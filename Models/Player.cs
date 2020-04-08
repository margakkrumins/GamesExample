using Models.ChessPieces;
using System.Collections.Generic;
using System.Drawing;

namespace Models
{
    public class Player
    {
        public string Name { get; set; }
        public Color PlayerColor { get; set; }
        public List<ChessPiece> Pieces { get; set; }
        public List <ChessPiece> CapturedPieces { get; set; }

        public Player()
        {
        }

        public Player(string name, Color playerColor)
        {
            Name = name;
            PlayerColor = playerColor;             
        }  
        
        public bool DidMovePiece(ChessPiece piece, Location destination)
        {
            return false;
        }

    }
}
