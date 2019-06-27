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
    public GameObject shot;
    private SpriteRenderer sprite;
    private Collider2D col;
    private bool inactive = false;
    private int id = 0;
    public int Id { get => id; set => id = value; }
    private int lifeTime = 0;
    public int LifeTime { get => lifeTime; set => lifeTime = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void SetInactive()
    {
        inactive = true;
    }

    public void Deactivate()
    {
        enabled = false;
        col.enabled = false;
        sprite.enabled = false;
    }

    public void Randomize()
    {
        health = (int)Math.Round((UnityEngine.Random.value + 0.1) * 10);
        shotSpeed = UnityEngine.Random.value / 10 + 0.01f;
        fireRate = (int)Math.Round(UnityEngine.Random.value * 90) + 30;
        lifeTime = 0;
    }

    void FixedUpdate()
    {
        
        if (inactive)
        {
            Deactivate();
        }
        if (enabled)
        {
            LifeTime++;
            float xDiff = transform.position.x - target.position.x;
            float yDiff = transform.position.y - target.position.y;

            if (room.UpdateCount % fireRate == 0)
            {
                Shoot();
            }

            var pos = transform.position;
            var dir = target.position - pos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
            var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = targetRotation;
        }
    }

    private void Shoot()
    {
        var dir = target.position - transform.position;

        GameObject temp = Instantiate(shot, transform.position + dir.normalized * 0.5f, Quaternion.identity);
        var script = temp.GetComponent<Shot>();
        script.dir = dir;
        script.target = "Player";
        script.vel = shotSpeed;
        script.SetRoom(room);
        temp.GetComponent<SpriteRenderer>().color = new Color(255,0,0);
        temp.tag = "EnemyShot";
    }

    public void Hurt()
    {
        health--;
        if (health <= 0)
        {
            Deactivate();
        }
    }

    public void Revive()
    {
        enabled = true;
        col.enabled = true;
        sprite.enabled = true;
        health = 4;
        inactive = false;
    }
}
