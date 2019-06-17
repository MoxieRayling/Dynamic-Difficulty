using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float fireRate;
    public int health;
    public float shotSpeed;
    public Room room;
    private Rigidbody2D rb;
    private Vector3 change;
    public GameObject shot;
    private bool busy;
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
    }

    void FixedUpdate()
    {
        float xDiff = transform.position.x - target.position.x;
        float yDiff = transform.position.y - target.position.y;
        float bearing = (float)(Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI);
        bearing += 270;
        if (bearing > 180) bearing -= 360;
        //Debug.Log(bearing + " " + rb.rotation);
        if (room.UpdateCount % fireRate == 0)
        {
            Shot();
        }
        if (!busy)
        {
            //StartCoroutine(Shot());
        }

        var pos = transform.position;
        var dir = target.position - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90 ;
        var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = targetRotation;
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        
    }

    private void Shot()
    {
        busy = true;
     //   yield return new WaitForSeconds(0.15f/fireRate);
        var dir = target.position - transform.position;

        GameObject temp = Instantiate(shot, transform.position + dir.normalized * 0.5f, Quaternion.identity);
        var script = temp.GetComponent<Shot>();
        script.dir = dir;
        script.target = "Player";
        script.vel = shotSpeed;
        script.SetRoom(room);
        temp.GetComponent<SpriteRenderer>().color = new Color(255,0,0);
        temp.tag = "EnemyShot";
       // yield return new WaitForSeconds(0.35f/fireRate);
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
    }
}
