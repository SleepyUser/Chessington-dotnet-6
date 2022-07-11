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
            RelativeMove(currentloc, playerMod, availableMoves, 0, 1);

            if (!moved)
            {
                RelativeMove(currentloc, playerMod, availableMoves, 0, 2);
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