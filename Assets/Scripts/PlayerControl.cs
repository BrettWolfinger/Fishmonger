using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    //
    [SerializeField] Button handButton;
    [SerializeField] Button knifeButton;
    [SerializeField] Button debonerButton;
    [SerializeField] Button descalerButton;
    //[SerializeField] Button chopperButton;
    //[SerializeField] GameObject filet;
    [SerializeField] GameObject knife;
    [SerializeField] GameObject deboner;
    [SerializeField] GameObject descaler;
    [SerializeField] GameObject chopper;
    [SerializeField] TextMeshProUGUI filetPopup;

    PlayerInput playerInput;
    Canvas canvas;
    InputActionMap currentToolMap;
    GameObject currentToolPrefab;
    Button currentButton;
    ColorBlock colors;
    private Vector3 mousePosition;
    bool isFileting = false;
    TextMeshProUGUI currentFiletPopup;
    void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentToolMap = playerInput.actions.FindActionMap("HandTool");
        currentToolMap.Enable();
        currentButton = handButton;
        currentButton.Select();
        canvas = FindObjectOfType<Canvas>();
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
                currentFiletPopup = Instantiate(filetPopup,mousePosition,filetPopup.transform.rotation,canvas.transform);
                Destroy(currentFiletPopup, .5f);
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
    {EquipTool();}
    void OnEquipChopper()
    {
        //EquipTool();
    }
    void OnEquipFilet()
    {EquipTool();}
    void OnEquipDeboner()
    {EquipTool();}
    void OnEquipDescaler()
    {EquipTool();}

    void EquipTool([CallerMemberName] string caller = null)
    {
        Destroy(currentToolPrefab);
        currentToolMap.Disable();
        switch (caller)
        {
            case "OnEquipHand":
                currentToolMap = playerInput.actions.FindActionMap("HandTool");
                currentToolPrefab = null;
                currentButton = handButton;
                break;
            case "OnEquipChopper":
                currentToolMap = playerInput.actions.FindActionMap("Chopper");
                currentToolPrefab = chopper;
                //currentButton = chopperButton;
                break;
            case "OnEquipFilet":
                currentToolMap = playerInput.actions.FindActionMap("FiletKnife");
                currentToolPrefab = knife;
                currentButton = knifeButton;
                break;
            case "OnEquipDeboner":
                currentToolMap = playerInput.actions.FindActionMap("Deboner");
                currentToolPrefab = deboner;
                currentButton = debonerButton;
                break;
            case "OnEquipDescaler":
                currentToolMap = playerInput.actions.FindActionMap("Descaler");
                currentToolPrefab = descaler;
                currentButton = descalerButton;
                break;
        }
        if(currentToolPrefab != null)
            currentToolPrefab = Instantiate(currentToolPrefab,mousePosition,currentToolPrefab.transform.rotation);
        currentToolMap.Enable();
        currentButton.Select();
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

    void OnFilet()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null && hit.transform.tag == "MiniGameStart")
        {
            print("minigame started");
            isFileting = true;
            currentFiletPopup = Instantiate(filetPopup,mousePosition,filetPopup.transform.rotation,canvas.transform);
            Destroy(currentFiletPopup, .5f);
        }
    }

    void OnDebone() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null && hit.transform.tag == "DeboneMiniGame")
        {
            print("pinbonehit");
            hit.transform.GetComponentInParent<FiletManager>().RemovedPinBone();
            Destroy(hit.transform.parent.gameObject);
        }
    }

    void OnDescale()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if (hit.collider != null && hit.transform.tag == "DescaleMiniGame")
        {
            hit.transform.GetComponentInParent<FiletManager>().Descaling();
        }
    }


    void Filet(GameObject filet)
    {
        Vector3 offset = new Vector3(.1f,-.1f,0);
        GameObject newFilet = Instantiate(filet,filet.transform.position + offset,filet.transform.rotation);
        newFilet.transform.localScale = new Vector3(2,2,2);
        newFilet.GetComponent<MeshCollider>().enabled = true;
        newFilet.GetComponent<FiletManager>().Activate();
        Destroy(filet);
    }

}
