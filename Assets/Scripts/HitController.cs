using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField]
    private GameObject punchSlayer;
    private PlayerController GetPlayer;

    private void Awake()
    {
        GetPlayer = GameObject.FindGameObjectWithTag(TagManager.Tags.PlayerTag).GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == TagManager.Tags.PlayerTag && !GetPlayer.isDefending)
        {
            Instantiate(punchSlayer, new Vector3(transform.position.x, transform.position.y, -4.0f), Quaternion.identity);
        }
        else if (target.tag == TagManager.Tags.EnemyTag)
        {
            Instantiate(punchSlayer, new Vector3(transform.position.x, transform.position.y, -4.0f), Quaternion.identity);
        }
    }
}
