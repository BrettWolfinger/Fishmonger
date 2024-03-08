using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keeps a ledger of the purchases made during the buy phase to be routed
 to other areas. May eventually want to add functionality for recording all time purchases
*/
public class EconomicsManager : MonoBehaviour
{
    Dictionary<string,int> purchaseLedger = new Dictionary<string, int>();
    SupplyManager supplyManager;
 
    void Awake()
    {
        supplyManager = gameObject.GetComponent<SupplyManager>();
    }

    void OnEnable()
    {
        BuyMenuUI.FishWasPurchased += RecordPurchase;
        GamePhaseManager.PurchasePhaseEnded += PurchasePhaseEnd;
    }

    void OnDisable()
    {
        BuyMenuUI.FishWasPurchased -= RecordPurchase;
        GamePhaseManager.PurchasePhaseEnded -= PurchasePhaseEnd;
    }

    void RecordPurchase(FishSO fish, int quantity, int price)
    {
        if(!purchaseLedger.TryAdd(fish.name, quantity))
        {
            purchaseLedger[fish.name] += quantity;
        }
    }

    void PurchasePhaseEnd()
    {
        supplyManager.purchaseLedger = purchaseLedger;
        supplyManager.ProcessPurchaseLedger();
    }
}
