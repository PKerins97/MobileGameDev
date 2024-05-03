using UnityEngine;
using System.Collections;

public class GyroScript : MonoBehaviour

{

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
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
        }
        rigid.AddForce(tilt);
    }
}
