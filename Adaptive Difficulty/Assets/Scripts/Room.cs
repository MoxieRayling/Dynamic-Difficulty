using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject shooter1;
    public GameObject shooter2;
    public GameObject shooter3;
    public GameObject shooter4;
    public GameObject shooter5;
    public GameObject shooter6;
    private GameObject player;
    private List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>() { shooter1, shooter2, shooter3, shooter4, shooter5, shooter6 };
        player = GameObject.FindWithTag("Player");
        EnemyFactory.Setup(shooter1, 1, 4, 4, 4);
        EnemyFactory.Setup(shooter2, 1, 4, 4, 4);
        EnemyFactory.Setup(shooter3, 1, 4, 4, 4);
        EnemyFactory.Setup(shooter4, 1, 4, 4, 4);
        EnemyFactory.Setup(shooter5, 1, 4, 4, 4);
        EnemyFactory.Setup(shooter6, 1, 4, 4, 4);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        Vector2 size = gameObject.GetComponent<BoxCollider2D>().size;
        foreach (Transform child in transform)
        {
            float xDiff = size.x / 2 - child.GetComponent<Collider2D>().bounds.size.x / 2;
            float yDiff = size.y / 2 - child.GetComponent<Collider2D>().bounds.size.y / 2;
            float x = Mathf.Clamp(child.position.x, -xDiff, xDiff);
            float y = Mathf.Clamp(child.position.y, -yDiff, yDiff);
            child.position = new Vector2(x, y);
        }
    }

    public GameObject NearestEnemy()
    {
        float dist = 100;
        GameObject result = null;
        foreach (GameObject e in enemies)
        {
            if (Vector3.Distance(player.transform.position, e.transform.position) < dist) result = e;
        }
        return result;
    }
}
