using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Shooter shooter1;
    public Shooter shooter2;
    public Shooter shooter3;
    public Shooter shooter4;
    public Shooter shooter5;
    public Shooter shooter6;
    public Player player;
    private List<Shooter> enemies;
    private Vector2 size;
    private GameObject[] shots;
    // Start is called before the first frame update
    void Start()
    {
        size = gameObject.GetComponent<BoxCollider2D>().size;
        enemies = new List<Shooter>() { shooter1, shooter2, shooter3, shooter4, shooter5, shooter6 };
        EnemyFactory.Setup(shooter1, 1, 4, 4, 4);
        EnemyFactory.Setup(shooter2, 1, 4, 4, 4);
        EnemyFactory.Setup(shooter3, 1, 4, 4, 4);
        EnemyFactory.Setup(shooter4, 1, 4, 4, 4);
        EnemyFactory.Setup(shooter5, 1, 4, 4, 4);
        EnemyFactory.Setup(shooter6, 1, 4, 4, 4);
        player.SetTarget(NearestEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        shots = GameObject.FindGameObjectsWithTag("EnemyShot");
        //Debug.Log("Shots: " + shots.Length);
        player.SetTarget(NearestEnemy());
        if (enemies.FindAll(enemy => enemy.enabled).Capacity == 0 && shots.Length == 0)
        {
            enemies.ForEach(enemy => enemy.Revive());
        }
    }

    public bool IsOutside(Vector2 pos)
    {
        if (pos.x > size.x / 2 || pos.x < -size.x / 2 || pos.y > size.y / 2 || pos.y < -size.y / 2)
            return true;
        return false;
    }

    void LateUpdate()
    {
        foreach (Transform child in transform)
        {
            float xDiff = size.x / 2 - child.GetComponent<Collider2D>().bounds.size.x / 2;
            float yDiff = size.y / 2 - child.GetComponent<Collider2D>().bounds.size.y / 2;
            float x = Mathf.Clamp(child.position.x, -xDiff, xDiff);
            float y = Mathf.Clamp(child.position.y, -yDiff, yDiff);
            child.position = new Vector2(x, y);
        }
    }

    private Shooter NearestEnemy()
    {
        var alive = enemies.FindAll(enemy => enemy.enabled);
        if (alive.Capacity > 0)
        {
            Vector2 pPos = player.transform.position;
            alive.Sort((e1, e2) => Vector2.Distance(pPos, e1.transform.position).CompareTo(Vector2.Distance(pPos, e2.transform.position)));
            return alive[0];
        }
        return null;
    }
}
