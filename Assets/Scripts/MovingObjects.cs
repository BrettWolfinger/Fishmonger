using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{
//
    [SerializeField] ParticleSystem ps;
    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    GetComponent<Rigidbody>().MovePosition(curPosition);

    }

    private void OnMouseUp() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, 100);
        foreach(RaycastHit hit in hits)
        {
            if(hit.transform.name == "Trash")
            {
                Destroy(gameObject);
            }
            if(hit.transform.name == "Sale")
            {
                ps.Play();
                Destroy(gameObject);
            }
        }
    }

}
