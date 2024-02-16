using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour, I_Interactable
{
    private Renderer objectRenderer;
    private Color originalColor;
    private bool isSelected = false;
    private static SphereScript currentlySelectedObject;
    private bool dragging = false;
    private bool hasMoved = false;
    private float timer = 0f;
    private Transform objectToDrag;
    private float dist;
    private RaycastHit hit;
    GestureManagerScript actOn;
    public float MaxTapTime = 0.5f;
    public void processDrag()
    {
        if (!dragging)
            return;

        Touch touch = Input.touches[0];
        Vector3 posTouched = touch.position;
        Ray ray = Camera.main.ScreenPointToRay(posTouched);

        if (touch.phase == TouchPhase.Moved)
        {
            Vector3 dragVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            dragVec = Camera.main.ScreenToWorldPoint(dragVec);
            objectToDrag.position = ray.GetPoint(dist);
            hasMoved = true;
        }
    }

    public void processTap()
    {
        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 posTouched = touch.position;
        Ray ray = Camera.main.ScreenPointToRay(posTouched);

        foreach (Touch t in Input.touches)
        {
            switch (t.phase)
            {
                case TouchPhase.Began:
                    timer = 0f;
                    hasMoved = false;

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
                case TouchPhase.Stationary:
                    timer += Time.deltaTime;
                    break;
                case TouchPhase.Ended:
                    if ((timer < MaxTapTime) && !hasMoved)
                    {
                        actOn.tapAt(t.position);
                    }
                    if (dragging && (touch.phase == TouchPhase.Ended))
                    {
                        dragging = false;
                    }
                    break;
            }
        }
    }

    public void selectedObejct()
    {

        objectRenderer.material.color = Color.yellow;
        isSelected = true;
    }
    public void deselectedObject()
    {
        objectRenderer.material.color = originalColor;
        isSelected = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        actOn = FindObjectOfType<GestureManagerScript>();
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                   
                    if (currentlySelectedObject != null && currentlySelectedObject != this)
                        currentlySelectedObject.deselectedObject();
                    
                    if (!isSelected)
                        selectedObejct();
                    else
                        deselectedObject();
                    currentlySelectedObject = isSelected ? this : null;
                }
            }
        }
    }
}