using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlateHandler : MonoBehaviour
{
    [SerializeField] float spawnBoxCooldown;
    [SerializeField] float spawnBoxRange;
    [SerializeField] Animator animator;
    private float cd;
    private bool colliding = false;

    private HashSet<Transform> overlapingEntities = new();

    private void Start()
    {
        cd = spawnBoxCooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cd == 0 && (collision.transform.tag == "Player" || collision.transform.tag == "Robot"))
        {
            overlapingEntities.Add(collision.transform);
            spawnBox();
        }
        animator.SetBool("IsOn", false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        overlapingEntities.Remove(collision.transform);
    }

    private void Update()
    {
        if (cd > 0) {
            cd = Mathf.Max(cd - Time.deltaTime, 0);
            if (cd == 0 && overlapingEntities.Count > 0) {
                spawnBox();
            }
        }
    }

    private void spawnBox()
    {
        if (cd <= 0) {
            var spawnAngle = Random.Range(0, 2 * Mathf.PI);
            var spawnDistance = Random.Range(0, spawnBoxRange);
            Vector2 spawnVec = new Vector2(Mathf.Cos(spawnAngle), Mathf.Sin(spawnAngle));
            Level.Current.SpawnBox((Vector2)transform.position + spawnVec * spawnDistance);
        }
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
