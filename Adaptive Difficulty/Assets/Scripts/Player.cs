using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject shot;
    public Room room;
    private Vector3 dir;
    private Vector2 best;
    private Rigidbody2D rb;
    private CircleCollider2D col;
    private Vector3 change;
    private bool dash = false;
    private Shooter target;
    private float fireRate = 1;
    private float shotSpeed = 4;
    private bool shooting = false;
    private float steering;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        /*
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
        if (!shooting && target != null) {
            StartCoroutine(Shoot());
        }
        /*
        if (Input.GetMouseButtonDown(0))
        {
            GameObject temp = Instantiate(hit, transform.position + dir.normalized * 0.5f, Quaternion.AngleAxis(angle, Vector3.forward));
            temp.GetComponent<HitScript>().target = "Enemy";
        }*/
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
        //rb.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);

        dir = new Vector3(dir.x + best.x*steering, dir.y + best.y*steering).normalized;
        rb.MovePosition(transform.position + dir * speed * Time.deltaTime);

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

    IEnumerator Shoot()
    {
        shooting = true;

        var pos = transform.position;
        var dir = target.transform.position - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        GameObject temp = Instantiate(shot, transform.position + dir.normalized * 0.5f, Quaternion.AngleAxis(angle, Vector3.forward));
        var script = temp.GetComponent<Shot>();
        script.dir = dir;
        script.target = "Enemy";
        script.vel = shotSpeed;
        script.SetRoom(room);
        yield return new WaitForSeconds(0.35f / fireRate);
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

    public void ResetPositon()
    {
        rb.MovePosition(new Vector2(0, 0));
    }
}
