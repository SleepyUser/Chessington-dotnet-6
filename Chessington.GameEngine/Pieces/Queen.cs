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
                RelativeMove(currentLoc, playerMod, availableMoves, i, -i, board);
                RelativeMove(currentLoc, playerMod, availableMoves, -i, i, board);
                RelativeMove(currentLoc, playerMod, availableMoves, -i, -i, board);
                RelativeMove(currentLoc, playerMod, availableMoves, i, i, board);
                AbsoluteMove(currentLoc, availableMoves, i, currentLoc.Row);
                AbsoluteMove(currentLoc, availableMoves, currentLoc.Col, i );
            }

            availableMoves = availableMoves.Distinct().ToList();
            return availableMoves;
        }
    }
}