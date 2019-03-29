using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimationDemo : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        if(GetComponent<Animator>() != null)
        {
            animator = GetComponent<Animator>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.CrossFade("walk", 0.1f);
            
        }

           
        if (Input.GetKeyUp(KeyCode.W))
        {
            
            animator.CrossFade("Default Take", 0.5f);
        }
    }
}
