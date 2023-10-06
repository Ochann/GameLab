using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

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
	
	private void SetCountText(){
		scoreText.text = "Score: " + count.ToString();
		if(count >= numPickups){
			winText.text = "You win!";
		}
	}
	
	private void SetPlayerPositionText(){
		playerPositionText.text = "Player Position: " + transform.position.ToString("0.00");
	}
	
	void Start(){
		count = 0;
		winText.text = "";
		SetCountText();
	}

    void OnMove (InputValue value) {
        moveValue = value.Get<Vector2>();
		SetPlayerPositionText();
    }

    void FixedUpdate () {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);

        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "PickUp") {
            other.gameObject.SetActive(false);
            count += 1;
			SetCountText();
        }
    }

}
