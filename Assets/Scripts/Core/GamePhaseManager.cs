using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script to send invoke events when different phases of the gameplay loop end
* so other scripts can respond accordingly.
*
* Main Menu -> PurchasePhase -> CleaningPhase -> SellingPhase -> Repeat
*/
public class GamePhaseManager : MonoBehaviour
{
    public static Action PurchasePhaseEnded = delegate { };
    public static Action CleaningPhaseEnded = delegate { };
    public static Action SellingPhaseEnded = delegate { };

    public void EndPurchasePhase()
    {
        PurchasePhaseEnded.Invoke();
    }
    public void EndCleaningPhase()
    {
        CleaningPhaseEnded.Invoke();
    }
    public void EndSellingPhase()
    {
        SellingPhaseEnded.Invoke();
    }
}
