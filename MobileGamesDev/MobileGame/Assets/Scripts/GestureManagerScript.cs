using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManagerScript : MonoBehaviour
{

    internal void tapAt(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit info;
        if (Physics.Raycast(ray, out info))
        {
            I_Interactable objectHit = info.collider.GetComponent<I_Interactable>();
            if (objectHit != null)
            {
                objectHit.processTap();
            }

        }
    }

    internal void draggedAt(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit info;
        if (Physics.Raycast(ray, out info))
        {
            I_Interactable objectHit = info.collider.GetComponent<I_Interactable>();
            if (objectHit != null)
            {
                objectHit.processDrag();
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}