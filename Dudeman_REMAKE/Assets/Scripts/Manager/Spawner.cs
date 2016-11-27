using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject CurrentProjectile;
    public GameObject projectileType;
    public float spawnTimer;
    bool spawning;

    void Start()
    {
        spawnTimer = 7.0f;
        if (CurrentProjectile == null)
            StartCoroutine(SpawnNew());
        spawning = false;
    }

    void Update()
    {
        if(!spawning)
        {
            StartCoroutine(SpawnNew());
        }
    }

    IEnumerator SpawnNew()
    {
        spawning = true;
        yield return new WaitForSeconds(spawnTimer);
        Instantiate(projectileType, this.gameObject.transform.position, this.gameObject.transform.rotation);
        spawning = false;
    }
}
