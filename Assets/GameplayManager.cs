using UnityEngine;
using System.Collections;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    public int currentPlayer = 1;
    public int depth = 1;
    public bool useIa = false;
    public Move nextAiMove;
    public GameObject trash;
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

            if (Physics.Raycast(ray, out hit, 100000) && currentPlayer > 0)
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Slot"))
                {
                    Slot slot = hit.transform.gameObject.GetComponent<Slot>();
                    if (slot.CanPlay(GetPlayerState(currentPlayer)))
                    {
                        slot.Play(currentPlayer);
                        if(Game.instance.originalBoard.CanPlay(-currentPlayer))
                            currentPlayer = -currentPlayer;
                        if (currentPlayer < 0)
                            if(useIa)
                                Invoke("JogaDiabo",2.0f);
                    }

                }

            }
        }
        
    }
    void JogaDiabo()
    {
        Minimax.instance.MiniMax( Game.instance.originalBoard, depth, -1,true);

        Invoke("ChangePlayer",0.5f);
    }
    void ChangePlayer()
    {
        if (Game.instance.originalBoard.CanPlay(-currentPlayer))
            currentPlayer = -currentPlayer;
        else
            Invoke("JogaDiabo", 2.0f);

    }
    public Slot.SlotState GetCurrentPlayerState()
    {
        Slot.SlotState playerState;
        if (currentPlayer > 0)
            playerState = Slot.SlotState.white;
        else
            playerState = Slot.SlotState.black;
        return playerState;

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

}
