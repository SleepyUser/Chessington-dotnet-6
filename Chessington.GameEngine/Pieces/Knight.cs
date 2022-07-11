using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square currentloc = board.FindPiece(this);
            List<Square> availableMoves = new List<Square>();
            int playerMod = (Player == Player.White) ? -1 : 1; //Modifies the move based on the owner
            RelativeMove(currentloc, playerMod, availableMoves, 2, 1, board);
            RelativeMove(currentloc, playerMod, availableMoves, 2, -1, board);
            RelativeMove(currentloc, playerMod, availableMoves, -2, 1, board);
            RelativeMove(currentloc, playerMod, availableMoves, -2, -1, board);
            RelativeMove(currentloc, playerMod, availableMoves, 1, -2, board);
            RelativeMove(currentloc, playerMod, availableMoves, 1, 2, board);
            RelativeMove(currentloc, playerMod, availableMoves, -1, -2, board);
            RelativeMove(currentloc, playerMod, availableMoves, -1, 2, board);

            return availableMoves;
        }
    }
}