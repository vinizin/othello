using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    public Text textScorePlayer;
    public Text textScoreComputador;

    void Update()
    {
        textScorePlayer.text = string.Format("PLAYER: {0}", Game.instance.originalBoard.GetScore().x);
        textScoreComputador.text = string.Format("COMPUTER: {0}", Game.instance.originalBoard.GetScore().y);
    }
}
