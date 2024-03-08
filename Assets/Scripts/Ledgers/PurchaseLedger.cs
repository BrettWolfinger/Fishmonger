 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Keeps a ledger of the purchases made during the buy phase to be routed
 to other areas. May eventually want to add functionality for recording all time purchases
*/
public class PurchaseLedger : MonoBehaviour
{
    public Dictionary<string,int> purchaseLedger {get; private set;}
    public PurchaseLedgerSaver purchaseLedgerSaver;

    void Awake()
    {
        purchaseLedger = new Dictionary<string, int>();
        Scene currentScene = SceneManager.GetActiveScene();
        if( currentScene.name == "GameplayScreen" || currentScene.name == "PostGameplay" )
        {
            purchaseLedger = purchaseLedgerSaver.Load();
        }
    }

    void OnEnable()
    {
        BuyMenuUIController.FishWasPurchased += RecordPurchase;
        GamePhaseManager.PurchasePhaseEnded += PurchasePhaseEnd;
    }

    void OnDisable()
    {
        BuyMenuUIController.FishWasPurchased -= RecordPurchase;
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
        purchaseLedgerSaver.Save(purchaseLedger);
    }
}
