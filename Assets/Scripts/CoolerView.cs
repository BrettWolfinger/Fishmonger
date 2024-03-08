using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolerView : MonoBehaviour
{
    
    [SerializeField] Button button;
    Button newButton;
    Animator animator;
    bool isOut;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        //Add buttons to the cooler UI for each fish held in it
        foreach(FishSO fish in dict.Keys)
        {
            newButton = Instantiate(button,gameObject.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = fish.name + " x " + dict[fish];
            newButton.onClick.AddListener(delegate {SpawnFish(fish,newButton); });
            newButton.onClick.AddListener(delegate {coolerAnimation.Slide();});
        }
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
