using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TouchInput leftZone;
    [SerializeField] private TouchInput rightZone;
    [SerializeField] private TouchInput gravityZone;
    private Inputs inputControlls;

    [Header("Variables")]
    public float bounceHeight;
    public float rotateSpeed;
    public float gravityMult;
    public float speedDiv;

    [Header("Components / Objects")]
    public Rigidbody2D rb;
    public GameObject Head;
    public Animator animator;
    public Camera sceneCamera;
    public GameObject GlobalVolume;

    [HideInInspector]
    public bool bumped = false;
    public int gravPoints = 0;
    public int bouncePoints = 0;
    private float speed;
    private float moveX;
    private float[] standardVariables = new float[5];
    private Vector2 mousePosition;
    private void Awake()
    {
        inputControlls = new Inputs();
    }
    void OnEnable() => inputControlls.Gameplay.Enable();
    void OnDisable() => inputControlls.Gameplay.Disable();
    private void Start()
    {
        speed = 1;
        Physics2D.IgnoreCollision(Head.GetComponent<Collider2D>(), this.GetComponent<Collider2D>(), true);
        standardVariables[0] =  bounceHeight;
        standardVariables[1] =  rotateSpeed;
        standardVariables[2] =  gravityMult;
        standardVariables[3] =  speedDiv;
        standardVariables[4] =  transform.localScale.y;
    }
    void Update()
    {
        getInputs();
    }
    bool left;
    bool right;
    bool gravity;
    private void FixedUpdate()
    {
        useInputs();
    }
    public void useInputs()
    {
        if (left) transform.Rotate(0f, 0f, rotateSpeed);
        if (right) transform.Rotate(0f, 0f, -rotateSpeed);
    }
    public void getInputs()
    {
        speed = rb.linearVelocity.magnitude;
        left = inputControlls.Gameplay.RotateLeft.IsPressed() || (leftZone && leftZone.IsHeld);
        right = inputControlls.Gameplay.RotateRight.IsPressed() || (rightZone && rightZone.IsHeld);
        gravity = inputControlls.Gameplay.GravityInput.IsPressed() || (gravityZone && gravityZone.IsHeld);

        if (gravity && Time.timeScale != 0f)
        {
            rb.gravityScale = gravityMult;
            Time.timeScale = 1.2f;
        }
        else if(Time.timeScale != 0f)
        {
            rb.gravityScale = 1f;
            Time.timeScale = 1f;
        }

        //for screenshotting for app store and to make it easier to test
       // if (Input.GetKeyDown(KeyCode.Space) == true && Time.timeScale == 1f)
       // {
       //     Time.timeScale = 0f;
       // }
       // else if (Input.GetKeyDown(KeyCode.Space) == true && Time.timeScale == 0f)
       // {
       //     Time.timeScale = 1f;
       // }
        // computer controlls


        // mobile controlls


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    
        if (bumped == false && collision.gameObject.tag != "power up")
        {
            StartCoroutine("squish");
            FindObjectOfType<AudioManager>().Play("Bounce");
            rb.linearVelocity = transform.up * bounceHeight + transform.up * (speed/speedDiv);
        }

    
   
        
    }
    IEnumerator bump()
    {
        FindObjectOfType<AudioManager>().Play("oof");
        //bounceHeight = standardVariables[0];
        //rotateSpeed = standardVariables[1];
        //gravityMult = standardVariables[2];
        //speedDiv = standardVariables[3];
        //gravPoints = 0;
        //bouncePoints = 0;
        bumped = true;
        animator.SetBool("bumped", bumped);
        Debug.Log("bumped");
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(3f);
        bumped = false;
        animator.SetBool("bumped", bumped);
        Debug.Log("not bumped");
        if(rb.linearVelocity.magnitude < .5f)
        {
            rb.linearVelocity = transform.up * 2;
        }

    }
    IEnumerator squish()
    {
  
        transform.localScale = new Vector2(standardVariables[4], standardVariables[4] * 0.8f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector2(standardVariables[4], standardVariables[4] * 1.2f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector2(standardVariables[4], standardVariables[4]);
  
    }
}
