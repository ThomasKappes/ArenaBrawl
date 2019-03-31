using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class characterController : MonoBehaviour
{
    Animator m_Animator;
    bool walking = false;
    bool running = false;
    public float turnSpeed;
    public Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        updateBooleans();
        m_Animator.SetBool("isWalking", walking);
        m_Animator.SetBool("isRunning", running);
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        float bog = (float) Math.Atan((mousePosition.y-screenPos.y)/(mousePosition.x-screenPos.x));
        float degree = ((bog * 180) / (float) Math.PI);
        float toRotate = (90 - degree);

        transform.rotation = Quaternion.AngleAxis((mousePosition.x-screenPos.x) >= 0 ? toRotate : 180 + toRotate, Vector3.up);


    }

    void updateBooleans()
    {
            walking = Input.GetKey(KeyCode.W) == true ? true : false;
            running = Input.GetKey(KeyCode.LeftShift) == true ? true : false;
    }
}