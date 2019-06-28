using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 0.15f;
    public GameObject shot;
    public Room room;
    private Vector3 dir;
    private Vector2 best;
    private Rigidbody2D rb;
    private CircleCollider2D col;
    private Vector3 change;
    private bool dash = false;
    private Shooter target;
    private float fireRate = 50;
    private float shotSpeed = 0.05f;
    private bool shooting = false;
    private float steering = 0.2f;
    private bool reset = false;
    private string targetting = "nearest";
    private float reactionSpeed = 0;
    private float accuracy = 0;


    public bool Reset { get => reset; set => reset = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public string Targetting { get => targetting; set => targetting = value; }
    public float ShotSpeed { get => shotSpeed; set => shotSpeed = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Accuracy { get => accuracy; set => accuracy = value; }
    public float ReactionSpeed { get => reactionSpeed; set => reactionSpeed = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        if (room.UpdateCount % FireRate == 0 && target != null)
        {
            Shoot();
        }
    }

    public void SetDirection(Vector2 dir)
    {
        this.best = dir;
    }

    public Shooter GetTarget()
    {
        return target;
    }

    void MovePlayer()
    {
        if (Reset) { 
            rb.MovePosition(new Vector2(0, 0));
            Debug.Log("reset");
            Reset = false;
            return;
        }
        else
        {
            dir = new Vector3(dir.x + best.x * steering, dir.y + best.y * steering).normalized;
            rb.MovePosition(transform.position + dir * Speed);
        }
    }

    IEnumerator Dash()
    {
        dash = true;
        col.enabled = false;
        Speed = 20;
        yield return new WaitForSeconds(0.3f);
        col.enabled = true;
        Speed = 4;
        yield return new WaitForSeconds(0.3f);
        dash = false;
    }

    private void Shoot()
    {
        shooting = true;

        var pos = transform.position;
        var dir = target.transform.position - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        GameObject temp = Instantiate(shot, transform.position + dir.normalized * 0.5f, Quaternion.AngleAxis(angle, Vector3.forward));
        var script = temp.GetComponent<Shot>();
        script.dir = dir;
        script.target = "Enemy";
        script.vel = ShotSpeed;
        script.SetRoom(room);
        shooting = false;
    }

    public void SetTarget(Shooter target)
    {
        this.target = target;
    }

    public void SetSteering(double s)
    {
        this.steering = (float)s;
    }
}
