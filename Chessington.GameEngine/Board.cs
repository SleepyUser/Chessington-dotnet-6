using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.CompilerServices;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class Board
    {
        private readonly Piece[,] _board;
        public Player CurrentPlayer { get; private set; }
        public IList<Piece> CapturedPieces { get; private set; } 

        public Board()
            : this(Player.White) { }

        public Board(Player currentPlayer, Piece[,] boardState = null)
        {
            _board = boardState ?? new Piece[GameSettings.BoardSize, GameSettings.BoardSize]; 
            CurrentPlayer = currentPlayer;
            CapturedPieces = new List<Piece>();
        }

        public void AddPiece(Square square, Piece pawn)
        {
            _board[square.Row, square.Col] = pawn;
        }
    
        public Piece GetPiece(Square square)
        {
            return _board[square.Row, square.Col];
        }
        
        public Square FindPiece(Piece piece)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
                for (var col = 0; col < GameSettings.BoardSize; col++)
                    if (_board[row, col] == piece)
                        return Square.At(row, col);

            throw new ArgumentException("The supplied piece is not on the board.", "piece");
        }

        public void MovePiece(Square from, Square to, bool enPassant = false, bool castling = false)
        {
            var movingPiece = _board[from.Row, from.Col];
            if (movingPiece == null) { return; }

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }
            else if (castling == true)
            {
                throw new NotImplementedException(); //Need to implement check for check to disallow move
            }

            //If the space we're moving to is occupied, we need to mark it as captured.
            if (_board[to.Row, to.Col] != null)
            {
                CapturedPieces.Add(_board[to.Row, to.Col]); //Currently does not function, fix for this
                OnPieceCaptured(_board[to.Row, to.Col]);
            }
            else if (enPassant) //Else, if enPassant is true
            {
                CapturedPieces.Add(_board[from.Row, to.Col]); //Currently does not function, fix for this
                OnPieceCaptured(_board[from.Row, to.Col]);
                _board[from.Row, to.Col] = null;
            }

            //Move the piece and set the 'from' square to be empty.
            _board[to.Row, to.Col] = _board[from.Row, from.Col];
            _board[from.Row, from.Col] = null;

            CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;
            OnCurrentPlayerChanged(CurrentPlayer);
        }

        public delegate void PieceCapturedEventHandler(Piece piece);
        
        public event PieceCapturedEventHandler PieceCaptured;

        protected virtual void OnPieceCaptured(Piece piece)
        {
            var handler = PieceCaptured;
            handler?.Invoke(piece);
        }

        public delegate void CurrentPlayerChangedEventHandler(Player player);

        public event CurrentPlayerChangedEventHandler CurrentPlayerChanged;

        protected virtual void OnCurrentPlayerChanged(Player player)
        {
            var handler = CurrentPlayerChanged;
            handler?.Invoke(player);
        }

        public bool CheckCheck(Player threatened)
        {
            foreach (var piece in _board)
            {
                if (piece.Player != threatened)
                {
                    foreach (Square s in piece.GetAvailableMoves(this))
                    {
                        Piece squarePiece = GetPiece(s);
                        if (squarePiece != null && squarePiece.GetType() == typeof(King))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckMateCheck(Player threatened)
        {
            if (CheckCheck(threatened))
            {
                Board parallelBoard = null;
                foreach (var piece in _board)
                {
                    if (piece.Player != threatened)
                    {
                        parallelBoard= (Board)this.MemberwiseClone();
                        foreach (Square s in piece.GetAvailableMoves(this))
                        {
                            Piece squarePiece = GetPiece(s);
                            squarePiece.MoveTo(parallelBoard,s);
                            if (squarePiece != null && squarePiece.GetType() == typeof(King))
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }

            return false;
        }
    }
}
