using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using Models;

namespace Chess
{
    public partial class ChessForm : Form
    {
        // Start a new game.
        private GameController game;

        public ChessForm()
        {
            InitializeComponent();
        }

        private void GameBoard_Load(object sender, EventArgs e)
        {
            LoadGameboard();
        }

        // ToDo: Protect against lower case column inputs
        // ToDo: Figure out why inputs retaining previous entries between moves
        private void btnWhiteMove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            int white = 1;

            // Get white player's piece's from and to locations
            Location origin = game.GetLocation(txtWhiteFrom.Text);
            Location destination = game.GetLocation(txtWhiteDestination.Text);

            //Try to execute the move
            string message;
            bool didExecuteMove = game.DidExecuteMove(white, origin, destination, out message);

            if (didExecuteMove)
            {
                UpdateBoard(white, origin, destination, message);
                UpdateCaptures(white, "lblWhiteCaptures");

                grpWhite.Visible = false;
                grpWhite.Enabled = false;
                grpBlack.Visible = true;
                grpBlack.Enabled = true;
                btnBlackMove.Enabled = true;
            }
            else
            {
                btn.Enabled = true;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = message;
            }
        }

        private void btnBlackMove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            int black = 0;

            // Get black player's piece's from and to locations
            Location origin = game.GetLocation(txtBlackFrom.Text);
            Location destination = game.GetLocation(txtBlackDestination.Text);

            //Try to execute the move
            string message = string.Empty;
            bool didExecuteMove = game.DidExecuteMove(black, origin, destination, out message);

            if (didExecuteMove)
            {
                UpdateBoard(black, origin, destination, message);
                UpdateCaptures(black, "lblBlackCaptures");

                grpBlack.Visible = false;
                grpBlack.Enabled = false;
                grpWhite.Visible = true;
                grpWhite.Enabled = true;
                btnWhiteMove.Enabled = true;
            }
            else
            {
                btn.Enabled = true;
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = message;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadGameboard();
        }


        #region Private Methods

        // ToDo: Finish resetting of board, fix issue with Move not enabled after Reset
        private void LoadGameboard()
        {
            game = new GameController();

            // Clear all textboxes
            ClearTextboxes(Controls);

            // Set other controls to initial state
            grpWhite.Visible = true;
            grpWhite.Enabled = true;
            lblWhiteCaptures.Text = string.Empty;
            txtBlackFrom.Focus();
            btnWhiteMove.Enabled = true;

            grpBlack.Visible = false;
            grpBlack.Enabled = false;
            lblBlackCaptures.Text = string.Empty;

            lblMessage.Text = string.Empty;
            
            // Set up the pieces in the textboxes for each player
            for (int i = 0; i < game.Players.Count; i++)
            {
                foreach (var piece in game.Players[i].Pieces)
                {
                    if (FindControl(this, piece.CurrentLocation.Coordinate) != null)
                    {
                        TextBox tbx = FindControl(this, piece.CurrentLocation.Coordinate) as TextBox;
                        tbx.Text = piece.Text;
                        SetBackcolorOfBoard(tbx);
                        tbx.ForeColor = game.Players[i].PlayerColor;
                        tbx.ReadOnly = true;
                    }
                }
            }
        }       

        private void UpdateBoard(int playerIndex, Location origin, Location destination, string message)
        {
            TextBox originTextbox = FindControl(this, origin.Coordinate) as TextBox;
            TextBox destinationTextbox = FindControl(this, destination.Coordinate) as TextBox;

            destinationTextbox.Text = originTextbox.Text;
            // "Changing" backcolor allows forecolor of readonly textbox to be changed
            destinationTextbox.BackColor = destinationTextbox.BackColor;
            destinationTextbox.ForeColor = game.Players[playerIndex].PlayerColor;
            originTextbox.Text = string.Empty;

            lblMessage.ForeColor = Color.Black;
            lblMessage.Text = message;
        }

        private void UpdateCaptures(int playerIndex, string labelName)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;

            if (game.Players[playerIndex].CapturedPieces != null)
            {
                foreach (var capturedPiece in game.Players[playerIndex].CapturedPieces)
                {
                    sb.Append($"{capturedPiece.Text} ");
                    if (index == 8)
                        sb.Append(Environment.NewLine);
                    index++;
                }

                Label capturesLabel = FindControl(this, labelName) as Label;
                capturesLabel.Text = sb.ToString();
            }
        }

        private void ClearTextboxes(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tbx = ctrl as TextBox;
                    tbx.Text = string.Empty;
                    SetBackcolorOfBoard(tbx);
                    tbx.ForeColor = Color.Black;
                    tbx.ReadOnly = false;
                }

                ClearTextboxes(ctrl.Controls);
            }
        }

        /// <summary>Finds the control recursively.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <returns>The Control</returns>
        private Control FindControl(Control parent, string name)
        {
            // Check the parent.
            if (parent.Name == name)
                return parent;

            // Recursively search the parent's children.
            foreach (Control ctl in parent.Controls)
            {
                Control found = FindControl(ctl, name);
                if (found != null) 
                    return found;
            }

            // If we still haven't found it, it's not here.
            return null;
        }

        private void SetBackcolorOfBoard(TextBox tbx)
        {
            if (tbx.Name.Length > 2)
                return;

            if (IsBlackSquare(tbx.Name))
                tbx.BackColor = Color.DarkGray;
            else
                tbx.BackColor = DefaultBackColor;
        }

        private bool IsInEvenRow(string name)
        {
            int rowPart = int.Parse(name.Substring(1, 1));

            if (rowPart % 2 == 0)
                return true;
            else
                return false;
        }

        private bool IsBlackSquare(string name)
        {
            string colPart = name.Substring(0, 1);

            if (IsInEvenRow(name))
            {
                List<string> evenLetters = new List<string>() { "A", "C", "E", "G" };
                if (evenLetters.Contains(colPart))
                    return true;
            }
            else
            {
                //True if colPart contains B, D, F, H
                List<string> oddLetters = new List<string>() { "B", "D", "F", "H" };
                if (oddLetters.Contains(colPart))
                    return true;
            }

            // If we got here, doesn't meet any of the criteria.
            return false;
        }

        #endregion
       
    }
}