using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	private int _damage = 5;
	private Transform _player;
	private float _speed = 0.8f;
	private void Start()
	{
		_player = FindObjectOfType<PlayerStats>().transform;
	}
	private void Update()
	{

		transform.position += _player.position * _speed * Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.GetComponent<PlayerStats>())
		{
			Debug.Log("Damage");
			var player = other.gameObject.GetComponent<PlayerStats>();
			player.TakeDamage(_damage);
			Destroy(gameObject);
		}
	}
}
