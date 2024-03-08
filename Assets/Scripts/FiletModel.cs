using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FiletModel : FiletElement
{
    //type of fish this filet belongs to
    [SerializeField] public FishSO fish;
    public FiletStruct filetStruct;

    private void Awake() 
    {
        filetStruct = new FiletStruct
        {
            name = fish.name
        };
    }
   
   [System.Serializable]
    public class FiletStruct
    {
        public string name;

        //booleans for different things players can chose to do/not do that
        //will affect the fish sale price
        public bool isDescaled;
        public bool isDeboned;
        public bool isSkinned;
        public int salePrice;

        public void CalculateSalePrice(FishSO fish)
        {
            salePrice = fish.salePricePerFilet;
            if(!isDescaled) salePrice -= fish.salePriceDecreaseNotDescaled;
            if(!isDeboned) salePrice -= fish.salePriceDecreaseNotDeboned;
            if(!isSkinned) salePrice -= fish.salePriceDecreaseNotSkinned;
        }

        public override string ToString()
        {
            return "Filet of " + name + " Sale Price: " + salePrice.ToString();
        }
    }
}
