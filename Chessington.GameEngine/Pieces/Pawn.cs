using System.Collections.Generic;
using System.Linq;
using System.Windows.Shell;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        private bool moved = false;

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square currentloc = board.FindPiece(this);
            List<Square> availableMoves = new List<Square>();
            int playerMod = (Player == Player.White) ? -1 : 1; //Modifies the move based on the owner
            RelativeMove(currentloc, availableMoves, 0, 1, board, playerMod, canTake: false);

            if (!moved) // && availableMoves.Count > 0)
            {
                RelativeMove(currentloc, availableMoves, 0, 2, board, playerMod, canTake: false, ignorePath: false);
            }

            int rowTarget = currentloc.Row + (1 * playerMod);
            int colTarget = currentloc.Col + (1 * playerMod);
            Square s = Square.At(rowTarget, colTarget); // Gets the appropriate square
            
            //Diagonal only if piece to take
            if (!(rowTarget is < 0 or > GameSettings.BoardSize - 1 || colTarget is < 0 or > GameSettings.BoardSize - 1)) // if invalid index
            {
                if (s != currentloc) // if current index
                {
                    if (board.GetPiece(s) != null && board.GetPiece(s).Player != Player)
                    {
                        if (!availableMoves.Contains(s)) //if it is not already in the list
                            availableMoves.Add(s); // Adds move to list
                    }
                }
            }
            rowTarget = currentloc.Row + (1 * playerMod);
            colTarget = currentloc.Col + (-1 * playerMod);
            s = Square.At(rowTarget, colTarget); // Gets the appropriate square
            if (!(rowTarget is < 0 or > GameSettings.BoardSize - 1 || colTarget is < 0 or > GameSettings.BoardSize - 1)) // if invalid index
            {
                if (s != currentloc) // if current index
                {
                    if (board.GetPiece(s) != null && board.GetPiece(s).Player != Player)
                    {
                        if (!availableMoves.Contains(s)) //if it is not already in the list
                            availableMoves.Add(s); // Adds move to list
                    }
                }
            }
            return availableMoves;
        }


        public override void MoveTo(Board board, Square newSquare) //Overrides base method and just adds change to moved property
        {
            base.MoveTo(board, newSquare);
            moved = true;
        }
    }
}