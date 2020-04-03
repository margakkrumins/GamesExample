using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using Models;
using Models.ChessPieces;

namespace Chess
{   
    public class GameController
    {
        private const string PlayerOne = "Marga";
        private const string PlayerTwo = "Opponent";
        public List<Player> Players { get; private set; }
        public List<Location> Locations { get; private set; }

        public GameController()
        {
            InitializeLocations();
            InitializePlayers();
            InitializePlayersPieces();
        }

        #region Game Initialization Methods

        private void InitializeLocations()
        {
            Locations = new List<Location>();

            string[] cols = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" };
            foreach (var col in cols)
            {
                for (int i = 1; i < 9; i++)
                {
                    // Sets Coordinate property of each location
                    string cell = col + i.ToString();
                    Location location = new Location(cell);
                    Locations.Add(location);
                }
            }
        }

        private void InitializePlayers()
        {
            Players = new List<Player>();

            if (Players.Count >= 2)
            {
                throw new ApplicationException("Game can only have two players.");
            }
            else
            {
                Player player1 = new Player(PlayerOne, Color.Black);
                Player player2 = new Player(PlayerTwo, Color.BurlyWood);

                Players.Add(player1);
                Players.Add(player2);
            }
        }

        private void InitializePlayersPieces()
        {
            foreach (var player in Players)
            {
                if (player.PlayerColor == Color.Black)
                {
                    ChessPiece one = new Rook("R1", "R", player.PlayerColor, GetLocation("A1"), null, false) as ChessPiece;
                    //ChessPiece two = new ChessPiece("Kn1", "Kn", player.PlayerColor, GetLocation("B1"), null);
                    //ChessPiece three = new ChessPiece("B1", "B", player.PlayerColor, GetLocation("C1"), null);
                    //ChessPiece four = new ChessPiece("Q", "Q", player.PlayerColor, GetLocation("D1"), null);
                    //ChessPiece five = new ChessPiece("K", "K", player.PlayerColor, GetLocation("E1"), null);
                    //ChessPiece six = new ChessPiece("B2", "B", player.PlayerColor, GetLocation("F1"), null);
                    //ChessPiece seven = new ChessPiece("Kn2", "Kn", player.PlayerColor, GetLocation("G1"), null);
                    ChessPiece eight = new Rook("R2", "R", player.PlayerColor, GetLocation("H1"), null, false) as ChessPiece;
                    ChessPiece nine = new Pawn("P1", "P", player.PlayerColor, GetLocation("A2"), null, false) as ChessPiece;
                    ChessPiece ten = new Pawn("P1", "P", player.PlayerColor, GetLocation("B2"), null, false) as ChessPiece;
                    ChessPiece eleven = new Pawn("P1", "P", player.PlayerColor, GetLocation("C2"), null, false) as ChessPiece;
                    ChessPiece twelve = new Pawn("P2", "P", player.PlayerColor, GetLocation("D2"), null, false) as ChessPiece;
                    ChessPiece thirteen = new Pawn("P3", "P", player.PlayerColor, GetLocation("E2"), null, false) as ChessPiece;
                    ChessPiece fourteen = new Pawn("P4", "P", player.PlayerColor, GetLocation("F2"), null, false) as ChessPiece;
                    ChessPiece fifteen = new Pawn("P5", "P", player.PlayerColor, GetLocation("G2"), null, false) as ChessPiece;
                    ChessPiece sixteen = new Pawn("P6", "P", player.PlayerColor, GetLocation("H2"), null, false) as ChessPiece;

                    player.Pieces = new List<ChessPiece>();
                    player.Pieces.Add(one);
                    //player.Pieces.Add(two);
                    //player.Pieces.Add(three);
                    //player.Pieces.Add(four);
                    //player.Pieces.Add(five);
                    //player.Pieces.Add(six);
                    //player.Pieces.Add(seven);
                    player.Pieces.Add(eight);
                    player.Pieces.Add(nine);
                    player.Pieces.Add(ten);
                    player.Pieces.Add(eleven);
                    player.Pieces.Add(twelve);
                    player.Pieces.Add(thirteen);
                    player.Pieces.Add(fourteen);
                    player.Pieces.Add(fifteen);
                    player.Pieces.Add(sixteen);
                }
                else
                {
                    ChessPiece one = new Rook("R3", "R", player.PlayerColor, GetLocation("A8"), null, false) as ChessPiece;
                    //ChessPiece two = new ChessPiece("Kn3", "Kn", player.PlayerColor, GetLocation("B8"), null);
                    //ChessPiece three = new ChessPiece("B3", "B", player.PlayerColor, GetLocation("C8"), null);
                    //ChessPiece four = new ChessPiece("Q", "Q", player.PlayerColor, GetLocation("D8"), null);
                    //ChessPiece five = new ChessPiece("K", "K", player.PlayerColor, GetLocation("E8"), null);
                    //ChessPiece six = new ChessPiece("B4", "B", player.PlayerColor, GetLocation("F8"), null);
                    //ChessPiece seven = new ChessPiece("Kn4", "Kn", player.PlayerColor, GetLocation("G8"), null);
                    ChessPiece eight = new Rook("R4", "R", player.PlayerColor, GetLocation("H8"), null, false) as ChessPiece;
                    ChessPiece nine = new Pawn("P7", "P", player.PlayerColor, GetLocation("A7"), null, false) as ChessPiece;
                    ChessPiece ten = new Pawn("P8", "P", player.PlayerColor, GetLocation("B7"), null, false) as ChessPiece;
                    ChessPiece eleven = new Pawn("P9", "P", player.PlayerColor, GetLocation("C7"), null, false) as ChessPiece;
                    ChessPiece twelve = new Pawn("P10", "P", player.PlayerColor, GetLocation("D7"), null, false) as ChessPiece;
                    ChessPiece thirteen = new Pawn("P11", "P", player.PlayerColor, GetLocation("E7"), null, false) as ChessPiece;
                    ChessPiece fourteen = new Pawn("P12", "P", player.PlayerColor, GetLocation("F7"), null, false) as ChessPiece;
                    ChessPiece fifteen = new Pawn("P13", "P", player.PlayerColor, GetLocation("G7"), null, false) as ChessPiece;
                    ChessPiece sixteen = new Pawn("P14", "P", player.PlayerColor, GetLocation("H7"), null, false) as ChessPiece;

                    player.Pieces = new List<ChessPiece>();
                    player.Pieces.Add(one);
                    //player.Pieces.Add(two);
                    //player.Pieces.Add(three);
                    //player.Pieces.Add(four);
                    //player.Pieces.Add(five);
                    //player.Pieces.Add(six);
                    //player.Pieces.Add(seven);
                    player.Pieces.Add(eight);
                    player.Pieces.Add(nine);
                    player.Pieces.Add(ten);
                    player.Pieces.Add(eleven);
                    player.Pieces.Add(twelve);
                    player.Pieces.Add(thirteen);
                    player.Pieces.Add(fourteen);
                    player.Pieces.Add(fifteen);
                    player.Pieces.Add(sixteen);
                }
            }
        }
        #endregion

        public Location GetLocation(string coordinate)
        {
            var location = Locations.SingleOrDefault(p => p.Coordinate == coordinate);
            return location;
        }

        /// <summary>
        /// Tries to execute the move.
        /// </summary>
        /// <param name="playerIndex">Index of player making the move.</param>
        /// <param name="origin">The origin Location</param>
        /// <param name="destination">The destination Location</param>
        /// <param name="message">Either an empty string or a message containing UI feedback.</param>
        /// <returns>True, if successful; else false</returns>
        internal bool DidExecuteMove(
            int playerIndex, Location origin, Location destination, out string message)
        {
            int otherPlayerIndex = GetOtherPlayerIndex(playerIndex);

            Player player = Players[playerIndex];
            Player opponent = Players[otherPlayerIndex];
            ChessPiece piece = Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == origin.Coordinate);

            if (DidMovePiece(player, opponent, origin, destination, piece))
            {
                // Update state of captured piece
                bool isCaptured = MoveAssistant.IsCapture(opponent, destination);
                if (isCaptured)
                {
                    ChessPiece capturedPiece =
                        opponent.Pieces.SingleOrDefault(p => p.CurrentLocation == destination);
                    capturedPiece.PreviousLocation = capturedPiece.CurrentLocation;
                    capturedPiece.CurrentLocation = null;
                    capturedPiece.IsCaptured = true;
                }

                piece.PreviousLocation = piece.CurrentLocation;
                piece.CurrentLocation = destination;

                message = $"Moved {piece.Text} from {piece.PreviousLocation.Coordinate} to {piece.CurrentLocation.Coordinate}";
                return true;
            }
            else
            {
                message = "Your move is not legal. Please try a different move.";
            }

            return false;
        }

        public bool DidMovePiece(
            Player player, Player opponent, Location origin, Location destination, ChessPiece piece)
        {
            // Check if a player's own piece is at destination.
            bool isOccupiedByOwn = player.Pieces.Exists(o => o.CurrentLocation == destination);
            if (isOccupiedByOwn)
                return false;

            bool didMove = false;

            // Figure out which type of piece and hand off to its DidMove method.
            // ToDo: Finish switch for rest of pieces and their DidMove methods!!!
            switch (piece)
            {
                case Rook rook:
                    didMove = rook.DidMove(player, opponent, origin, destination);
                    break;
                case Pawn pawn:
                    didMove = pawn.DidMove(player, opponent, origin, destination);
                    break;
                case null:                    
                    break;
                default:
                    break;
            }

            //If we got here, return the final check's value.
            return didMove;
        }

        /// <summary>
        /// Determines if move is legal for a Pawn
        /// </summary>
        /// <param name="piece">Chesspiece</param>
        /// <param name="opponent">Other Player</param>
        /// <param name="destination">Destination Location</param>
        /// <returns>True, if it is; else, false
        //public bool IsLegalForPawn(ChessPiece piece, Player otherPlayer, Location destination)
        //{
        //    int originRow = GetRowNumber(piece.CurrentLocation.Coordinate);
        //    int destRow = GetRowNumber(destination.Coordinate);

        //    if (IsCapture(otherPlayer, destination) 
        //        && IsAdjacentColumn(piece.CurrentLocation.Coordinate, destination.Coordinate))
        //    {
        //        // Can move 1 row further in the adjacent column                
        //        if (IsNumberRowsLegal(otherPlayer, originRow, destRow, 1))
        //            return true;

        //    }
        //    else
        //    {
        //        if (!IsSameColumn(piece.CurrentLocation.Coordinate, destination.Coordinate))
        //            return false;

        //        // Can move up to 2 rows further in same column
        //        bool isLegalSameColumnMove = 
        //            IsNumberRowsLegal(otherPlayer, originRow, destRow, 2);
        //    }

        //    // ToDo: Handle upgrade of Pawn to Queen if reach last row.

        //    // If we got here, didn't satisfy any tests.
        //    return false;
        //}

        public int GetOtherPlayerIndex(int playerIndex)
        {
            int otherPlayerIndex = 0;

            if (playerIndex == 0)
                otherPlayerIndex = 1;

            return otherPlayerIndex;
        }

        /// <summary>
        /// Determines if destination is in adjacent column
        /// </summary>
        /// <param name="currCoord">Current Coordinate</param>
        /// <param name="destCoord">Destination Coordinate</param>
        /// <returns>True, if it is; else, false.</returns>
        //public bool IsAdjacentColumn(string currCoord, string destCoord)
        //{
        //    int currColNo = GetColumnNumber(currCoord);
        //    int destColNo = GetColumnNumber(destCoord);

        //    if ((currColNo + 1 == destColNo) || (currColNo - 1 == destColNo))
        //        return true;
        //    else
        //        return false;
        //}

    }
}
