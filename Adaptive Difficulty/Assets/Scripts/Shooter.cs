using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform target;
    public float speed;
    private float fireRate;
    private int health;
    private int maxHealth;
    private float shotSpeed;
    public Room room;
    private Rigidbody2D rb;
    public GameObject shot;
    private SpriteRenderer sprite;
    private Collider2D col;
    private bool inactive = false;
    private int id = 0;
    private int lifeTime = 0;
    private int shotsFired = 0;
    private int hits = 0;

    public int Id { get => id; set => id = value; }
    public int LifeTime { get => lifeTime; set => lifeTime = value; }
    public float ShotSpeed { get => shotSpeed; set => shotSpeed = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int Health { get => health; set => health = value; }
    public int ShotsFired { get => shotsFired; set => shotsFired = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Hits { get => hits; set => hits = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
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

            if (room.UpdateCount % FireRate == 0)
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
        Health = (int)Math.Round((UnityEngine.Random.value) * 29)+1;
        MaxHealth = Health;
        ShotSpeed = UnityEngine.Random.value / 2 + 0.01f;
        FireRate = (int)Math.Round(UnityEngine.Random.value * 115) + 5;
        LifeTime = 0;
        ShotsFired = 0;
        Hits = 0;
    }

   

    private void Shoot()
    {
        shotsFired++;
        var dir = target.position - transform.position;

        GameObject temp = Instantiate(shot, transform.position + dir.normalized * 0.5f, Quaternion.identity);
        var script = temp.GetComponent<Shot>();
        script.dir = dir;
        script.Enemy = this;
        script.target = "Player";
        script.vel = ShotSpeed;
        script.SetRoom(room);
        temp.GetComponent<SpriteRenderer>().color = new Color(255,0,0);
        temp.tag = "EnemyShot";
    }

    public void Hurt()
    {
        Health--;
        if (Health <= 0)
        {
            room.InsertEnemy(this);
            Deactivate();
        }
    }

    public void Revive()
    {
        enabled = true;
        col.enabled = true;
        sprite.enabled = true;
        Health = 4;
        inactive = false;
    }
}
