using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Game : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<List<Slot>> slots;
    public static Game instance;
    public Board originalBoard;
    public Board copy;
    public int[] values1;
    public int copiesSize;

    void Awake()
    {
        instance = this;
        values1 = new int[64]{
                         120, -20, 20, 5, 5, 20, -20, 120,
                         -20, -40, -5, -5, -5, -5, -40, -20, 
                         20, -5, 15, 3, 3, 15, -5, 20, 
                         5, -5, 3, 3, 3, 3, -5, 5, 
                         5, -5, 3, 3, 3, 3, -5, 5, 
                         20, -5, 15, 3, 3, 15, -5, 20,
                         -20, -40, -5, -5, -5, -5, -40, -20,
                         120, -20, 20, 5, 5, 20, -20, 120
                     };
    }

    void Start () {
        originalBoard = new Board();
        GameObject ob = new GameObject();
        ob.name = "Board";

        slots = new List<List<Slot>>();
        for (int i = 0; i < 8; i++)
        {
            List<Slot> slotLine = new List<Slot>();
            for (int j = 0; j < 8; j++)
            {
                GameObject obj = Instantiate(slotPrefab, new Vector3(j, 0, i), Quaternion.identity) as GameObject;
                obj.transform.parent = ob.transform;
                Slot slot = obj.GetComponent<Slot>();
                slotLine.Add(slot);
                slot.value = values1[8 * i + j];
                slot.board = originalBoard;
            }
            slots.Add(slotLine);
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i < 7)
                {
                    slots[i][j].up = slots[i + 1][j];
                }
                if (i > 0)
                {
                    slots[i][j].down = slots[i - 1][j];
                }
                if (j > 0)
                {
                    slots[i][j].left = slots[i][j - 1];
                }
                if (j < 7)
                {
                    slots[i][j].right = slots[i][j + 1];
                }
                if (j < 7 && i < 7 && slots[i + 1] != null && slots[i + 1][j + 1] != null)
                {
                    slots[i][j].upRight = slots[i + 1][j + 1];
                }
                if (i < 7 && j > 0 && slots[i + 1] != null && slots[i + 1][j - 1] != null)
                {
                    slots[i][j].upLeft = slots[i + 1][j - 1];
                }
                if ( i > 0 && j < 7 && slots[i - 1] != null && slots[i - 1][j + 1] != null)
                {
                    slots[i][j].downRight = slots[i - 1][j + 1];
                }
                if ( i > 0 && j > 0 && slots[i - 1] != null && slots[i - 1][j - 1] != null)
                {
                    slots[i][j].downLeft = slots[i - 1][j - 1];
                }
            }
        }

        slots[3][4].state = Slot.SlotState.black;
        slots[4][3].state = Slot.SlotState.black;
        slots[4][4].state = Slot.SlotState.white;
        slots[3][3].state = Slot.SlotState.white;

        originalBoard.slots = slots;
        copy = originalBoard.Clone();
    }

    public Board GetCopy()
    {
        return originalBoard.Clone();
    }

    public List<List<Slot>> GetTable()
    {
        List<List<Slot>> listCopy = new List<List<Slot>>();
        foreach (List<Slot> ls in slots)
        {
            listCopy.Add(ls.ToList());
        }
        return listCopy;
    }

 
}
