using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject camera1;
    public GameObject camera2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeCam();
        }
    }
    void ChangeCam()
    {
        if (camera1.activeSelf)
        {
            camera1.SetActive(false);
            camera2.SetActive(true);
        }
        else if (camera2.activeSelf)
        {
            camera1.SetActive(true);
            camera2.SetActive(false);
        }
    }
}
