using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

[System.Serializable]
public class Board : MonoBehaviour
{
    public List<List<Slot>> slots;
    public Move nextMove;
    public bool isCopy = false;
    public GameObject parent;

    public Board Clone()
    {
        Board b = new Board();

        b.slots = new List<List<Slot>>();
        b.isCopy = true;
        for (int i = 0; i < slots.Count; i++)
        {
            var lines = new List<Slot>();

            for (int j = 0; j < slots[i].Count; j++)
            {
                Slot s = slots[i][j].Clone() as Slot;

                if (slots[i][j].up)
                    s.up = slots[i][j].up.Clone() as Slot;
                if (slots[i][j].upLeft != null)
                    s.upLeft = slots[i][j].upLeft.Clone() as Slot;
                if (slots[i][j].upRight)
                    s.upRight = slots[i][j].upRight.Clone() as Slot;
                if (slots[i][j].down)
                    s.down = slots[i][j].down.Clone() as Slot;
                if (slots[i][j].downLeft)
                    s.downLeft = slots[i][j].downLeft.Clone() as Slot;
                if (slots[i][j].downRight)
                    s.downRight = slots[i][j].downRight.Clone() as Slot;
                if (slots[i][j].left)
                    s.left = slots[i][j].left.Clone() as Slot;
                if (slots[i][j].right)
                    s.right = slots[i][j].right.Clone() as Slot;

                lines.Add(s);
            }
            b.slots.Add(lines);
        }

        return b;
    }

    public Vector2 GetScore()
    {
        Vector2 score = new Vector2();
        
        foreach (List<Slot> slot in slots)
        {
            foreach (Slot s in slot)
            {
                if (s.state == Slot.SlotState.black)
                    score.y ++;
                else if (s.state == Slot.SlotState.white)
                {
                    score.x++;
                }
            }
        }
        return score;
    }

    public void Move(Move move, int player,bool forReal)
    {
        if (forReal)
        {
            slots[move.x][move.y].Play(player);
        }
    }

    public bool IsGameOver()
    {
        bool canPlayBlack = false;
        bool canPlayWhite = false;

        foreach (List<Slot> slot in slots)
        {
            foreach (Slot s in slot)
            {
                if(s.CanPlay(Slot.SlotState.black))
                   canPlayBlack = true;
                if (s.CanPlay(Slot.SlotState.white))
                   canPlayWhite = true;
            }
        }
        return !canPlayBlack && !canPlayWhite;
    }


    public int StaticEvaluator(int player)
    {
        
        Slot.SlotState playerState = GetPlayerState(player);
        int score = 0;
        foreach(List<Slot> slot in slots)
        {
            foreach(Slot s in slot)
            {
                if (s.state == playerState)
                    score += s.value;

            }
        }
        return score;
    }

    public Slot.SlotState GetPlayerState(int player)
    {
        Slot.SlotState playerState;
        if (player > 0)
            playerState = Slot.SlotState.white;
        else
            playerState = Slot.SlotState.black;
        return playerState;
    }

    public bool CanPlay(int player)
    {
        foreach (List<Slot> slot in slots)
        {
            foreach (Slot s in slot)
            {
                if (s.CanPlay(GetPlayerState(player)))
                    return true;
            }
        }
        return false;
    }

    public void MoveNext()
    {
        slots[nextMove.x][nextMove.y].Play(GameplayManager.instance.currentPlayer);
        GameplayManager.instance.currentPlayer = -GameplayManager.instance.currentPlayer;
    }

    public List<Move> MoveGenerator(int player)
    {
        List<Move> moves = new List<Move>();
        int i = 0;
        foreach (List<Slot> slot in slots)
        {
            int j = 0;
            foreach (Slot s in slot)
            {
                if (s.CanPlay(GetPlayerState(player)))
                    moves.Add(new Move(i,j,s.value));
                j++;
            }
            i++;
        }
        return moves;
    }
}
