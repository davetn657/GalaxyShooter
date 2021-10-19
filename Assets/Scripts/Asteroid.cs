using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20;

    private SpawnManager _spawnManager;

    [SerializeField]
    private GameObject _explosion;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("The spawn manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Laser"))
        {
            _spawnManager.StartSpawning();
            //destroy laser prefab
            Destroy(other.gameObject);

            Instantiate(_explosion, transform.position, Quaternion.identity);

            //destroy this object after animation
            Destroy(this.gameObject);
        }
    }
}
