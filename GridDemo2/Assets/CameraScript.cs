using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    Camera cam;
    characterController controller;
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent(typeof(Camera)) as Camera;
        controller = (characterController) player.GetComponent(typeof(characterController));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(player.transform.position);
        fixCam(controller.runspeed, screenPos.x);
    }

    void fixCam(float speed, float screenX)
    {
        if(Math.Abs(screenX - (Screen.width/2) ) < 400)
        {
            return;
        }
        transform.Translate(Vector3.right * (speed + 2) * Time.deltaTime * Math.Sign(screenX - (Screen.width/2)));
    }
}
