﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject hit;
    private Rigidbody2D rb;
    private CircleCollider2D col;
    private Vector3 change;
    private bool dash = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = Input.mousePosition - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(change != Vector3.zero)
        {
            MovePlayer();
        }
        if (Input.GetKeyDown("space") && !dash)
        {
            StartCoroutine(Dash());
        }
        if (Input.GetMouseButtonDown(0))
        {
            GameObject temp = Instantiate(hit, transform.position + dir.normalized * 0.5f, Quaternion.AngleAxis(angle, Vector3.forward));
            temp.GetComponent<HitScript>().target = "Enemy";
        }
    }

    void MovePlayer()
    {
        rb.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);

    }
    IEnumerator Dash()
    {
        dash = true;
        col.enabled = false;
        speed = 20;
        yield return new WaitForSeconds(0.3f);
        col.enabled = true;
        speed = 4;
        yield return new WaitForSeconds(0.3f);
        dash = false;
    }
}
