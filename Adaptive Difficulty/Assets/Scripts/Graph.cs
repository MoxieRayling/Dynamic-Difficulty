using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Graph : MonoBehaviour
{
    public GameObject pixelPrefab;
    public Room room;
    private List<Pixel> pixels;
    private Vector2 pos;
    private Candidate candidate;

    // Start is called before the first frame update
    void Start()
    {
        pixels = new List<Pixel>();
        for(int i = 0; i < 360; i++)
        {
            Pixel p = Instantiate(pixelPrefab).GetComponent<Pixel>();
            p.transform.SetPositionAndRotation(Rotate(new Vector2(1, 0),i),Quaternion.identity);
            pixels.Add(p);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<360; i++)
        {
            pixels[i].val = (float)Direction(i);
            pixels[i].transform.SetPositionAndRotation(pos + Rotate(new Vector2(1, 0), i), Quaternion.identity);
        }
    }

    public Vector2 GetDirection()
    {
        int max = 0;
        float val = pixels[0].val;
        for (int i = 1; i < 360; i++)
        {
            if (pixels[i].val < val)
            {
                val = pixels[i].val;
                max = i;
            }
        }
        return Rotate(new Vector2(1, 0), max); 
    }

    public void UpdatePos(Vector2 pos)
    {
        this.pos = pos;
    }

    private double Direction(double x)
    {
        var pos = room.GetPlayer().transform.position;
        double result = 0;
        room.GetLivingEnemies().ForEach(e => result += EnemyBias(x, e, e.transform.position - pos));
        //Debug.Log("The value for " + x + " is " + result);
        List<Shot> shots = room.GetShots();
        if(shots.Capacity>0) shots.ForEach(s => result += ShotBias(x, s, s.transform.position - pos));
        //Debug.Log("The value for " + x + " is " + result);
        result += Dist((x+180)%360, pos.magnitude* candidate.GetGene(5).Value(), candidate.GetGene(6).Value(), Mathf.Atan2(-1 * pos.y, -1 * pos.x) * Mathf.Rad2Deg);
        //Debug.Log("The value for " + x + " is " + result);
        return result;
    }

    private double ShotBias(double x, Shot s, Vector3 dir)
    {
        return Dist(x, candidate.GetGene(1).Value(), candidate.GetGene(2).Value(), Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }

    private double EnemyBias(double x, Shooter e, Vector3 dir)
    {
        return Dist(x, Math.Exp(-1 * dir.magnitude) * candidate.GetGene(3).Value(), candidate.GetGene(4).Value(), Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }

    private double Dist(double x, double weight, double spread, double peak)
    {
        if (x == 90) return 0;

        var result = weight / Math.Sqrt(Math.PI * 2) * Math.Pow(Math.E, (-1 * Math.Pow(spread * (x - peak), 2) / 2)) +
            weight / Math.Sqrt(Math.PI * 2) * Math.Pow(Math.E, (-1 * Math.Pow(spread * (x - peak + 360), 2) / 2)) +
            weight / Math.Sqrt(Math.PI * 2) * Math.Pow(Math.E, (-1 * Math.Pow(spread * (x - peak - 360), 2) / 2));
        //Debug.Log("Input: " + x + ", Weight: " + weight + ", Spread: " + spread + ", Peak: " + peak + ", Result: " + result);
        return result;
    }
    private Vector2 Rotate(Vector2 aPoint, float angle)
    {
        if (angle == 0) return aPoint;

        float rad = angle * Mathf.Deg2Rad;
        float s = Mathf.Sin(rad);
        float c = Mathf.Cos(rad);
        return new Vector2(
            aPoint.x * c - aPoint.y * s,
            aPoint.y * c + aPoint.x * s);
    }

    public void SetCandidate(Candidate c)
    {
        this.candidate = c;
    }
}
