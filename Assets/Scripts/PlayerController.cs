using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//ALL TEXT CANVAS OBJECTS MUST BE ASSIGNED TO THE PLAYERCONTROLLER SLOTS TO WORK
public class PlayerController : MonoBehaviour {
//ship movement

    public float yawSpeed;
    public float rollSpeed;
    public float pitchSpeed; 
    public float BoostSpeed;
    public float BrakeSpeed;
    public float ShipSpeed;
    public Rigidbody projectile;
    public float ProjSpeed;
    public Transform Launch;
    public float DefaultSpeed;
    public float speed;
    public Text scoreText;
    public Text winText;
//score
    private int score; 
    private Rigidbody rb;

    //Hides bonus text, sets count to 0
    void Start() {
        ShipSpeed = DefaultSpeed;
        score = 0;
        SetscoreText();
        winText.text = "";
    }
    
    void Update () {
        transform.Translate(Vector3.forward * ShipSpeed * Time.deltaTime);
        Screen.lockCursor = true;
        if (Input.GetKey(KeyCode.Space)) {
                ShipSpeed = BoostSpeed; //accelerate
        }
        else if(Input.GetKey(KeyCode.B)) {
                ShipSpeed = BrakeSpeed; //decelerate
        }
        else {
                ShipSpeed = DefaultSpeed;
        }
        if (Input.GetKey(KeyCode.A)){ // yaw left
                transform.Rotate(Vector3.up, - yawSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) { //yaw right
                transform.Rotate(Vector3.up, yawSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W)) { //pitch down
                transform.Rotate(Vector3.right, - pitchSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) { //pitch up
                transform.Rotate(Vector3.right, pitchSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E)) { //roll right
                transform.Rotate(Vector3.forward, - rollSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q)) { //roll left
                transform.Rotate(Vector3.forward, rollSpeed * Time.deltaTime);
        }
    }
    //Updates count of cubes collected
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag ("Pickup")) {
            other.gameObject.SetActive (false);
            score = score + 1;
            SetscoreText();
            //Opens door to lv 2 (destroys gameobject "Door 1")
            if (score == 12){
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

     }
}
   