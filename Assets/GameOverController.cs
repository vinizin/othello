using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour {

    public Text textWin;
    public GameObject playAgain;
    public Vector2 score;
	// Update is called once per frame
    void Start()
    {
        playAgain.SetActive(false);
    }
	void Update () {

        if (Game.instance.originalBoard.IsGameOver())
        {
            playAgain.SetActive(true);
            score = Game.instance.originalBoard.GetScore();

            if (score.x > score.y)
                textWin.text = "PLAYER 1 WINS";
            else if (score.x < score.y)
                textWin.text = "COMPUTER 1 WINS";
            else
                textWin.text = "DRAW";


        }
    }

   public  void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
