using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandTool : MonoBehaviour
{
    //
    [SerializeField] Sprite sideOne;
    [SerializeField] Sprite otherSide;
    SpriteRenderer spriteRenderer;
   
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(spriteRenderer.sprite == otherSide)
                spriteRenderer.sprite = sideOne;
            else
                spriteRenderer.sprite = otherSide;
            gameObject.transform.Rotate(180,0,0);
        }
    }
}

