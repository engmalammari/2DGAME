using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    private Animator animation;
 
    void Start()
    {
        animation = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animation.SetBool("walk", true);
        }
        else animation.SetBool("walk", false);

        if (Input.GetKey(KeyCode.W))
        {
            animation.SetTrigger("jump");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            animation.SetTrigger("attack");
        }
    }
}
