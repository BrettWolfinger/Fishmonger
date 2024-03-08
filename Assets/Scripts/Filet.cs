using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiletElement : MonoBehaviour
{
   // Gives access to the application and all instances.
   public Filet filet { get { return GetComponent<Filet>(); }}
}

public class Filet : MonoBehaviour
{

   // Reference to the root instances of the MVC.
   public FiletModel model;
   //public BuyMenuView view;
   public FiletManager controller;

   // Init things here
   void Start() { }
}
