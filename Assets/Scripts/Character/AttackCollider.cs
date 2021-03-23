using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField]
    private string targetTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == targetTag)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
