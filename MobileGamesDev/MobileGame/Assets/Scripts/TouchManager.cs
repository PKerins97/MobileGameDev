using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;


//Need to move selected object function into I_Interactable & need to move other parts to gestureScript and I_Interactable
public class TouchManager : MonoBehaviour
{
    private float timer;
    private bool hasMoved;
    private float MaxTapTime = 0.2f;
    GestureManagerScript actOn;
    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    private Transform objectToDrag;

    // Start is called before the first frame update
    void Start()
    {
        actOn = FindObjectOfType<GestureManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch = Input.touches[0];
        Vector3 posTouched = touch.position;
        Ray ray = Camera.main.ScreenPointToRay(posTouched);
        RaycastHit hit;
        Vector3 dragVec;
        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }
     
        foreach (Touch t in Input.touches)
            {
                switch (t.phase)
                {
                    case TouchPhase.Began:
                        timer = 0f;
                        hasMoved = false;
                        print(Input.touchCount);
                       
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.GetComponent(typeof(I_Interactable)))
                            {
                            objectToDrag = hit.transform;
                            dist = Vector3.Distance( hit.transform.position,  Camera.main.transform.position);
                            dragging = true;
                            }
                        }
                    break;
                    case TouchPhase.Moved:
                        hasMoved = true;
                        print("moved");
                        if (dragging && touch.phase == TouchPhase.Moved)
                        {
                        dragVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                        dragVec = Camera.main.ScreenToWorldPoint(dragVec);
                        objectToDrag.position = ray.GetPoint(dist);
                        }
                    break;
                    case TouchPhase.Stationary:
                        timer += Time.deltaTime;
                        print("Stationary");
                        break;
                    case TouchPhase.Ended:
                        print("ended");
                        if ((timer < MaxTapTime) && !hasMoved)
                        {
                            actOn.tapAt(t.position);
                            print("Tap");
                        }
                        if (dragging && (touch.phase == TouchPhase.Ended))
                        {
                            dragging = false;
                        }
                    break;
                }

            }
        //GetComponent<I_Interactable>().processDrag();
        //GetComponent<I_Interactable>().processTap();
        GetComponent<I_Interactable>().deselectedObject();
        GetComponent<I_Interactable>().selectedObejct();

    }
}