using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenuElement : MonoBehaviour
{
   // Gives access to the application and all instances.
   public BuyMenu buyMenu { get { return GetComponent<BuyMenu>(); }}
}

public class BuyMenu : MonoBehaviour
{

   // Reference to the root instances of the MVC.
   public BuyMenuModel model;
   public BuyMenuView view;
   public BuyMenuUIController controller;

   // Init things here
   void Start() { }
}

