using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField]
    private GameObject punchSlayer;


    private void OnTriggerEnter2D(Collider2D target) // Cambié "onTriggerEnter2D" a "OnTriggerEnter2D"
    {
        if (target.tag == TagManager.Tags.EnemyTag || target.tag == TagManager.Tags.PlayerTag) // Cambié "Collision" a "target"
        {
            Instantiate(punchSlayer, new Vector3(transform.position.x, transform.position.y, -4.0f), Quaternion.identity);
        }
    }
}

