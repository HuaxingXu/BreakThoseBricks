﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public Paddle paddle;
	public bool gameStarted = false;
	private Vector3 paddleVector;

	//Make the min and max speed to be configurable in the editor.
	public float MinimumSpeed = 10;
	public float MaximumSpeed = 20;
	
	//To prevent the ball from keep bouncing horizontally we enforce a minimum vertical movement
	public float MinimumVerticalMovement = 0.5F;


	// Use this for initialization
	void Start () {
		//Set the ball on the paddle position
		paddleVector = this.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameStarted){
			this.transform.position = paddle.transform.position + paddleVector;
				if(Input.GetMouseButtonDown(0)){
					print("Mouse clicked!");
					gameStarted = true;
					this.GetComponent<Rigidbody2D>().velocity = new Vector2 (Random.Range(-2.0f, 2.0f),10f);
					
					//enable Trail Renderer
					//TrailRenderer trailTime = this.GetComponent<TrailRenderer>();
					//trailTime.time = 0;
					//this.GetComponent<TrailRenderer>().time = trailTime.time;
					this.GetComponent<TrailRenderer>().enabled = true;
				}
			}
		launchBall ();
	}

	//private void OnTriggerEnter2D(Collider2D other)
	/*void OnTriggerEnter2D (Collider2D other)
	{
		print (other.name);
		if (other.name == "Lose")
		{
			print ("Ball Destroyed!");
			Destroy(this.gameObject);
		}
	}*/

	public void launchBall() {
		//Get current speed and direction
		Vector2 direction = GetComponent<Rigidbody2D>().velocity;
		//float speed = 20f;
		float speed = direction.magnitude;
		direction.Normalize();
		
		//Make sure the ball never goes straight horizotal else it could never come down to the paddle.
		//if (direction.z > -MinimumVerticalMovement && direction.z < MinimumVerticalMovement)
		if (direction.x > -MinimumVerticalMovement && direction.x < MinimumVerticalMovement)
		{
			//We will use the Ternary Operator for our conditional. First is boolean, followed by true statement
			//Then false statement
			//Adjust the y, make sure it keeps going into the direction it was going (up or down)
			//direction.z = direction.z < 0 ? -MinimumVerticalMovement : MinimumVerticalMovement;
			direction.x = direction.x < 0 ? -MinimumVerticalMovement : MinimumVerticalMovement;

			//direction.x = Random.Range(-1.0f, 1.0f);
			//Adjust the x also as x + y = 1
			direction.y = direction.y < 0 ? -1 + MinimumVerticalMovement : 1 - MinimumVerticalMovement;

			//print(direction.x);

			//Apply it back to the ball
			GetComponent<Rigidbody2D>().velocity = direction * speed;   
		}
		
		if (speed < MinimumSpeed || speed > MaximumSpeed)
		{
			//Limit the speed so it always above min en below max
			speed = Mathf.Clamp(speed, MinimumSpeed, MaximumSpeed);

			//Apply the limit
			//Note that we don't use * Time.deltaTime here since we set the velocity once, not every frame.
			GetComponent<Rigidbody2D>().velocity = direction * speed;   
		}
	
	}
}