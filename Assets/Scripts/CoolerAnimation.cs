using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles state machine of cooler being slide out or back for players taking fish
*/
public class CoolerAnimation : MonoBehaviour
{

    Animator animator;
    bool isOut;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    //Used with OnClick event on the cooler button
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
