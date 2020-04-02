using NUnit.Framework;
using System.Linq;
using Models;
using Chess;
using Models.ChessPieces;

namespace ModelsTests
{   
    public class Tests
    {
        // ToDo: Add more tests, especially for edge cases.
        // ToDo: Arrange tests without dependencies, but for now, limiting testing
        // to tests within context of how board is set up.

        #region Test Moves Across Columns

        [Test]
        public void IsLegalMoveAcrossColumns_IsTrue_Test_001()
        {
            // Arrange across columns
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("H5");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A8");
            // Fake a previous move that places piece in desired location.
            piece.CurrentLocation = origin;

            // Act
            bool isLegalMoveAcrossColumns =
                MoveAssistant.IsLegalMoveAcrossColumns(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination);

            // Assert
            Assert.IsTrue(isLegalMoveAcrossColumns);
        }

        [Test]
        public void IsLegalMoveAcrossColumns_IsTrue_Test_002()
        {
            // Arrange across columns with capture
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("H5");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A8");
            ChessPiece opponentPiece = gc.Players[otherPlayerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A1");
            // Fake previous moves that places pieces in desired locations.
            piece.CurrentLocation = origin;
            opponentPiece.CurrentLocation = destination;

            // Act
            bool isLegalMoveAcrossColumns =
                MoveAssistant.IsLegalMoveAcrossColumns(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination);

            // Assert
            Assert.IsTrue(isLegalMoveAcrossColumns);
        }

        [Test]
        public void IsLegalMoveAcrossColumns_IsFalse_Test_001()
        {
            // Arrange IsFalse because not across columns
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("A3");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A8");
            // Fake a previous move that places piece in desired location.
            piece.CurrentLocation = origin;

            // Act
            bool isLegalMoveAcrossColumns =
                MoveAssistant.IsLegalMoveAcrossColumns(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination);

            // Assert
            Assert.IsFalse(isLegalMoveAcrossColumns);
        }

        [Test]
        public void IsLegalMoveAcrossColumns_IsFalse_Test_002()
        {
            // Arrange IsFalse because own piece encountered
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("H5");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A8");
            ChessPiece secondPiece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "H8");
            // Fake previous moves that places pieces in desired locations.
            piece.CurrentLocation = origin;
            secondPiece.CurrentLocation = gc.GetLocation("B5");

            // Act
            bool isLegalMoveAcrossColumns =
                MoveAssistant.IsLegalMoveAcrossColumns(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination);

            // Assert
            Assert.IsFalse(isLegalMoveAcrossColumns);
        }

        [Test]
        public void IsLegalMoveAcrossColumns_IsFalse_Test_003()
        {
            // Arrange incremented IsFalse because opponent piece encountered
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("H5");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A8");
            ChessPiece opponentPiece = gc.Players[otherPlayerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A1");
            // Fake previous moves that places pieces in desired locations.
            piece.CurrentLocation = origin;
            opponentPiece.CurrentLocation = gc.GetLocation("B5");

            // Act
            bool isLegalMoveAcrossColumns =
                MoveAssistant.IsLegalMoveAcrossColumns(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination);

            // Assert
            Assert.IsFalse(isLegalMoveAcrossColumns);
        }

        [Test]
        public void IsLegalMoveAcrossColumns_IsFalse_Test_004()
        {
            // Arrange decremented IsFalse because opponent piece encountered
            GameController gc = new GameController();

            Location origin = gc.GetLocation("H5");
            Location destination = gc.GetLocation("A5");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A8");
            ChessPiece opponentPiece = gc.Players[otherPlayerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A1");
            // Fake previous moves that places pieces in desired locations.
            piece.CurrentLocation = origin;
            opponentPiece.CurrentLocation = gc.GetLocation("G5");

            // Act
            bool isLegalMoveAcrossColumns =
                MoveAssistant.IsLegalMoveAcrossColumns(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination);

            // Assert
            Assert.IsFalse(isLegalMoveAcrossColumns);
        }

        #endregion

        #region Test Moves Through Rows

        [Test]
        public void IsLegalMoveThroughRows_IsTrue_Test_001()
        {
            // Arrange through rows
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("A1");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A8");
            // Fake a previous move that places piece in desired location.
            piece.CurrentLocation = origin;

            // Act
            bool isLegalMoveThroughRows =
                MoveAssistant.IsLegalMoveThroughRows(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination);

            // Assert
            Assert.IsTrue(isLegalMoveThroughRows);
        }

        [Test]
        public void IsLegalMoveThroughRows_IsTrue_Test_002()
        {
            //Arrange is legal move through rows with capture
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("A1");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A8");
            // Fake previous moves that places pieces in desired location.
            ChessPiece opponentPiece = gc.Players[otherPlayerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A1");
            piece.CurrentLocation = origin;

            // Act
            bool isLegalMoveThroughRows =
                MoveAssistant.IsLegalMoveThroughRows(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination);

            // Assert
            Assert.IsTrue(isLegalMoveThroughRows);
        }

        [Test]
        public void IsLegalMoveThroughRows_IsFalse_Test_001()
        {
            // Arrange IsFalse because not through rows
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("H5");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A8");
            // Fake a previous move that places piece in desired location.
            piece.CurrentLocation = origin;

            // Act
            bool isLegalMoveThroughRows =
                MoveAssistant.IsLegalMoveThroughRows(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination);

            // Assert
            Assert.IsFalse(isLegalMoveThroughRows);
        }

        [Test]
        public void IsLegalMoveThroughRows_IsFalse_Test_002()
        {
            // Arrange IsFalse because encountered own piece
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("A1");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A8");
            ChessPiece secondPiece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "H8");
            // Fake previous moves that places pieces in desired locations.
            piece.CurrentLocation = origin;
            secondPiece.CurrentLocation = gc.GetLocation("A4");

            // Act
            bool isLegalMoveThroughRows =
                MoveAssistant.IsLegalMoveThroughRows(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination);

            // Assert
            Assert.IsFalse(isLegalMoveThroughRows);
        }

        [Test]
        public void IsLegalMoveThroughRows_IsFalse_Test_003()
        {
            // Arrange incremented IsFalse because opponent piece encountered 
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A1");
            Location destination = gc.GetLocation("A5");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A8");
            ChessPiece opponentPiece = gc.Players[otherPlayerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A1");
            // Fake previous moves that places pieces in desired locations.
            piece.CurrentLocation = origin;
            opponentPiece.CurrentLocation = gc.GetLocation("A2");

            // Act
            bool isLegalMoveThroughRows =
                MoveAssistant.IsLegalMoveThroughRows(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination);

            // Assert
            Assert.IsFalse(isLegalMoveThroughRows);
        }

        [Test]
        public void IsLegalMoveThroughRows_IsFalse_Test_004()
        {
            // Arrange decremented IsFalse because opponent piece encountered 
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("A1");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A8");
            ChessPiece opponentPiece = gc.Players[otherPlayerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A1");
            // Fake previous moves that places pieces in desired locations.
            piece.CurrentLocation = origin;
            opponentPiece.CurrentLocation = gc.GetLocation("A4");

            // Act
            bool isLegalMoveThroughRows =
                MoveAssistant.IsLegalMoveThroughRows(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination);

            // Assert
            Assert.IsFalse(isLegalMoveThroughRows);
        }

        [Test]
        public void IsLegalMoveThroughRows_IsFalse_Test_005()
        {
            // Arrange IsFalse because exceeded limit
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A7");
            Location destination = gc.GetLocation("A4");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A7");
            // Fake previous moves that places pieces in desired locations.
            piece.CurrentLocation = origin;

            // Act
            bool isLegalMoveThroughRows =
                MoveAssistant.IsLegalMoveThroughRows(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination, 2, false);

            // Assert
            Assert.IsFalse(isLegalMoveThroughRows);
        }

        [Test]
        public void IsLegalMoveThroughRows_IsFalse_Test_006()
        {
            // Arrange IsFalse because exceeded limit for subsequent moves of pawn
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("A3");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A7");
            // Fake previous moves that places pieces in desired locations.
            piece.PreviousLocation = gc.GetLocation("A7");
            piece.CurrentLocation = origin;

            // Act
            bool isLegalMoveThroughRows =
                MoveAssistant.IsLegalMoveThroughRows(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination, 1, false);

            // Assert
            Assert.IsFalse(isLegalMoveThroughRows);
        }

        [Test]
        public void IsLegalMoveThroughRows_IsFalse_Test_007()
        {
            // Arrange IsFalse because pawn retreated
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("A6");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A7");
            // Fake previous moves that places pieces in desired locations.
            piece.CurrentLocation = origin;

            // Act
            bool isLegalMoveThroughRows =
                MoveAssistant.IsLegalMoveThroughRows(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination, 2, false);

            // Assert
            Assert.IsFalse(isLegalMoveThroughRows);
        }


        #endregion

        #region Test Diagonal Moves

        [Test]
        public void IsLegalMoveDiagonally_IsTrue_Test_001()
        {
            // Arrange diagonal capture by pawn
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A7");
            Location destination = gc.GetLocation("B6");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A7");
            ChessPiece opponentPiece = gc.Players[otherPlayerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "B2");
            // Fake previous moved that places pieced in desired locations.
            piece.CurrentLocation = origin;
            opponentPiece.CurrentLocation = gc.GetLocation("B6");

            // Act
            bool isLegalMoveDiagonally =
                MoveAssistant.IsLegalMoveDiagonally(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination, 1, false);
            Assert.IsTrue(isLegalMoveDiagonally);
        }

        [Test]
        public void IsLegalMoveDiagonally_IsFalse_Test_001()
        {
            // Arrange diagonal move  where occupied by own
            GameController gc = new GameController();

            Location origin = gc.GetLocation("A7");
            Location destination = gc.GetLocation("B6");
            int playerIndex = 1;
            int otherPlayerIndex = gc.GetOtherPlayerIndex(playerIndex);
            ChessPiece piece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "A7");
            ChessPiece otherPiece = gc.Players[playerIndex].Pieces
                .SingleOrDefault(p => p.CurrentLocation.Coordinate == "B7");
            // Fake previous moved that places pieces in desired locations.
            piece.CurrentLocation = origin;
            otherPiece.CurrentLocation = destination;

            // Act
            bool isLegalMoveDiagonally =
                MoveAssistant.IsLegalMoveDiagonally(
                    gc.Players[playerIndex], gc.Players[otherPlayerIndex], origin, destination, 1, false);
            Assert.IsFalse(isLegalMoveDiagonally);
        }

        #endregion

    }
}