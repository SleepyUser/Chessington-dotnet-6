using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square currentLoc = board.FindPiece(this);
            List<Square> availableMoves = new List<Square>();
            int playerMod = (Player == Player.White) ? -1 : 1; //Modifies the move based on the owner
            for(int i = 0; i < GameSettings.BoardSize; i++)
            {
                RelativeMove(currentLoc, availableMoves, i, currentLoc.Row, board, ignorePath:false);
                RelativeMove(currentLoc, availableMoves, -i, currentLoc.Row, board, ignorePath:false);
                RelativeMove(currentLoc, availableMoves, currentLoc.Col, i, board, ignorePath:false);
                RelativeMove(currentLoc, availableMoves, currentLoc.Col, -i, board, ignorePath:false);
            }
            
            Console.WriteLine(availableMoves);
            return availableMoves;
        }
    }
}