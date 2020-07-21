using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int _health;
    private int _maxHealth = 25;
    public bool IsDead = false;

   
    private void Start()
    {
        _health = _maxHealth;
    }
    [SerializeField] private GameObject GameOver;
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Debug.Log("Die");
            GameOver.SetActive(true);
            Time.timeScale = 0;
            IsDead = true;
        }
    }
}
