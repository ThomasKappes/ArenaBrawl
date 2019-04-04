using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class characterController : MonoBehaviour
{
    Animator m_Animator;
    bool walking = false;
    bool running = false;
    public float walkspeed;
    public float runspeed;
    public Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = updateBooleans();
        m_Animator.SetBool("isWalking", walking);
        m_Animator.SetBool("isRunning", running);
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        Vector3 oldworldpos = transform.position;
        float mouseX = mousePosition.x;
        float mouseY = mousePosition.y;
        float screenX = screenPos.x;
        float screenY = screenPos.y;
        float distX = mouseX - screenX;
        float distY = mouseY - screenY;

        float bog = (float) Math.Atan((mouseY-screenY)/(mouseX-screenX));
        float degree = ((bog * 180) / (float) Math.PI);
        float toRotate = (90 - degree);

        transform.rotation = Quaternion.AngleAxis((mouseX-screenX) >= 0 ? toRotate : 180 + toRotate, Vector3.up);
        
        if (Math.Abs(mousePosition.x - screenPos.x) <= 10 && Math.Abs(mousePosition.y - screenPos.y) <= 10)
        {
            return;
        }

        if (isWalking() && isRunning())
        {
            transform.Translate(moveVector * runspeed * Time.deltaTime);
        } else if (isWalking()) {
            transform.Translate(moveVector * walkspeed * Time.deltaTime);
        }
        Vector3 camRelocatorVec = transform.position - oldworldpos;
        screenPos = cam.WorldToScreenPoint(transform.position);
        screenX = screenPos.x;
        screenY = screenPos.y;
        if(screenX > 400 && screenX < (Screen.width - 400))
        {
            camRelocatorVec =  new Vector3 (0,0,camRelocatorVec.z);
        }
        if(screenY > 150 && screenY < (Screen.height - 500))
        {
            camRelocatorVec = new Vector3 (camRelocatorVec.x,0,0);
        }
        fixCam(camRelocatorVec);
        

        
    }

    void fixCam(Vector3 moveVec)
    {
        cam.transform.Translate((new Vector3(moveVec.x, 0, moveVec.z)), Space.World);
    }

    Vector3 updateBooleans()
    {
        walking = false;
        running = false;
        Vector3 moveVec = new Vector3(0, 0, 0);
        
        if (Input.GetKey(KeyCode.W))
        {
            moveVec = moveVec + Vector3.forward;
            walking = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVec = moveVec + Vector3.left;
            walking = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVec = moveVec + Vector3.back;
            walking = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVec = moveVec + Vector3.right;
            walking = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            running = true;
        }
        return moveVec;
         
    }

    bool isWalking(){
        return walking;
    }

    bool isRunning(){
        return running;
    }
}