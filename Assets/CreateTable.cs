using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateTable : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<List<Slot>> slots;

    void Start () {
        slots = new List<List<Slot>>();
        for (int i = 0; i < 8; i++)
        {
            List<Slot> slotLine = new List<Slot>();
            for (int j = 0; j < 8; j++)
            {
                GameObject obj = Instantiate(slotPrefab, new Vector3(j, 0, i), Quaternion.identity) as GameObject;
                Slot slot = obj.GetComponent<Slot>();
                slotLine.Add(slot);
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

    }

    // Update is called once per frame
    void Update () {
	
	}
}
