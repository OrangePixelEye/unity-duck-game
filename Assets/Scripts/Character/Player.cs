using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DeadEventHandler();

public class Player : Character
{

    private static Player instance;

    public event DeadEventHandler Dead;

    
    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private bool airControl;

    public Rigidbody2D MyRigidbody { get; set; }

    private Vector2 startPos;

    private bool Colliding;

    public bool Slide { get; set; }

    public bool Jump { get; set; }

    public bool OnGround { get; set; }

    public static Player Instance {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    public override bool IsDead
    {
        get
        {
            if(health <= 0)
            {
                OnDead();
            }
            
            return health <= 0;
        }
    }

    private bool immortal = false;

    private float direction;

    private bool move;

    private float btnHorizontal;

    [SerializeField]
    private float imortalTime;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Screen.orientation = ScreenOrientation.Landscape;
        startPos = transform.position;
        //reference to my rigid body
        MyRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");

            OnGround = IsGrounded();

            if (move)
            {
                this.btnHorizontal = Mathf.Lerp(btnHorizontal, direction, Time.deltaTime * 2);
                Flip(direction);
                HandleMovement(btnHorizontal);
            }
            else
            {
                Flip(horizontal);
                HandleMovement(horizontal);
            }
            HandleLayers();
        }
    }
        
    public void OnDead()
    {
        if(Dead != null)
        {
            Dead();
        }
    }

    private void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -16)
            {
                Death();
            }
            HandleInput();
        }
    }

    private void HandleMovement(float horizontal)
    {
     if(MyRigidbody.velocity.y < 0)
        {
            MyAnimator.SetBool("land", true);
        }
     if(!Attack && !Slide && (OnGround || airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }
     if(Jump && MyRigidbody.velocity.y == 0) 
        {
            Jump = false;
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

   

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            MyAnimator.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            MyAnimator.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            MyAnimator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            MyAnimator.SetTrigger("throw");
        }

    }

    private void Flip(float flip)
    {
        if(flip > 0 && !facingRight || flip < 0 && facingRight)
        {
            ChangeDirection();
        }
    }

  

    private bool IsGrounded()
    {
        if(MyRigidbody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for(int i = 0; i<collider2Ds.Length; i++)
                {
                    if (collider2Ds[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            MyAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            MyAnimator.SetLayerWeight(1, 0);
        }
    }

    public override void ThrowBall(int value)
    {
        if(!OnGround && value == 1 || OnGround && value == 0)
        {
            base.ThrowBall(value);  
        }
        
    }

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    public override IEnumerator TakeDamage()
    {
        if (!immortal)
        {
            health -= 10;
            Jump = false;
            if (!IsDead)
            {
                MyAnimator.SetTrigger("damage");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(imortalTime);
                immortal = false;
            }
            else
            {
                MyAnimator.SetLayerWeight(1, 0);
                MyAnimator.SetTrigger("die");
            }
        }
        

    }

    public override void Death()
    {
        MyRigidbody.velocity = Vector2.zero;
        MyAnimator.SetTrigger("idle");
        health = 30;
        transform.position = startPos;
    }
    public void BtnJump()
    {
        MyAnimator.SetTrigger("jump");
        Jump = true;
    }
    public void BtnAttack()
    {
        MyAnimator.SetTrigger("attack");
    }
    public void BtnSlide()
    {
        MyAnimator.SetTrigger("slide");
    }
    public void BtnThrow()
    {
        MyAnimator.SetTrigger("throw");
    }
    public void BtnMove(float direction)
    {
        this.direction = direction;
        this.move = true;
    }
    public void BtnStopMove()
    {
        this.direction = 0;
        this.btnHorizontal = 0;
        move = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            GameManager.Instance.CollectedCoins++;
            Destroy(collision.gameObject);
            
        }
        else if(collision.gameObject.tag == "CheckPoint")
        {
            startPos = transform.position;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (collision.gameObject.tag == "LCheckPoint")
        {
            GameManager.Instance.Scene(1);
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if(collision.gameObject.tag == "Finish")
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.Instance.Scene(2);
        }
        else if(collision.gameObject.tag == "BCheckPoint")
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameManager.Instance.Scene(-1);
        }
        

    }
    
}