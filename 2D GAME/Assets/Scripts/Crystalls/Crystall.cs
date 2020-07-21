using UnityEngine;

public class Crystall : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.GetComponent<PlayerMove>())
		{
			CrystalManager.Instance.Crystalls.Add(this);
			Destroy(this.gameObject);
		}
	}
}
