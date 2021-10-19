using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab, _enemyContainer;
    [SerializeField]
    private GameObject[] powerUps;
    [SerializeField]
    private float _waitTime = 0.5f;
    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2.5f);

        while(_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-10f, 10f), 6f, 0f), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_waitTime);
        }
    }

    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(2.5f);

        while (_stopSpawning == false)
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerUp], new Vector3(Random.Range(-10f, 10f), 6f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(8f, 13f));
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
