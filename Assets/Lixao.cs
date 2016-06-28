using UnityEngine;
using System.Collections;

public class Lixao : MonoBehaviour {
    public static Lixao instance;
	// Use this for initialization
	void Awake () {
        instance = this;

    }
	
	// Update is called once per frame
	public void Clean () {
        while (transform.GetChildCount() > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
	}
}
