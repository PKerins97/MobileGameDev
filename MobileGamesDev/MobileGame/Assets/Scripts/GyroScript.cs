using UnityEngine;
using System.Collections;

public class GyroScript : MonoBehaviour

{
    private readonly float moveSpeed = 1f;
   

    public bool isFlat = true;
    private Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 tilt = Input.acceleration;

        if (isFlat)
        {
            
            Quaternion moveObject = Quaternion.Euler(-tilt.y * moveSpeed * 90, tilt.x * moveSpeed * 90, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, moveObject, Time.deltaTime * moveSpeed);

        }
        rigid.AddForce(tilt);
    }
}
