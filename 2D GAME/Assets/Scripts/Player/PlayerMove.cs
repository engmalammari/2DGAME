using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	[SerializeField] private float MoveSpeed;
	[SerializeField] private float JumpScale;
	[SerializeField] private float Move;
	[SerializeField] private Transform GroundCheck;
	[SerializeField] private float CheckHeiht;
	[SerializeField] private LayerMask Ground;
	[SerializeField] private int Jump;
	[SerializeField] private int VolOfJump;
	private bool isGrounded;
	private bool facingRight = true;
	private Rigidbody2D Player;

	private void Start()
	{
		Jump = VolOfJump;
		Player = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (isGrounded == true)
		{
			Jump = VolOfJump;
		}

		if (Input.GetKey(KeyCode.W) && Jump > 0)
		{
			Player.velocity = Vector2.up * JumpScale;
			Jump--;
		}
		else if (Input.GetKey(KeyCode.W) && Jump == 0 && isGrounded == true)
		{
			Player.velocity = Vector2.up * JumpScale;
		}

	}

	private void FixedUpdate()
	{
		isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckHeiht, Ground);
		Move = Input.GetAxis("Horizontal");
		Player.velocity = new Vector2(Move * MoveSpeed, Player.velocity.y);
		if (facingRight == false && Move > 0)
		{
			Flip();
		}
		else if (facingRight == true && Move < 0)
		{
			Flip();
		}
	}

	private void Flip()
	{
		facingRight = !facingRight;
		Vector3 Scaler = transform.localScale;
		Scaler.x *= -1;
		transform.localScale = Scaler;
	}
	[SerializeField] private GameObject GameOver;
	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.name == "Death")
		{
			GameOver.SetActive(true);
			Time.timeScale = 0;

		}
	}


}


