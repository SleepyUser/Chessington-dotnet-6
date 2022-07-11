using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square currentloc = board.FindPiece(this);
            List<Square> availableMoves = new List<Square>();
            int playerMod = (Player == Player.White) ? -1 : 1; //Modifies the move based on the owner
            Square s = Square.At(currentloc.Row + 1*playerMod, currentloc.Col); // Gets the appropriate square
            availableMoves.Add(s); // Adds move to list
            return availableMoves;
        }
    }
}