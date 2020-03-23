using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Models;
using Chess;
using Models.ChessPieces;

namespace ModelsTests
{   
    public class Tests
    {
        private GameController gc = new GameController();

        #region Test Moves Across Columns

        [Test]
        public void IsLegalMoveAcrossColumns_Pass_Test_001()
        {
            // Arrange across columns
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
        public void IsLegalMoveAcrossColumns_Fail_Test_001()
        {
            // Arrange fail because not across columns
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
        public void IsLegalMoveAcrossColumns_Pass_Test_002()
        {
            // Arrange across columns with capture
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
        public void IsLegalMoveAcrossColumns_Fail_Test_002()
        {
            // Arrange fail because own piece encountered
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
        public void IsLegalMoveAcrossColumns_Fail_Test_003()
        {
            // Arrange incremented fail because opponent piece encountered
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
        public void IsLegalMoveAcrossColumns_Fail_Test_004()
        {
            // Arrange decremented fail because opponent piece encountered
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
        public void IsLegalMoveThroughRows_Pass_Test_001()
        {
            // Arrange through rows
            Location origin = gc.GetLocation("A5");
            Location destination = gc.GetLocation("A1");
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

        #endregion

    }
}