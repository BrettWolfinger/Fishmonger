using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FiletManager : FiletElement
{
    [SerializeField] UnityEngine.UI.Slider descaleProgressBarPrefab;

    int numOfPinbones;
    bool isDescaling;
    float descaleTime;
    float timeToDescale = 2f;
    Vector3 mousePosition;
    Canvas canvas;
    UnityEngine.UI.Slider currentProgressBar;
    Vector3 offset;

        
    public static Action<FiletModel.FiletStruct> FiletSold = delegate { };

    void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        offset = new Vector3(0,2,0);
        //pinbones = GetComponentsInChildren
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 1.0f;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if(Input.GetMouseButton(1) && isDescaling)
        {
            descaleTime += Time.deltaTime;
            if(currentProgressBar != null)
                currentProgressBar.value = descaleTime / timeToDescale;
                currentProgressBar.transform.position = mousePosition + offset;
        }
        if(Input.GetMouseButtonUp(1) && isDescaling)
        {
            isDescaling = false;
            print("stopped");
            if(currentProgressBar != null)
                Destroy(currentProgressBar.gameObject);
        }
        if(descaleTime > timeToDescale && !filet.model.filetStruct.isDescaled)
        {
            FinishedDescaling();
        }
    }

    private void FinishedDescaling()
    {
        filet.model.filetStruct.isDescaled = true;
        Destroy(GetComponentInChildren<DescaleMiniGame>().transform.parent.gameObject);
        Destroy(currentProgressBar.gameObject);
        currentProgressBar = null;
    }

    public void RemovedPinBone()
    {
        numOfPinbones--;
        if(numOfPinbones == 0) filet.model.filetStruct.isDeboned = true;
    }

    public void Activate()
    {
        DeboneMiniGame[] pinbones = GetComponentsInChildren<DeboneMiniGame>();
        foreach(DeboneMiniGame pinbone in pinbones)
        {
            pinbone.GetComponent<CircleCollider2D>().enabled = true;
        }
        numOfPinbones = GetComponentsInChildren<DeboneMiniGame>().Length;
    }

    public void Sell()
    {
        filet.model.filetStruct.CalculateSalePrice(filet.model.fish);
        FiletSold.Invoke(filet.model.filetStruct);
    }

    public void Descaling()
    {
        currentProgressBar = Instantiate(descaleProgressBarPrefab,mousePosition + offset, descaleProgressBarPrefab.transform.rotation, canvas.transform);
        isDescaling = true;
    }
}
