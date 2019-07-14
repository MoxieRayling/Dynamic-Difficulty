using System;
using System.Linq;
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
    public Graph graph;
    private Vector2 size;
    private List<Shot> shots;
    private bool busy;
    private DBManager dbm;
    private int test;
    private int shotsFired = 0;
    private EnemyFactory ef;
    private int updateCount = 0;
    private List<Shooter> enemies;
    private int wave = 1;
    private int shotsOnScreen = 0;
    private int hits = 0;

    public int UpdateCount { get => updateCount; }
    public List<Shooter> Enemies { get => enemies; set => enemies = value; }
    public int Wave { get => wave; }
    public int ShotsOnScreen { get => shotsOnScreen; set => shotsOnScreen = value; }
    public int Hits { get => hits; set => hits = value; }

    void Start()
    {
        //GetComponent<SpriteRenderer>().enabled = false;
        size = gameObject.GetComponent<BoxCollider2D>().size;
        shots = new List<Shot>();
        enemies = new List<Shooter>() { shooter1, shooter2, shooter3, shooter4, shooter5, shooter6 };
        ef = new EnemyFactory();
        ef.GetRandWave(enemies);
        player.Target = NearestEnemy();
        dbm = new DBManager();
        test = dbm.TestCount()+1;
        Debug.Log("Test " + test);
        dbm.InsertPlayer(player, wave, test);
        dbm.InsertTest(test, "GA Test");
    }

    void FixedUpdate()
    {
        if (!ef.RunningGA)
        {
            StartCoroutine(ef.RunGA());
        }
        updateCount++;
        if(player.Target == null || !player.Target.enabled)
            player.Target = WeakestEnemy();
        if (Enemies.FindAll(enemy => enemy.enabled).Capacity == 0)
        {
            Reset();
            player.Reset = true;
        }
        graph.UpdatePos(player.transform.position);
        player.SetDirection(graph.GetDirection());
    }

    public void HitPlayer(Shot s)
    {
        if (player.Invincible) {
            //Debug.Log("Miss");
        }
        else
        {
            Hits++;
            s.Enemy.Hits++;
            //Debug.Log("Hits: " + Hits);
            player.Hit();
        }
    }

    public void InsertShot(int enemyId)
    {
        dbm.InsertHit(enemyId, wave, test, shotsOnScreen);
    }

    public void RemoveShot(Shot shot)
    {
        shots.Remove(shot);
    }

    public void AddShot(Shot shot)
    {
        shotsFired++;
        shots.Add(shot);
    }

    private void Reset()
    {
        dbm.InsertWave(wave, test, enemies.FindAll(e => e.Id > 0).Count,Hits,updateCount, shotsFired );
        shots.ForEach(s => Destroy(s.gameObject));
        shots.Clear();
        Enemies.ForEach(enemy => enemy.Revive());
        ef.RunningGA = false;
        ef.GetBestWave(enemies);
        wave++;
        Debug.Log("Wave: " + wave);
        updateCount = 0;
        shotsFired = 0;
        shotsOnScreen = 0;
        hits = 0;
    }

    public List<Shot> GetShots()
    {
        return shots.FindAll(s => s.target == "Player");
    }

    public List<Shooter> GetLivingEnemies()
    {
        return Enemies.FindAll(e => e.enabled);
    }

    public Player GetPlayer()
    {
        return player;
    }

    public bool IsOutside(Vector2 pos)
    {
        if (pos.x > size.x / 2 || pos.x < -size.x / 2 || pos.y > size.y / 2 || pos.y < -size.y / 2) {
            return true;
        }
        return false;
    }

    void LateUpdate()
    {
        Clamp(player.transform);

    }

    private void Clamp(Transform child)
    {
        float xDiff = size.x / 2 - child.GetComponent<Collider2D>().bounds.size.x / 2;
        float yDiff = size.y / 2 - child.GetComponent<Collider2D>().bounds.size.y / 2;
        float x = Mathf.Clamp(child.position.x, -xDiff, xDiff);
        float y = Mathf.Clamp(child.position.y, -yDiff, yDiff);
        child.position = new Vector2(x, y);
    }

    public void InsertEnemy(Shooter s)
    {
        dbm.InsertEnemy(s, wave, test);
    }

    private Shooter NearestEnemy()
    {
        var alive = Enemies.FindAll(enemy => enemy.enabled);
        if (alive.Count > 0)
        {
            Vector2 pPos = player.transform.position;
            alive.Sort((e1, e2) => Vector2.Distance(pPos, e1.transform.position).CompareTo(Vector2.Distance(pPos, e2.transform.position)));
            return alive[0];
        }
        return null;
    }

    private Shooter WeakestEnemy()
    {
        var alive = Enemies.FindAll(enemy => enemy.enabled);
        if (alive.Count > 0)
        {
            alive.OrderBy(e => e.MaxHealth);
            return alive[0];
        }
        return null;
    }
}
