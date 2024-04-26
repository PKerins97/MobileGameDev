using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestureManagerScript : MonoBehaviour
{
    [SerializeField]
    Material m_Material;
    float zoomOutMin = 1;
    float zoomOutMax = 80;
    Vector3 touchStart;
    I_Interactable selectedObject;


    internal void tapAt(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider);
            print(hit.collider.gameObject.name);

            I_Interactable objectHit = hit.collider.GetComponent<I_Interactable>();
            if (objectHit != null)
            {
                if (selectedObject != null)
                {
                    selectedObject.deselectedObject();
                    selectedObject = null;
                }
                else
                {
                    selectedObject = objectHit;
                    objectHit.processTap();
                    objectHit.processRotate();
                    objectHit.processScale();
                }

            }
            else
            {
                if (selectedObject != null)
                {
                    selectedObject.deselectedObject();
                    selectedObject = null;
                }
            }

        }
        else
        {
            if (selectedObject != null)
            {
                selectedObject.deselectedObject();
                selectedObject = null;
            }
        }


    }

    internal void dragAt(Vector2 draggedPos)
    {

        if (selectedObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(draggedPos);

            selectedObject.processDrag(ray);
        }

    }

    internal void rotateAt(Vector2 rotatePos)
    {
        if (selectedObject != null)
        {
           selectedObject.processRotate();
        }
    }

    internal void scaleAt(Vector2 scale)
    {
        if(selectedObject != null)
        {
            selectedObject.processScale();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch1 = Input.GetTouch(0);
           
                if (Input.touchCount == 1)
                {
                
                    // Handle single touch movement
                    touchStart = Camera.main.ScreenToWorldPoint(touch1.position);
                    Vector3 distance = touchStart - Camera.main.ScreenToWorldPoint(touch1.position);
                    Camera.main.transform.position += distance;
                    print("panning");


                }
                else if (Input.touchCount == 2)
                {
                    // Handle two-finger zoom
                    Touch touch2 = Input.GetTouch(1);

                    Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
                    Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

                    float prevMagnitude = (touch1PrevPos - touch2PrevPos).magnitude;
                    float currentMagnitude = (touch1.position - touch2.position).magnitude;

                    float difference = currentMagnitude - prevMagnitude;
                    print("zoom in");
                    zoom(difference * 0.2f);
                }

                else
                {
                    Touch touch2 = Input.GetTouch(1);
                    Vector2 touch1PrevPos = touch1.position + touch1.deltaPosition;
                    Vector2 touch2PrevPos = touch2.position + touch2.deltaPosition;

                    float prevMagnitude = (touch1PrevPos + touch2PrevPos).magnitude;
                    float currentMagnitude = (touch1.position + touch2.position).magnitude;

                    float difference = currentMagnitude + prevMagnitude;
                    print("zoom out");
                    zoom(difference * 0.2f);
                }
            
            
        }
        if (Input.touchCount == 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(Input.touchCount == 5)
        {
            Application.Quit();
        }
    }
    void zoom(float increment)
    {
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - increment, zoomOutMin, zoomOutMax);
     }    
}

