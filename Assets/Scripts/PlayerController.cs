using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed = 500;
    private int count = 0;
	private int numPickups = 5;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI winText;
	public TextMeshProUGUI playerPositionText;
	public TextMeshProUGUI playerVelocityText;
	public Vector3 lastPosition;
	public Vector3 currentPosition;
	
	private void SetCountText(){
		scoreText.text = "Score: " + count.ToString();
		if(count >= numPickups){
			winText.text = "You win!";
		}
	}
	
	private void SetPlayerPositionText(){
		playerPositionText.text = "Player Position: " + transform.position.ToString("0.00");
	}
	
	private void SetPlayerVelocityText(){

		playerVelocityText.text = "Player Velocity: " + (CountDist(lastPosition, currentPosition) / Time.fixedDeltaTime).ToString("0.00");
		//playerVelocityText.text = "Player Velocity: " + (lastPosition.x - currentPosition.x).ToString("0.00");
	}

	private float CountDist(Vector3 v1, Vector3 v2){
		float dist = 0;
		dist = (float)Math.Pow((v1.x - v2.x)*(v1.x - v2.x) + (v1.z - v2.z)*((v1.z - v2.z)), 0.5);
		return dist;
	}

	void Start(){
		count = 0;
		winText.text = "";
		SetCountText();
		lastPosition = transform.position;
	}

    void OnMove (InputValue value) {
        moveValue = value.Get<Vector2>();
		SetPlayerPositionText();
		SetPlayerVelocityText();
    }

    void FixedUpdate () {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);

        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);

        lastPosition = currentPosition;
        currentPosition = transform.position;
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "PickUp") {
            other.gameObject.SetActive(false);
            count += 1;
			SetCountText();
        }
    }

}
