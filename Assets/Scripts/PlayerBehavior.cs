using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    //effect
    public ParticleSystem explod;

    float rotZ;

    float speed;
    Rigidbody2D playerRB;
    public GameObject lazer;

    //sound
    AudioSource audioSLazer;
    public AudioSource audioSExp;
    public AudioClip audioLazer;
    public AudioClip audioExplode;

    // Start is called before the first frame update
    void Start()
    {
        rotZ = transform.rotation.z;
        speed = 250;
        playerRB = GetComponent<Rigidbody2D>();
        audioSLazer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DirectionPlayer(string dir)
    {
        if (dir == "Up")
        {
            playerRB.velocity = new Vector2(0, speed * Time.deltaTime);
        }
        else if (dir == "Down")
        {
            playerRB.velocity = new Vector2(0, -speed * Time.deltaTime);
        }
        else if (dir == "Left")
        {
            playerRB.velocity = new Vector2(-speed * Time.deltaTime, 0);
        }
        else if (dir == "Right")
        {
            playerRB.velocity = new Vector2(speed * Time.deltaTime, 0);
        }
        else if (dir == "Stop")
        {
            playerRB.velocity = new Vector2(0, 0);
        }
        else if (dir == "UpLeft")
        {
            if (rotZ < 45)
            {
                rotZ = rotZ + 3;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
            }
        }
        else if (dir == "UpRight")
        {
            if (rotZ > -45)
            {
                rotZ = rotZ - 3;
                //transform.Rotate(0, 0, -1, Space.Self);
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
            }
        }
    }
    public void LazerShoot()
    {
        Instantiate(lazer, transform.position, transform.rotation);
        audioSLazer.PlayOneShot(audioLazer);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyLazer")
        {
            audioSExp.PlayOneShot(audioExplode);
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            Instantiate(explod, transform.position, explod.transform.rotation);
            StartCoroutine(DeadScene());
        }
    }
    IEnumerator DeadScene()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("PlayerDead");
    }
}
