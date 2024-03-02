using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    //
    [SerializeField] Button handButton;
    [SerializeField] Button chopperButton;
    //[SerializeField] GameObject filet;
    [SerializeField] GameObject knife;
    PlayerInput playerInput;
    InputActionMap currentToolMap;
    GameObject currentToolPrefab;
    private Vector3 mousePosition;
    bool isFileting = false;
    void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentToolMap = playerInput.actions.FindActionMap("HandTool");
        currentToolMap.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 1.0f;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if(currentToolPrefab != null)
        {
            currentToolPrefab.transform.position = mousePosition;
        }

        if (Input.GetMouseButtonUp(1) && isFileting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.transform.tag == "MiniGameEnd")
            {
                Debug.Log("MiniGameComplete!");
                Filet(hit.transform.parent.gameObject);
                isFileting = false;
            }
        }
    }

    void OnRotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.Log(hit.transform.name);
            Debug.Log("hit");
        }
        //make sure it is an object we want to be moving
        if(hit.transform.gameObject.GetComponent<MovingObjects>() != null)
            hit.transform.Rotate(180,0,0);
    }

    void OnEquipHand()
    {
        currentToolMap.Disable();
        currentToolMap = playerInput.actions.FindActionMap("HandTool");
        currentToolMap.Enable();
        Destroy(currentToolPrefab);
        handButton.Select();
    }
    void OnEquipChopper()
    {
        currentToolMap.Disable();
        currentToolMap = playerInput.actions.FindActionMap("Chopper");
        currentToolPrefab = knife;
        currentToolPrefab = Instantiate(knife,mousePosition,knife.transform.rotation);
        currentToolMap.Enable();
        chopperButton.Select();
    }

    void OnChop()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null && hit.transform.tag == "MiniGameStart")
        {
            print("minigame started");
            isFileting = true;
        }
    }

    void Filet(GameObject filet)
    {
        GameObject newFilet = Instantiate(filet,filet.transform.position,filet.transform.rotation);
        newFilet.transform.localScale = new Vector3(3,3,3);
        newFilet.GetComponent<MeshCollider>().enabled = true;
        Destroy(filet);
    }

}
