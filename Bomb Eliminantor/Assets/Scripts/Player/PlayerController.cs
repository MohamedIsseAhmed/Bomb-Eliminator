using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private float turnSpeed;
  
    [SerializeField] private int segments;
    [SerializeField] private float xradius;
    [SerializeField] private float yradius;

    private LineRenderer line;

    private Vector3 desiredPosition = Vector3.zero;
    private Vector3 desiredTurn = Vector3.zero;

    private Animator animator;
    private float desiredAnimationSpeed;
   [SerializeField] private float animationBlendSpeed;
    private void Awake()
    {
       animator= GetComponent<Animator>();
       line = GetComponent<LineRenderer>();

    }
    void Start()
    {  
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreatePoints();
    }



    void CreatePoints()
    {
        float x;
        float y=0f;
        float z;

        float angle = 40f;
        Vector3[] posints = new Vector3[segments+1];
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            posints[i] = new Vector3(x, y, z);
         

            angle += (segments);
        }
        line.SetPositions(posints);
    }

 
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (ClampJoystic.instance.CanDrag)
            {
                HandleMovemnt();
            }
           
        }
        if (Input.GetButtonUp("Fire1"))
        {
            desiredAnimationSpeed = 0;
           
            animator.SetFloat("speed", desiredAnimationSpeed);
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
        if (desiredPosition.magnitude > 0)
        {
            desiredAnimationSpeed= 1;
            
        }
        else
        {
            desiredAnimationSpeed = 0;
           
        }
       
        animator.SetFloat("speed", Mathf.Lerp(animator.GetFloat("speed"), desiredAnimationSpeed, animationBlendSpeed * Time.deltaTime));

    }
  
}
