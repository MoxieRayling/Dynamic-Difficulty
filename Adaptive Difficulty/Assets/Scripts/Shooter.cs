using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    public Transform target;
    public float speed;
    private Rigidbody2D rb;
    private Vector3 change;
    public GameObject shot;
    private bool busy;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        shot.GetComponent<Shot>().target = "Player";
        shot.GetComponent<Shot>().vel = 4;
    }

    void Update()
    {
        float xDiff = transform.position.x - target.position.x;
        float yDiff = transform.position.y - target.position.y;
        float bearing = (float)(Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI);

        if ( rb.rotation <bearing - 75 && rb.rotation > bearing - 105 && !busy)
        {
            StartCoroutine(Shot());
        }
        if (!busy)
        {
            var pos = transform.position;
            var dir = target.position - pos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90 ;
            var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    IEnumerator Shot()
    {
        busy = true;

        yield return new WaitForSeconds(0.15f);
        var pos = transform.position;
        var dir = target.position - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        
        yield return new WaitForSeconds(0f);
        GameObject temp = Instantiate(shot, transform.position + dir.normalized * 0.5f, Quaternion.AngleAxis(angle, Vector3.forward));
        temp.GetComponent<Shot>().dir = dir+pos;

        yield return new WaitForSeconds(0.35f);
        busy = false;
    }
}
