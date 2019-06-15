using System;
using System.Linq;
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
    public Canvas canvas;
    private List<Shooter> enemies;
    private Vector2 size;
    private GameObject[] shots;
    //public GameObject pixel;
    //private List<Pixel> pixels;
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
        player.SetTarget(NearestEnemy());
        if (enemies.FindAll(enemy => enemy.enabled).Capacity == 0 && shots.Length == 0)
        {
            enemies.ForEach(enemy => enemy.Revive());
        }
        Graph();
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

    private void Graph()
    {
        for(int i = 0; i<20; i++) {
            var y = (float)Direction(i*18);
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

    private double Direction(double x)
    {

        var pos = player.transform.position;
        double result = 0;
        enemies.ForEach(e => result += EnemyBias(x, e, e.transform.position - pos));
        //Debug.Log("The value for " + x + " is " + result);
        shots.ToList().ForEach(s => result += ShotBias(x, s, s.transform.position - pos));
        //Debug.Log("The value for " + x + " is " + result);
        result += Dist(x, -1, 1, Mathf.Atan2(-1 * pos.y, -1 * pos.x) * Mathf.Rad2Deg);
        Debug.Log("The value for " + x + " is " + result);
        return result;
    }

    private double ShotBias(double x, GameObject s, Vector3 dir)
    {
        return Dist(x, 100, 1, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }

    private double EnemyBias(double x, Shooter e, Vector3 dir)
    {
        return Dist(x, Math.Exp(-1* dir.magnitude)*100, 1, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }

    private double Dist(double x, double weight, double spread, double peak)
    {
        if (x == 90) return 0;

        var result = weight / Math.Sqrt(Math.PI * 2) * Math.Pow(Math.E, (-1 * Math.Pow(spread * (x - peak), 2) / 2)) +
            weight / Math.Sqrt(Math.PI * 2) * Math.Pow(Math.E,    (-1 * Math.Pow(spread * (x - peak + 360), 2) / 2)) +
            weight / Math.Sqrt(Math.PI * 2) * Math.Pow(Math.E,    (-1 * Math.Pow(spread * (x - peak - 360), 2) / 2));
        Debug.Log("Input: " + x + ", Weight: " + weight + ", Spread: " + spread + ", Peak: " + peak + ", Result: " + result);
        return result;
    }
}
