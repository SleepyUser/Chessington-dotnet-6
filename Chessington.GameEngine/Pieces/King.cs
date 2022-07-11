using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square currentloc = board.FindPiece(this);
            List<Square> availableMoves = new List<Square>();
            int playerMod = (Player == Player.White) ? -1 : 1; //Modifies the move based on the owner
            
            RelativeMove(currentloc, availableMoves, 1, -1, board);
            RelativeMove(currentloc, availableMoves, 1, 1, board);
            RelativeMove(currentloc, availableMoves, -1, -1, board);
            RelativeMove(currentloc, availableMoves, -1, 1, board);
            RelativeMove(currentloc, availableMoves, 0, -1, board);
            RelativeMove(currentloc, availableMoves, 0, 1, board);
            RelativeMove(currentloc, availableMoves, 1, 0, board);
            RelativeMove(currentloc, availableMoves, -1, 0, board);

            return availableMoves;
        }
    }
}