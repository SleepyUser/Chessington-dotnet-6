using System.Collections.Generic;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        protected Piece(Player player)
        {
            Player = player;
        }

        public Player Player { get; private set; }

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public virtual void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
        }
        
        protected void RelativeMove(Square currentLoc, int playerMod, List<Square> availableMoves, int colMod, int rowMod, Board board)
        {
            int rowTarget = currentLoc.Row + (rowMod * playerMod);
            int colTarget = currentLoc.Col + (colMod * playerMod);
            Square s = Square.At(rowTarget, colTarget); // Gets the appropriate square
            if(rowTarget < 0 || rowTarget > GameSettings.BoardSize-1 || colTarget < 0 || colTarget > GameSettings.BoardSize-1) // if invalid index
                return;
            if (s == currentLoc) // if current index
                return;
            if (board.GetPiece(s) != null) 
                return;
            availableMoves.Add(s); // Adds move to list
        }

        protected void AbsoluteMove(Square currentLoc, List<Square> availableMoves, int col,
            int row)
        {
            Square s = Square.At(row, col); // Gets the appropriate square
            if (s == currentLoc)
            {
                return;
            }
            availableMoves.Add(s); // Adds move to list
        }
    }
}