using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FiletManager : MonoBehaviour
{
    //type of fish this filet belongs to
    [SerializeField] FishSO fish;
    [SerializeField] UnityEngine.UI.Slider descaleProgressBarPrefab;

    //booleans for different things players can chose to do/not do that
    //will affect the fish sale price
    bool isDescaled = false;
    bool isDeboned = false;
    bool isSkinned = false;
    int numOfPinbones;
    bool isDescaling;
    float descaleTime;
    float timeToDescale = 2f;
    Vector3 mousePosition;
    Canvas canvas;
    UnityEngine.UI.Slider currentProgressBar;
    Vector3 offset;

    SaleManager saleManager;

    void Awake()
    {
        saleManager = FindObjectOfType<SaleManager>();
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
        if(descaleTime > timeToDescale && !isDescaled)
        {
            FinishedDescaling();
        }
    }

    private void FinishedDescaling()
    {
        isDescaled = true;
        Destroy(GetComponentInChildren<DescaleMiniGame>().transform.parent.gameObject);
        Destroy(currentProgressBar.gameObject);
        currentProgressBar = null;
    }

    public void RemovedPinBone()
    {
        numOfPinbones--;
        if(numOfPinbones == 0) isDeboned = true;
        print(isDeboned);
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
        int salePrice = fish.GetSalePrice();
        if(!isDescaled) salePrice -= fish.salePriceDecreaseNotDescaled;
        if(!isDeboned) salePrice -= fish.salePriceDecreaseNotDeboned;
        if(!isSkinned) salePrice -= fish.salePriceDecreaseNotSkinned;

        Filet filet = new Filet(fish.name,isDescaled,isDeboned,isSkinned,salePrice);
        saleManager.Record(filet);
    }

    public void Descaling()
    {
        currentProgressBar = Instantiate(descaleProgressBarPrefab,mousePosition + offset, descaleProgressBarPrefab.transform.rotation, canvas.transform);
        isDescaling = true;
    }

    [System.Serializable]
    public class Filet
    {
        public string name;
        public bool isDescaled;
        public bool isDeboned;
        public bool isSkinned;
        public int salePrice;

        public Filet(string name,bool isDescaled,bool isDeboned,bool isSkinned,int salePrice)
        {
            this.name = name;
            this.isDescaled = isDescaled;
            this.isDeboned = isDeboned;
            this.isSkinned = isSkinned;
            this.salePrice = salePrice;
        }

        public override string ToString()
        {
            return "Filet of " + name + " Sale Price: " + salePrice.ToString();
        }
    }
}
