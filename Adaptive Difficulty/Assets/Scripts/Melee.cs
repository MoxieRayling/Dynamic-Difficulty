using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public Transform target;
    public float speed;
    public GameObject hit;
    private Rigidbody2D rb;
    private Vector3 change;
    private bool busy = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < 1 && !busy)
        {
            StartCoroutine(Slash());
        }
        if (!busy) { 
            Move();
        }

    }
    IEnumerator Slash()
    {
        busy = true;
        
        var pos = transform.position;
        var dir = target.position - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;
        yield return new WaitForSeconds(0.5f);
        GameObject temp = Instantiate(hit, transform.position + dir.normalized * 0.5f, Quaternion.AngleAxis(angle, Vector3.forward));
        temp.GetComponent<HitScript>().target = "Player";

        busy = false;
    }

    private void Move()
    {
        change = target.position - transform.position;
        if (change != Vector3.zero && target.position != transform.position)
            rb.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);

        var pos = transform.position;
        var dir = target.position - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
    }
}
