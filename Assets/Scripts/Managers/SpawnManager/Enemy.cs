using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player player;

    [SerializeField]
    private GameObject _explosion;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        if (player == null)
        {
            Debug.LogError("Player is null");
        }
    }
    // Update is called once per frame
    void Update()
    {
        float randomNum = Random.Range(-9, 9);

        transform.position += new Vector3(0, -1, 0) * _speed * Time.deltaTime;

        if (transform.position.y <= -5f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            
            if(player != null)
            {
                player.UpdateScore();
            }

            OnEnemyDeath();

            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            if(player != null)
            {
                player.Damage();
            }

            OnEnemyDeath();

            Destroy(this.gameObject);
        }
    }

    private void OnEnemyDeath()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        _speed = 0;
    }
}
