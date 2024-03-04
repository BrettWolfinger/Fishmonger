using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiletManager : MonoBehaviour
{
    //type of fish this filet belongs to
    [SerializeField] FishSO fish;

    //booleans for different things players can chose to do/not do that
    //will affect the fish sale price
    bool isDescaled = false;
    bool isDeboned = false;
    bool isSkinned = false;
    int numOfPinbones;
    bool isDescaling;
    float descaleTime;

    SaleManager saleManager;

    void Awake()
    {
        saleManager = FindObjectOfType<SaleManager>();
        //pinbones = GetComponentsInChildren
    }

    void Update()
    {
        if(Input.GetMouseButton(1) && isDescaling)
        {
            descaleTime += Time.deltaTime;
            print(isDescaled);
        }
        if(Input.GetMouseButtonUp(1) && isDescaling)
        {
            isDescaling = false;
        }
        if(descaleTime > 2f && !isDescaled)
        {
            FinishedDescaling();
        }
    }

    private void FinishedDescaling()
    {
        isDescaled = true;
        Destroy(GetComponentInChildren<DescaleMiniGame>().transform.parent.gameObject);
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
