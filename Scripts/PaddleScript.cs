using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    //changable in the editor
    public float speed;

    //changable in the editor
    public float rightScreenEdge, leftScreenEdge;

    float horizontal;

    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.gameOver)
            return;

        //returns positive or negative 1 depending on the key pressed
        horizontal = Input.GetAxis("Horizontal");

        //Vector2.right gives 1 in the x position and 0 in the y position
        //Time.deltaTime returns the time since the last frame
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);

        //Checks if the player is out of bounds
        if(transform.position.x < leftScreenEdge){
            transform.position = new Vector2(leftScreenEdge, transform.position.y);
        }
        if(transform.position.x > rightScreenEdge){
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }
    }

    //Reminder: Destroy needs a gameObject
    //A trigger so we do not need to have a transform reference
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Extra Life")){
            gm.UpdateLives(1);
            Destroy(other.gameObject);
        }
    }
}
