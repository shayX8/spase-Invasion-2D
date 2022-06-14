using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreats : MonoBehaviour
{
    public GameObject enemy;
    Vector2 enemyPos;

    public int enemyCounter;
    public int lvl;
    // Start is called before the first frame update
    void Start()
    {
        lvl = 5;
        enemyCounter = 0;
        while (enemyCounter < lvl)
            CreatEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreatEnemy()
    {
        enemyPos.x = Random.Range(-2.3f, 2.3f);
        enemyPos.y = Random.Range(0, 4.4f);

        Instantiate(enemy, enemyPos, enemy.transform.rotation);
        enemyCounter++;
    }
}
