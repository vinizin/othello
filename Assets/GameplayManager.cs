using UnityEngine;
using System.Collections;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    public int player = 1;

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100000))
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Slot"))
                {
                    Slot slot = hit.transform.gameObject.GetComponent<Slot>();
                    if (slot.CanPlay)
                    {
                        Debug.Log("JOGUEI?");
                        slot.Play(player);
                        player = -player;
                    }

                }

            }
        }
        
    }

    public Slot.SlotState GetPlayerState()
    {
        Slot.SlotState playerState;
        if (player > 0)
            playerState = Slot.SlotState.white;
        else
            playerState = Slot.SlotState.black;
        return playerState;

    }


}
