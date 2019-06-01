using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject melee;
    public GameObject shooter;
    private GameObject player;
    private EnemyFactory ef;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount <= 1 && Vector3.Distance(transform.position, player.transform.position) < 50)
        {
            Instantiate(Enemy(), transform);
        }
    }

    void LateUpdate()
    {
        Vector2 size = gameObject.GetComponent<BoxCollider2D>().size;
        foreach (Transform child in transform)
        {
            float xDiff = size.x / 2 - child.GetComponent<Collider2D>().bounds.size.x/2;
            float yDiff = size.y / 2 - child.GetComponent<Collider2D>().bounds.size.y/2;
            float x = Mathf.Clamp(child.position.x, -xDiff, xDiff);
            float y = Mathf.Clamp(child.position.y, -yDiff, yDiff);
            child.position = new Vector2(x, y);
        }
    }

    GameObject Enemy()
    {
        if (Random.value >= 0.5)
        {
            return melee;
        }
        else return shooter;
    }
}
