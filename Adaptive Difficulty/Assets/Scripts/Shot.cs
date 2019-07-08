using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public string target;
    public float vel;
    public Vector3 dir;
    public SpriteRenderer sr;
    public bool inactive = false;
    private Room room;
    private Shooter enemy;

    public Shooter Enemy { get => enemy; set => enemy = value; }

    void Start()
    {
        sr.enabled = false;
        room.AddShot(this);
        if (target == "Player") room.ShotsOnScreen++;
    }

    void FixedUpdate()
    {
        transform.position = transform.position + dir.normalized * vel;
        if (room.IsOutside(transform.position))
        {
            KillShot();
        }
    }

    private void KillShot()
    {
        room.RemoveShot(this);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == target && target == "Enemy")
        {
            col.gameObject.GetComponent<Shooter>().Hurt();
            inactive = true;
            KillShot();
        }
        if (col.tag == target && target == "Player")
        {
            room.InsertShot(Enemy.Id);
            room.HitPlayer(this);
            room.ShotsOnScreen--;
            KillShot();
        }
    }

    public void SetRoom(Room room)
    {
        this.room = room;
    }
}
