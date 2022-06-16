using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private float turnSpeed;
    private Vector3 desiredPosition=Vector3.zero;
    private Vector3 desiredTurn = Vector3.zero;
    public int segments;
    public float xradius;
    public float yradius;
    LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();

        line.positionCount=segments + 1;
        line.useWorldSpace = false;
        CreatePoints();
    }


    void CreatePoints()
    {
        float x;
        float y=0f;
        float z;

        float angle = 40f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y,z));

            angle += (segments);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            HandleMovemnt();
        }
    }

    private void HandleMovemnt()
    {
        float horizontal = dynamicJoystick.Horizontal;
        float vertical = dynamicJoystick.Vertical;
        desiredPosition = new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);

        transform.position += desiredPosition;
        desiredTurn = Vector3.forward * vertical + Vector3.right * horizontal;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredTurn), turnSpeed * Time.deltaTime);

        
    }
  
}
