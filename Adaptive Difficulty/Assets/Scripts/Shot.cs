using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public string target;
    public float vel;
    public Vector3 dir;
    public bool inactive = false;
    private Room room;
    public static int playerHits = 0;


    void Start()
    {
        room.AddShot(this);
    }

    void Update()
    {
        transform.position = transform.position + dir.normalized * vel * Time.deltaTime;
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
            playerHits++;
            KillShot();
        }
    }

    public void SetRoom(Room room)
    {
        this.room = room;
    }
}
