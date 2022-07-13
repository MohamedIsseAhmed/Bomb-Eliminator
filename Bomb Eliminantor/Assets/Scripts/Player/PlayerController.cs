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
  
    [SerializeField] private int segmentsOfLineRenderer;

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

   
    private bool isAiming;
    private bool isDraging;
    public bool ÝsDraging { get { return isDraging; } private set { } }

    private Transform currentTargetEnemyTransform;
    private Shooting aimAndShoot;
    private Transform currentWeapon;  
   
    private GunController gunController;

    private Collider[] enemyAroundResults = new Collider[10];
    [SerializeField] private int enemyColliders;
    private float distanceBetweenPlayerAndEnemy = Mathf.Infinity;

    private Rigidbody rigidbody;
    private Vector3 moveVector=Vector3.zero;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>(); 
       gunController = GetComponent<GunController>();
       animator = GetComponent<Animator>();
       line = GetComponent<LineRenderer>();
       aimAndShoot = GetComponent<Shooting>();
    }
    void Start()
    {
        line.positionCount = segmentsOfLineRenderer + 1;
        line.useWorldSpace = false;
        CreateCircle();
        
    }


    void CreateCircle()
    {
        float x;
        float y=0f;
        float z;

        float angle = 40f;
        Vector3[] posints = new Vector3[segmentsOfLineRenderer+1];
        for (int i = 0; i < (segmentsOfLineRenderer + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            posints[i] = new Vector3(x, y, z);
         

            angle += (segmentsOfLineRenderer);
        }
        line.SetPositions(posints);
    }

 
    void Update()
    {

        if (GameManager.instance.GameOver) 
        {
            return; 
        }
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
            isDraging = false;
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
        moveVector = desiredPosition;
     
        transform.position += desiredPosition;
        desiredTurn = Vector3.forward * vertical + Vector3.right * horizontal;
        if(desiredTurn!=Vector3.zero)
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
        gunController.WeaponPositionAndRotationOnShootingPlayer();
    }

    private IEnumerator Aim()
    {
        animator.ResetTrigger("RifleDown");
        yield return new WaitForSeconds(0.09f);
        animator.SetTrigger("Shoot");

    }
    private void CheckEnemyAround()
    {
        enemyColliders = Physics.OverlapSphereNonAlloc(transform.position, sphereRadius, enemyAroundResults, enemyLayerMaks,QueryTriggerInteraction.Ignore);
     
        for (int i = 0; i < enemyColliders; i++)
        {
            Enemy enemy = enemyAroundResults[i].GetComponent<Enemy>();
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
           
            if (Vector3.Distance(transform.position, enemy.transform.position) < shootingDistanceRange)
            {
                enemy.ActivateCrossHair(true);
                if (distanceToEnemy< distanceBetweenPlayerAndEnemy)
                {
                    currentTargetEnemyTransform=enemy.transform;
                    aimAndShoot.AimAndShoot(currentTargetEnemyTransform);
                    distanceBetweenPlayerAndEnemy = distanceToEnemy;
                   
                }
                else
                {
                    currentTargetEnemyTransform = enemy.transform;
                    aimAndShoot.AimAndShoot(currentTargetEnemyTransform);
                    enemy.ActivateCrossHair(true);
                }
            }
            else
            {
                enemy.ActivateCrossHair(false);
            }
           
        }
        
       
    }
  
}
