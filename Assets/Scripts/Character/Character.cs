using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Character : MonoBehaviour
{
    public Animator MyAnimator { get; private set; }
    [SerializeField]
    protected GameObject ballPrefab;
    [SerializeField]
    protected Transform ballPosition;

    [SerializeField]
    protected float movementSpeed;
    protected bool facingRight;
    [SerializeField]
    protected int health;
    public bool Attack { get; set; }
    // Start is called before the first frame update
    public abstract bool IsDead { get; }

    public bool TakingDamage { get; set; }
    public EdgeCollider2D AttackCollider { get => attackCollider;  }

    [SerializeField]
    private List<string> damageSources;

    [SerializeField]
    private EdgeCollider2D attackCollider;

    public abstract void Death();

    public virtual void Start()
    {
        MyAnimator = GetComponent<Animator>();

        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
    public virtual void ThrowBall(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(ballPrefab, ballPosition.position, Quaternion.identity);
            tmp.GetComponent<ThrowBall>().Initialize(Vector2.right);
           
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(ballPrefab, ballPosition.position, Quaternion.identity);
            tmp.GetComponent<ThrowBall>().Initialize(Vector2.left);
        }
    }

    public abstract IEnumerator TakeDamage();

    public void MeleeAttack()
    {
        AttackCollider.enabled = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }
}
