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
    private List<Shooter> enemies;
    private Vector2 size;
    private List<Shot> shots;
    public Graph graph;
    private bool busy;
    private MovementGA ga;
    private int updateCount = 0;

    public int UpdateCount { get => updateCount; }
    public List<Shooter> Enemies { get => enemies; set => enemies = value; }

    void Start()
    {
        size = gameObject.GetComponent<BoxCollider2D>().size;
        shots = new List<Shot>();
        enemies = new List<Shooter>() { shooter1, shooter2, shooter3, shooter4, shooter5, shooter6 };
        EnemyFactory.Setup(shooter1);
        EnemyFactory.Setup(shooter2);
        EnemyFactory.Setup(shooter3);
        EnemyFactory.Setup(shooter4);
        EnemyFactory.Setup(shooter5);
        EnemyFactory.Setup(shooter6);
        player.SetTarget(NearestEnemy());
        ga = new MovementGA();
        ga.GenerationZero();

        //StartCoroutine(StartGA());
    }

    public void RemoveShot(Shot shot)
    {
        shots.Remove(shot);
    }

    public void AddShot(Shot shot)
    {
        shots.Add(shot);
    }

    private void Reset()
    {
        shots.ForEach(s => Destroy(s.gameObject));
        shots.Clear();
        Shot.playerHits = 0;
        Enemies.ForEach(enemy => enemy.Revive());
    }

    void FixedUpdate()
    {
        updateCount++;
        Debug.Log(Shot.playerHits);
        player.SetTarget(NearestEnemy());
        if (Enemies.FindAll(enemy => enemy.enabled).Capacity == 0)
        {
            Reset();
            player.ResetPositon();
        }
        graph.UpdatePos(player.transform.position);
        player.SetDirection(graph.GetDirection());
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
        if (pos.x > size.x / 2 || pos.x < -size.x / 2 || pos.y > size.y / 2 || pos.y < -size.y / 2)
            return true;
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

    private Shooter NearestEnemy()
    {
        var alive = Enemies.FindAll(enemy => enemy.enabled);
        if (alive.Capacity > 0)
        {
            Vector2 pPos = player.transform.position;
            alive.Sort((e1, e2) => Vector2.Distance(pPos, e1.transform.position).CompareTo(Vector2.Distance(pPos, e2.transform.position)));
            return alive[0];
        }
        return null;
    }
    IEnumerator StartGA()
    {
        
        for(int i = 0; i < 50; i++)
        {
            foreach(Candidate c in ga.GetCandidates())
            {
                Debug.Log(c.ToString() + "hi ");
                player.SetSteering( c.GetGene(0).Value());
                graph.SetCandidate(c);
                yield return new WaitForSeconds(0.2f);
                ((MovementCand)c).SetScore(Shot.playerHits);
                Reset();
                player.ResetPositon();
            }
            ga.NextGen();
        }
    }
}
