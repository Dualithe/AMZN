using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlateHandler : MonoBehaviour
{
    [SerializeField] float spawnBoxCooldown;
    [SerializeField] float spawnBoxRange;
    [SerializeField] Animator animator;
    [SerializeField] GameObject box;
    private float cd;
    private bool colliding = false;

    private void Start()
    {
        cd = spawnBoxCooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cd == 0 && (collision.transform.tag == "Player" || collision.transform.tag == "Robot"))
        {
            spawnBox();
        }
        colliding = true;
        animator.SetBool("IsOn", false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cd = spawnBoxCooldown;
        colliding = false;
    }

    private void Update()
    {
        if (colliding == false)
        {
            cd -= Time.deltaTime;
            cd = Mathf.Max(cd, 0);
        }
    }

    private void spawnBox()
    {
        var spawnAngle = Random.Range(0, 2 * Mathf.PI);
        var spawnDistance = Random.Range(0, spawnBoxRange);
        Vector2 thisPos = transform.position;
        Vector2 spawnVec = new Vector2(Mathf.Cos(spawnAngle), Mathf.Sin(spawnAngle));
        Instantiate(box, ((spawnVec * spawnDistance) + thisPos), Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, spawnBoxRange);
    }

    public void setOn()
    {
        animator.SetBool("IsOn", true);
    }
}
