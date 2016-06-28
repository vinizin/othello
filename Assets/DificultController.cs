using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DificultController : MonoBehaviour {

    public Text dificult;
    public Slider slider;


    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {

    }
    public void OnChangeDificulty(float value)
    {
        int teste = Mathf.CeilToInt( slider.value );
        if (teste <= 1)
            dificult.text = "Fácil";
        else if (teste <= 2)
            dificult.text = "Médio";
        else if (teste <= 3)
            dificult.text = "Difícil";

        GameplayManager.instance.depth = 1;
    }
}
