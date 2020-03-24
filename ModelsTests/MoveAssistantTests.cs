using NUnit.Framework;
using System.Linq;
using Models;
using Chess;
using Models.ChessPieces;

namespace ModelsTests
{   
    public class Tests
    {
        //ToDo: Add more tests, especially for edge cases.

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
            opponentPiece.CurrentLocation = gc.GetLocation("A4");

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


        #endregion

    }
}