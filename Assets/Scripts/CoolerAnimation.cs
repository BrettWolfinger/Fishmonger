using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolerAnimation : MonoBehaviour
{

    Animator animator;

    bool isOut;
    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void Slide()
    {
        if(isOut)
        {
            animator.ResetTrigger("SlideOut");
            animator.SetTrigger("SlideBack");
            isOut = false;
        }
        else
        {
            animator.ResetTrigger("SlideBack");
            animator.SetTrigger("SlideOut");
            isOut = true;
        }
    }
}
