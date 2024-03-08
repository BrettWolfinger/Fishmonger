using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 // Base class for all elements in this application.
public class CoolerElement : MonoBehaviour
{
   // Gives access to the application and all instances.
   public Cooler cooler { get { return GameObject.FindObjectOfType<Cooler>(); }}
}

public class Cooler : MonoBehaviour
{

   // Reference to the root instances of the MVC.
   public CoolerModel model;
   public CoolerView view;
   public CoolerController controller;

   // Init things here
   void Start() { }
}
