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
            
            RelativeMove(currentloc, playerMod, availableMoves, 1, -1);
            RelativeMove(currentloc, playerMod, availableMoves, 1, 1);
            RelativeMove(currentloc, playerMod, availableMoves, -1, -1);
            RelativeMove(currentloc, playerMod, availableMoves, -1, 1);
            RelativeMove(currentloc, playerMod, availableMoves, 0, -1);
            RelativeMove(currentloc, playerMod, availableMoves, 0, 1);
            RelativeMove(currentloc, playerMod, availableMoves, 1, 0);
            RelativeMove(currentloc, playerMod, availableMoves, -1, 0);

            return availableMoves;
        }
    }
}