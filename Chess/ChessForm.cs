using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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

            // Set up the pieces in the textboxes for each player
            for (int i = 0; i < Game.Players.Count; i++)
            {
                foreach (var piece in Game.Players[i].Pieces)
                {
                    if (FindControl(this, piece.CurrentLocation.Coordinate) != null)
                    {
                        TextBox tbx = FindControl(this, piece.CurrentLocation.Coordinate) as TextBox;
                        tbx.Text = piece.Text;
                        SetBackcolor(tbx);
                        tbx.ForeColor = Game.Players[i].PlayerColor;
                        tbx.ReadOnly = true;
                    }
                }
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
                    SetBackcolor(tbx);
                    tbx.ForeColor = Color.Black;
                    tbx.ReadOnly = false;
                }
            }
        }
        
        // Recursively find the named control.
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

        private void SetBackcolor(TextBox tbx)
        {
            if (tbx.Name.Length > 2)
                return;

            if (IsBlackSquare(tbx.Name))
                tbx.BackColor = Color.DarkGray;
            else
                tbx.BackColor = DefaultBackColor;
        }

        /// <summary>
        /// Checks if name indicates item is in an even row.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>True, if it is; else, false</returns>
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

        private void btnWhiteMove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;

            // Get white player's piece's from and to locations
            Location origin = Game.GetLocation(txtWhiteFrom.Text);
            Location destination = Game.GetLocation(txtWhiteDestination.Text);

            //Try to execute the move
            string message = string.Empty;
            bool didExecuteMove = Game.DidExecuteMove(1, origin, destination, out message);

            if (didExecuteMove)
            {
                //ToDo: Get value of text at origin control, 
                    //assign its value to destination control
                    //Set value of origin control to string.empty


                //ToDo: Update UI labels, textboxes, and buttons
            }
            else
            {
                lblMessage.Text = message;
            }

        }
    }
}