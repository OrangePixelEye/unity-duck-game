using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrigger : MonoBehaviour
{
 

    [SerializeField]
    private BoxCollider2D platformCollider;
    [SerializeField]
    private BoxCollider2D platformTrigger;
    // Start is called before the first frame update
    void Start()
    {
        //playerCollider = GameObject.Find("duck").GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "duck" || other.gameObject.name == "enemy")
        {
            Physics2D.IgnoreCollision(platformCollider, other, true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "duck" || collision.gameObject.name == "enemy")
        {
            Physics2D.IgnoreCollision(platformCollider, collision, false);
        }
    }
   
}
