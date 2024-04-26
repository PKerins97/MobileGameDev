using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create a game object with a cube collider to keep the cube at the same distance away 
public class CubeScript : MonoBehaviour, I_Interactable
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
        GetComponent<Renderer>().material.color = Color.red;
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
        float rotationAmount = 5f;
        transform.Rotate(Vector3.up, rotationAmount);
    }
}