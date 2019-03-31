using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("up")) {
            m_Animator.SetBool("isWalking", true);
        } else {
            m_Animator.SetBool("isWalking", false);
        }
    }
}
