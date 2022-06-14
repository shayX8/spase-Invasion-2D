using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    public ParticleSystem explode;
    public ParticleSystem explodePlayer;

    //sound
    public AudioClip audioShot;
    public AudioClip audioExplode;
    public GameObject audioSExp;
    AudioSource audioS;

    float timershoot;

    //direction
    bool dirPosX, dirPosY;
    int dirChanger;

    //lazer
    public GameObject enemyLazer;

    //creat enemys
    GameObject enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        audioSExp = GameObject.Find("ExplodeSound");
        enemyManager = GameObject.Find("EnemyCreats");
        dirPosX = true;
        dirPosY = true;
        dirChanger = 0;
        StartCoroutine(ShootLazer());

        //sound
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //direction
        if (dirPosX)
            transform.Translate(0.0025f, 0, 0);
        else
            transform.Translate(-0.0025f, 0, 0);
        if (dirPosY)
            transform.Translate(0, 0.001f, 0);
        else
            transform.Translate(0, -0.001f, 0);
        if (dirChanger <100)
        {
            dirChanger++;
        }
        else
        {
            dirChanger = 0;
            if (dirPosX)
            {
                if (dirPosY)
                    dirPosY = false;
                else
                    dirPosY = true;
                dirPosX = false;
            }
            else
                dirPosX = true;
        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Lazer")
        {
            //effect
            Instantiate(explode, transform.position, explode.transform.rotation);
            //sound
            audioSExp.GetComponent<AudioSource>().PlayOneShot(audioExplode);
            //audioSExp.PlayOneShot(audioExplode);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            enemyManager.GetComponent<EnemyCreats>().enemyCounter--;
            if (enemyManager.GetComponent<EnemyCreats>().enemyCounter == 0)
            {
                enemyManager.GetComponent<EnemyCreats>().lvl++;
                while (enemyManager.GetComponent<EnemyCreats>().enemyCounter < enemyManager.GetComponent<EnemyCreats>().lvl)
                {
                    enemyManager.GetComponent<EnemyCreats>().CreatEnemy();
                }
            }
        }
    }
    IEnumerator ShootLazer()
    {
        timershoot = Random.Range(0, 6.1f);

        yield return new WaitForSeconds(timershoot);

        Instantiate(enemyLazer, transform.position, enemyManager.transform.rotation);
        audioS.PlayOneShot(audioShot);

        StartCoroutine(ShootLazer());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSExp.GetComponent<AudioSource>().PlayOneShot(audioExplode);
            //audioSExp.PlayOneShot(audioExplode);
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            Instantiate(explode, transform.position, explode.transform.rotation);
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            Instantiate(explodePlayer, collision.transform.position, explodePlayer.transform.rotation);
            StartCoroutine(DeadScene());
        }
    }
    IEnumerator DeadScene()
    {
        
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("PlayerDead");
    }
}
