using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;
    [SerializeField] private LayerMask enemyLayerMaks;
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private float turnSpeed;
  
    [SerializeField] private int segments;
    [SerializeField] private float xradius;
    [SerializeField] private float yradius;
    [SerializeField] private float sphereRadius;
    [SerializeField] private float rotationFactor;
    [SerializeField] private float shootingDistanceRange=10;

    [SerializeField] private GunScriptableObject weapon;
    [SerializeField] private Vector3 lookOffset;

    private LineRenderer line;

    private Vector3 desiredPosition = Vector3.zero;
    private Vector3 desiredTurn = Vector3.zero;

    private Animator animator;
    private float desiredAnimationSpeed;
   [SerializeField] private float animationBlendSpeed;

    private Vector3 weaponOriginPosition;
    private Vector3 weaponOriginRotation;
    private bool isAiming;
    private bool isDraging;
    public bool ÝsDraging { get { return isDraging; } private set { } }
    private Transform currentTarget;
    IAimAndShoot aimAndShoot;
    private Transform currentWeapon;
    private Vector3 gunSpawnPosition;
    [SerializeField] private Transform gunParent;
    private Vector3 gunrotaion;

    private GunController gunController;
    private void Awake()
    {   
       gunController = GetComponent<GunController>();
       animator = GetComponent<Animator>();
       line = GetComponent<LineRenderer>();
       aimAndShoot = GetComponent<IAimAndShoot>();

    }
    void Start()
    {
        //gunSpawnPosition = new Vector3(0.215000004f, 0.832000017f, 0.414999992f);
        //gunrotaion = new Vector3(319.814056f, 6.36982012f, 81.3262711f);
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreateCircle();
        //currentWeapon=Instantiate(weapon.gunPrfab.transform,gunSpawnPosition,Quaternion.Euler(gunrotaion),gunParent);
        //weaponOriginPosition =gunSpawnPosition;
        //weaponOriginRotation = gunrotaion;

    }
    void CreateCircle()
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
        if (Input.GetMouseButtonDown(0))
        {
            isAiming = false;
           
            isDraging = true;
        }
        if (Input.GetButton("Fire1"))
        {
          
            if (ClampJoystic.instance.CanDrag)
            {
                HandleMovemnt();     
            }
          
        }
       
        if (Input.GetMouseButtonUp(0))
        {
            isDraging=false;
            desiredAnimationSpeed = 0;
            animator.SetFloat("speed", desiredAnimationSpeed);
            BringBackWeaponOriginPosition();
           
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAiming = true;
            StartCoroutine(Aim());
          
        }
        CheckEnemyAround();
       
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
           gunController.BringBackWeaponOriginPosition();
        }
        if (desiredAnimationSpeed > 0.95f)
        {
            gunController.RepositionWeaponWhenRunning();
        }
       
        if (!isAiming)
        {
            animator.SetFloat("speed", Mathf.Lerp(animator.GetFloat("speed"), desiredAnimationSpeed, animationBlendSpeed * Time.deltaTime));
        }
    }
    //AnimationEvent
    private void BringBackWeaponOriginPosition()
    {
        gunController.BringBackWeaponOriginPosition();

    } 
    //AnimationEvent
    private void RepositionWeaponWhenRunning()
    {
        currentWeapon.transform.localPosition=new Vector3(0.116065338f, 0.924593449f, 0.141138822f);
        currentWeapon.transform.localEulerAngles = new Vector3(273.997253f, 279.588867f, 144.62648f);
    }
    //AnimationEvent
    private void WeaponPositionAndRotationOnShooting()
    {
        //currentWeapon.transform.localPosition=new Vector3(0.305999994f, 0.90200001f, 0.0689999983f);
        //currentWeapon.transform.localEulerAngles = new Vector3(302.237488f, 61.6823273f, 348.534119f);
        gunController.WeaponPositionAndRotationOnShooting();
    }

    private IEnumerator Aim()
    {
        animator.ResetTrigger("RifleDown");
        yield return new WaitForSeconds(0.09f);
        animator.SetTrigger("Shoot");

    }
    private void CheckEnemyAround()
    {
        Collider[] results=new Collider[10];
        int enemyColliders = Physics.OverlapSphereNonAlloc(transform.position, sphereRadius, results,enemyLayerMaks);
      
        for (int i = 0; i <enemyColliders; i++)
        {
            Enemy enemy = results[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                currentTarget = enemy.transform;
                print(results[i].transform.name);
                
                aimAndShoot.AimAndShoot(currentTarget);
                print("shoot");
            }
            else
            {
                print("null");
            }
            
        }
       
    }
  
}
