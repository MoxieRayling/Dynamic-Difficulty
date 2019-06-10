﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float fireRate;
    public Room room;
    private Rigidbody2D rb;
    private Vector3 change;
    public GameObject shot;
    private bool busy;
    public int health;
    public float shotSpeed;
    public Vector3 spawn;
    private SpriteRenderer sprite;
    private Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        spawn = transform.position;
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        StartCoroutine(Startup());
    }

    void Update()
    {
        float xDiff = transform.position.x - target.position.x;
        float yDiff = transform.position.y - target.position.y;
        float bearing = (float)(Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI);
        bearing += 270;
        if (bearing > 180) bearing -= 360;
        //Debug.Log(bearing + " " + rb.rotation);
        if ( rb.rotation < bearing + 15 && rb.rotation > bearing - 15 && !busy)
        {
            StartCoroutine(Shot());
        }
        if (!busy)
        {
            var pos = transform.position;
            var dir = target.position - pos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90 ;
            var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = targetRotation;
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    IEnumerator Startup()
    {
        busy = true;
        yield return new WaitForSeconds(1);
        busy = false;
    }

    IEnumerator Shot()
    {
        busy = true;
        yield return new WaitForSeconds(0.15f/fireRate);
        var pos = transform.position;
        var dir = target.position - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        
        //yield return new WaitForSeconds(0f);
        GameObject temp = Instantiate(shot, transform.position + dir.normalized * 0.5f, Quaternion.AngleAxis(angle, Vector3.forward));
        temp.GetComponent<Shot>().dir = dir;
        temp.GetComponent<Shot>().target = "Player";
        temp.GetComponent<Shot>().vel = shotSpeed;

        yield return new WaitForSeconds(0.35f/fireRate);
        busy = false;
    }

    public void Hurt()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        enabled = false;
        col.enabled = false;
        sprite.enabled = false;
    }

    public void Revive()
    {
        enabled = true;
        col.enabled = true;
        sprite.enabled = true;
        health = 4;
        StartCoroutine(Startup());
    }
}
