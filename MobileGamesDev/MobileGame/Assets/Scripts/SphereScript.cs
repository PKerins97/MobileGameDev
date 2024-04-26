using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour, I_Interactable
{
    [SerializeField]
    Material material;
    [SerializeField]
    Terrain terrain;
    private float initialDistance;
    private Vector3 initialScale;
    private float initialRotation;

    void Start()
    {
       terrain = FindAnyObjectByType<Terrain>();

    }

    public void processTap()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void processDrag(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.gameObject == terrain.gameObject)
            {
                print("W");
                float terrainHeight = terrain.SampleHeight(hit.point);

                Vector3 desiredPosition = new Vector3(hit.point.x, terrainHeight + transform.localScale.y / 2f, hit.point.z);

                transform.position = desiredPosition;
            }
         
        }
    }

    public void deselectedObject()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void processScale()
    {
      
    }

    public void processRotate()
    {
        float rotationAmount = 5f;
        transform.Rotate(Vector3.up, rotationAmount);


    }
}
