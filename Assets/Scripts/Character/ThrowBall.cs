using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class ThrowBall : MonoBehaviour
{
    [SerializeField]
    private float SpeedTest;

    private Rigidbody2D myRigidBody;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myRigidBody.velocity = direction * SpeedTest;
    }

    public void Initialize(Vector2 direction)
    {

        this.direction = direction;
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
