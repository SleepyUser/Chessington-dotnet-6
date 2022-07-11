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
        
        protected void RelativeMove(Square currentLoc, List<Square> availableMoves, int colMod, int rowMod, Board board, int playerMod = 1, bool ignorePath = false, bool canTake = true)
        {
            int rowTarget = currentLoc.Row + (rowMod * playerMod);
            int colTarget = currentLoc.Col + (colMod * playerMod);
            Square s = Square.At(rowTarget, colTarget); // Gets the appropriate square
            if(rowTarget < 0 || rowTarget > GameSettings.BoardSize-1 || colTarget < 0 || colTarget > GameSettings.BoardSize-1) // if invalid index
                return;
            if (s == currentLoc) // if current index
                return;
            if (ignorePath == false)
            {
                s = ClearPath(currentLoc, s, board);
                if (s.Col == -1) //if path is NOT clear (allied piece), return
                    return;
            }

            if (board.GetPiece(s) != null)
            {
                if (board.GetPiece(s).Player == Player)
                    return;
                else if (board.GetPiece(s).Player != Player && canTake == false)
                    return;
            }

            if (!availableMoves.Contains(s)) //if it is not already in the list
                availableMoves.Add(s); // Adds move to list
            else
                return; 
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

        /// <summary>
        /// Checks direct route from start to finish square.
        /// Returns first square with enemy piece
        /// Returns Square(-1,-1) upon reaching first allied piece
        /// Returns finish square if path is clear
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        private Square ClearPath(Square start, Square finish, Board board)
        {
            int c = start.Col;
            int r = start.Row;
            Square checkSquare = new Square(-1, -1);
            
            while (c != finish.Col || r != finish.Row)
            {
                if (finish.Col > start.Col)
                {
                    c += 1;
                }
                else if (finish.Col < start.Col)
                {
                    c -= 1;
                }
                if (finish.Row > start.Row)
                {
                    r += 1;
                }
                else if (finish.Row < start.Row)
                {
                    r -= 1;
                }

                checkSquare = Square.At(r, c);
                if (board.GetPiece(checkSquare) != null)
                {
                    if (board.GetPiece(checkSquare).Player != Player)
                        return checkSquare;
                    else if (board.GetPiece(checkSquare).Player == Player)
                        return new Square(-1, -1);
                }
            }
            return checkSquare;
        }
    }
}