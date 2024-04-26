using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour, I_Interactable
{
    [SerializeField]
    Material material;
    public void deselectedObject()
    {
        this.GetComponent<Renderer>().material.color = Color.white;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void processTap()
    {
        this.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void processDrag(Ray ray)
    {
        float distance = Vector3.Distance(ray.origin, transform.position);

        Vector3 newPosition = new Vector3(ray.GetPoint(distance).x, ray.GetPoint(distance).y, transform.position.z);
        transform.position = newPosition;
    }

    public void processScale()
    {
        

    }

    public void processRotate()
    {
        float rotationAmount = 10f;
        transform.Rotate(Vector3.up, rotationAmount);
    }
}