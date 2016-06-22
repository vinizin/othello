using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Slot : MonoBehaviour
{
    public Animator anim;
    public Slot up;
    public Slot right;
    public Slot down;
    public Slot left;
    public Slot upRight;
    public Slot upLeft;
    public Slot downRight;
    public Slot downLeft;
    public enum SlotState
    {
        empty,
        black,
        white
    };

    public enum Direction
    {
        up,
        right,
        down,
        left,
        upRight,
        upLeft,
        downRight,
        downLeft
    };

    public SlotState state;

    public bool canPlay;
    
    public bool CanPlay
    {
        get
        {
            bool hasNeighbor = false;
            if (up != null && up.state != SlotState.empty ||
                down != null && down.state != SlotState.empty ||
                right != null && right.state != SlotState.empty ||
                left != null && left.state != SlotState.empty
                ) hasNeighbor = true;

            bool willScore = false;
            var values = Enum.GetValues(typeof(Direction));

            foreach (Direction d in values)
            {
                if (CheckUpdateNeighbor(GameplayManager.instance.GetPlayerState(), d))
                {
                    willScore = true;
                    break;
                }
            }

            return state == SlotState.empty && hasNeighbor && willScore;

        }
        set { }
    }

    bool IsEnemy(Direction d)
    {
        if (GetDirection(d).state != GameplayManager.instance.GetPlayerState())
        {
            return true;
        }
        return false;
    }


    // Use this for initialization
    void Awake() {
        state = SlotState.empty;
    }

    // Update is called once per frame
    void Update() {
        canPlay = CanPlay;
        anim.SetInteger("State", (int)state);
    }

    public void Play(int player)
    {
        SlotState playerState;
        if (player > 0)
            playerState = SlotState.white;
        else
            playerState = SlotState.black;

        state = playerState;

        var values = Enum.GetValues(typeof(Direction));
        foreach (Direction d in values)
        {
           StartCoroutine( UpdateNeighbor(playerState, d));
        }
    }

    Slot GetDirection(Direction dir)
    {
        switch(dir)
        {
            case Direction.up:
                return up;
            case Direction.down:
                return down;
            case Direction.right:
                return right;
            case Direction.left:
                return left;
            case Direction.upRight:
                return upRight;
            case Direction.upLeft:
                return upLeft;
            case Direction.downRight:
                return downRight;
            case Direction.downLeft:
                return downLeft;
            default:
                return this;
        }
    }

    public IEnumerator UpdateNeighbor(SlotState playerState,Direction dir)
    {

        Slot mainSlot = this;
        List<Slot> slotsToChange = new List<Slot>();

        while (mainSlot.GetDirection(dir) != null && mainSlot.GetDirection(dir).state != playerState && mainSlot.GetDirection(dir).state != SlotState.empty)
        {
            mainSlot = mainSlot.GetDirection(dir);
            slotsToChange.Add(mainSlot);
        }

        if (mainSlot.GetDirection(dir) != null && mainSlot.GetDirection(dir).state == playerState)
        {
            foreach (Slot s in slotsToChange)
            {
                yield return new WaitForSeconds(0.1f);
                s.state = playerState;
            }
        }

    }

    public bool CheckUpdateNeighbor(SlotState playerState, Direction dir)
    {
        Slot mainSlot = this;
        List<Slot> slotsToChange = new List<Slot>();

        while (mainSlot.GetDirection(dir) != null && mainSlot.GetDirection(dir).state != playerState && mainSlot.GetDirection(dir).state != SlotState.empty)
        {
            mainSlot = mainSlot.GetDirection(dir);
            slotsToChange.Add(mainSlot);
        }

        if (mainSlot.GetDirection(dir) != null && mainSlot.GetDirection(dir).state == playerState)
        {
            if (slotsToChange.Count == 1 && slotsToChange[0] == this)
                return false;
            if (slotsToChange.Count > 0)
                return true;
        }
        return false;
    }

}
