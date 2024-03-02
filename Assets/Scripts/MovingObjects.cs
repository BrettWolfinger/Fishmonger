using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Set Mesh to be the mesh of item and Rigidbody to ignore gravity and be kinematic
//disable colliders on filets, they become enabled after they are removed from body
[RequireComponent(typeof(MeshCollider),typeof(Rigidbody))]
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

    //Check for special locations on releasing a held item
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
            //Only sell item if it is a filet
            if(hit.transform.name == "Sale" && gameObject.GetComponent<FiletManager>() != null)
            {
                gameObject.GetComponent<FiletManager>().Sell();
                ps.Play();
                Destroy(gameObject);
            }
        }
    }

}
