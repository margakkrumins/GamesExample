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
        readonly GameController Game = new GameController();

        public ChessForm()
        {
            InitializeComponent();
        }

        private void GameBoard_Load(object sender, EventArgs e)
        {
            // Clear all textboxes.
            ClearTextboxes();
            grpBlack.Visible = false;
            grpBlack.Enabled = false;

            // Set up the pieces in the textboxes for each player
            for (int i = 0; i < Game.Players.Count; i++)
            {
                foreach (var piece in Game.Players[i].Pieces)
                {
                    if (FindControl(this, piece.CurrentLocation.Coordinate) != null)
                    {
                        TextBox tbx = FindControl(this, piece.CurrentLocation.Coordinate) as TextBox;
                        tbx.Text = piece.Text;
                        SetBackcolorOfBoard(tbx);
                        tbx.ForeColor = Game.Players[i].PlayerColor;
                        tbx.ReadOnly = true;
                    }
                }
            }            
        }

        private void btnWhiteMove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            int white = 1;

            // Get white player's piece's from and to locations
            Location origin = Game.GetLocation(txtWhiteFrom.Text);
            Location destination = Game.GetLocation(txtWhiteDestination.Text);

            //Try to execute the move
            string message = string.Empty;
            bool didExecuteMove = Game.DidExecuteMove(white, origin, destination, out message);

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
            Location origin = Game.GetLocation(txtBlackFrom.Text);
            Location destination = Game.GetLocation(txtBlackDestination.Text);

            //Try to execute the move
            string message = string.Empty;
            bool didExecuteMove = Game.DidExecuteMove(black, origin, destination, out message);

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

        private void UpdateBoard(int playerIndex, Location origin, Location destination, string message)
        {
            TextBox originTextbox = FindControl(this, origin.Coordinate) as TextBox;
            TextBox destinationTextbox = FindControl(this, destination.Coordinate) as TextBox;

            destinationTextbox.Text = originTextbox.Text;
            // "Changing" backcolor allows forecolor of readonly textbox to be changed
            destinationTextbox.BackColor = destinationTextbox.BackColor;
            destinationTextbox.ForeColor = Game.Players[playerIndex].PlayerColor;
            originTextbox.Text = string.Empty;

            lblMessage.ForeColor = Color.Black;
            lblMessage.Text = message;
        }

        private void UpdateCaptures(int playerIndex, string labelName)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;

            if (Game.Players[playerIndex].CapturedPieces != null)
            {
                foreach (var capturedPiece in Game.Players[playerIndex].CapturedPieces)
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
        
        private void ClearTextboxes()
        {
            foreach (var ctrl in Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tbx = ctrl as TextBox;
                    tbx.Text = string.Empty;
                    SetBackcolorOfBoard(tbx);
                    tbx.ForeColor = Color.Black;
                    tbx.ReadOnly = false;
                }
            }
        }

        /// <summary>Finds the control recursively.</summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <returns>The Control</returns>
        private Control FindControl(Control parent, string name)
        {
            // Check the parent.
            if (parent.Name == name) return parent;

            // Recursively search the parent's children.
            foreach (Control ctl in parent.Controls)
            {
                Control found = FindControl(ctl, name);
                if (found != null) return found;
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
    }
}