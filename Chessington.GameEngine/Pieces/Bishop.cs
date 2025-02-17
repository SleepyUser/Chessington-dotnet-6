﻿using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square currentloc = board.FindPiece(this);
            List<Square> availableMoves = new List<Square>();
            int playerMod = (Player == Player.White) ? -1 : 1; //Modifies the move based on the owner
            for (int i = 0; i < GameSettings.BoardSize; i++)
            {
                RelativeMove(currentloc, availableMoves, i, -i, board);
                RelativeMove(currentloc, availableMoves, -i, i, board);
                RelativeMove(currentloc, availableMoves, -i, -i, board);
                RelativeMove(currentloc, availableMoves, i, i, board);
            }

            availableMoves = availableMoves.Distinct().ToList();
            return availableMoves;
        }
    }
}