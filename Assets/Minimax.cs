using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Move
{
    public int x;
    public int y;
    public int value;
    public double random;

    public Move(int x, int y, int value)
    {
        this.x = x;
        this.y = y;
        this.value = value;
    }

    public static readonly Move Empty = new Move(-1, -1, -1);

}

public class Minimax : MonoBehaviour {
    public static Minimax instance;
    public int playerColor = -1;
    public int bug = 0;

    void Awake()
    {
        instance = this;
    }

    

    public int MiniMax( Board board, int depth, int currentPlayer, bool clean)
    {
        // If the game is over, terminate the search
        if (board.IsGameOver())
        {
            int value = board.StaticEvaluator(this.playerColor);
            return value;
        }

        // At the bottom of the search space
        if (depth == 0)
        {
            int value = board.StaticEvaluator(this.playerColor);
            return value;
        }

        // If this player can't play, skip turn
        if (!board.CanPlay(currentPlayer))
        {
            int value = MiniMax( board, depth, -currentPlayer,false);
            return value;
        }

        // Get the list of plausable moves, sorted by most pieces flipped
        List<Move> moves = board.MoveGenerator(currentPlayer);

        // Find the best move avaiable
        Move bestMove = Move.Empty;
        int bestValue;
        if (currentPlayer == this.playerColor)
            bestValue = int.MinValue;
        else
            bestValue = int.MaxValue;

        foreach (Move currentMove in moves)
        {
            // Copy the board
            Board currentBoard;

            currentBoard = board.Clone();

            // Make the selected move
            
            if (currentBoard.isCopy)
            {
                currentBoard.Move(currentMove, currentPlayer,false);
            }

            int currentValue = MiniMax(currentBoard, depth - 1, -currentPlayer,false);

            // MAX
            if (currentPlayer == this.playerColor)
            {
                if (currentValue > bestValue)
                {
                    bestValue = currentValue;
                    bestMove = currentMove;
                }
            }
            // MIN
            else
            {
                if (currentValue < bestValue)
                {
                    bestValue = currentValue;
                    bestMove = currentMove;
                }
            }

        }

        if (!bestMove.Equals(Move.Empty))
        {
            if (board.isCopy == false)
            {
                board.Move(bestMove,-1,true);
            }

        }
        
        return bestValue;
    }
}
