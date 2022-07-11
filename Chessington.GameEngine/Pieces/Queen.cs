using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square currentLoc = board.FindPiece(this);
            List<Square> availableMoves = new List<Square>();
            int playerMod = (Player == Player.White) ? -1 : 1; //Modifies the move based on the owner
            for (int i = 0; i < GameSettings.BoardSize; i++)
            {
                RelativeMove(currentLoc, availableMoves, i, -i, board, ignorePath: false);
                RelativeMove(currentLoc, availableMoves, -i, i, board, ignorePath:false);
                RelativeMove(currentLoc, availableMoves, -i, -i, board, ignorePath:false);
                RelativeMove(currentLoc, availableMoves, i, i, board, ignorePath:false);
                RelativeMove(currentLoc, availableMoves, i, 0, board, ignorePath:false); 
                RelativeMove(currentLoc, availableMoves, 0, i, board, ignorePath:false); 
                RelativeMove(currentLoc, availableMoves, -i, 0, board, ignorePath:false); 
                RelativeMove(currentLoc, availableMoves, 0, -i, board, ignorePath:false); 
            }

            availableMoves = availableMoves.Distinct().ToList();
            return availableMoves;
        }
    }
}