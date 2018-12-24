using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollingBallController : MonoBehaviour {
    //ALL TEXT CANVAS OBJECTS MUST BE ASSIGNED TO THE rollingball CONTROLLER SLOTS TO WORK

    public float speed;
    public Text scoreText;
    public Text winText;
    public Text finalwinText;

    private int score; 
    private int bonus;
    private Rigidbody rb;

    //Hides bonus text, sets count to 0
    void Start() {
        rb = GetComponent<Rigidbody>();
        score = 0;
        SetscoreText();
        winText.text = "";
        finalwinText.text = "";
    }
    //Controls player movement, move and fly (fly disabled until score>12), resets player to origin on death
    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
        if (Input.GetKey (KeyCode.Space)) {
           if (score >= 12) {
            Vector3 atas = new Vector3 (0,2,0);
            rb.AddForce(atas * speed);
          }
        }
        if (Input.GetKey (KeyCode.V)) {
            Vector3 atas = new Vector3 (0,-2,0);
            rb.AddForce(atas * speed);    
        }

        if (transform.position.y < -20) {
             transform.position = new Vector3(0, 0, 0);
        }
    }
    //Updates count of cubes collected
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag ("Pickup")) {
            other.gameObject.SetActive (false);
            score = score + 1;
            SetscoreText();
            //Opens door to lv 2 (destroys gameobject "Door 1")
            if (score == 12) {
                var x = GameObject.FindGameObjectsWithTag("Door")[0];
                Destroy (x);
            }
        }    
    }
    //Diplays Lv 2 text and deletes at 12>th cube
    void SetscoreText() {
        scoreText.text = "score: " + score.ToString();
        if (score == 12) {
            winText.text = "Press space to Fly and V to come down";
        }
        else if (score > 12) {
            winText.text = "";
        }
        if (score == 16) {
            finalwinText.text = "You Win!!";
        }
        

     }
}
   