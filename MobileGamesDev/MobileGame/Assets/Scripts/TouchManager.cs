using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;


public class TouchManager : MonoBehaviour
{
    private float timer;
    private bool hasMoved;
    private float MaxTapTime = 0.2f;
    GestureManagerScript actOn;
    private float dist;
    private bool dragging = false;
    private bool rotating = false;
    private Vector2 dragStartPosition;
    private Transform objectToDrag;
    [SerializeField]
    
    

    // Start is called before the first frame update
    void Start()
    {
        actOn = FindFirstObjectByType<GestureManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        print(Input.touchCount);
        if (Input.touchCount == 0)
        {
            return;
        }
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
                            dist = Vector3.Distance(hit.transform.position, Camera.main.transform.position);
                            dragging = true;
                        }

                    }
                    break;
                case TouchPhase.Moved:
                    hasMoved = true;
                    print("moved");
                    
                            if (Input.touchCount == 2)
                            {
                                // Get the positions of both touches
                                Touch touch1 = Input.GetTouch(0);
                                Touch touch2 = Input.GetTouch(1);

                                // Check if the first touch is stationary
                                if (touch1.phase == TouchPhase.Stationary && touch2.phase == TouchPhase.Moved)
                                {
                                    // Calculate the position change of the second touch relative to the first touch
                                    Vector2 deltaTouchPosition = touch2.position - touch2.deltaPosition - touch1.position;

                                    // Rotate the object based on the change in position
                                    actOn.rotateAt(deltaTouchPosition);
                                }

                            }

                            if (!rotating && !dragging && Vector2.Distance(t.position, dragStartPosition) > 1)
                            {
                                rotating = true;
                            }

                            if (dragging && touch.phase == TouchPhase.Moved)
                            {
                                dragVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                                dragVec = Camera.main.ScreenToWorldPoint(dragVec);
                                actOn.dragAt(t.position);
                            }
                            actOn.scaleAt(t.position);
                        
                    
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

    }
}