using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;

    public bool inPlay;

    public Transform paddle, explosion, powerUp;

    public float speed;

    public GameManager gm;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver)
        {
            transform.position = paddle.position;
            return;
        }

        if (!inPlay)
        {
            transform.position = paddle.position;
        }

        //or use GetKeyDown("space");
        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
    }

    //Need to spell this correctly
    //For triggers
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bottom"))
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
        }
    }

    //For colliders
    //Collision2D does not have the CompareTag function tied to it
    //Instantiate creates the particle effect in the position of the brick
    //Destroy command always needs a game object attached to it
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Brick"))
        {
            //Accesses the script as soon as they collide
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();
            if (brickScript.hitsToBreak > 1)
            {
                brickScript.BreakBrick();
            }
            else
            {
                //min is inclusive and max is exclusive
                int randomChance = Random.Range(1, 101);
                if (randomChance < 50)
                {
                    Instantiate(powerUp, other.transform.position, other.transform.rotation);
                }

                Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(newExplosion.gameObject, 3f);

                //Goes to the gameObject's script to get the value of points
                gm.UpdateScore(brickScript.points);

                gm.UpdateNumberOfBricks();

                Destroy(other.gameObject);
            }

            audio.Play();
        }
    }
}
