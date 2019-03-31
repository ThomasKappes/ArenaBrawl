using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    Animator m_Animator;
    bool walking = false;
    bool running = false;
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
    }

    void updateBooleans()
    {
            walking = Input.GetKey(KeyCode.W) == true ? true : false;
            running = Input.GetKey(KeyCode.LeftShift) == true ? true : false;
    }
}