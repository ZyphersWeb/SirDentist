using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EggProjectileSpawner : MonoBehaviour
{
    public GameObject EggPrefab;

    public float spawnDist = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timeDelay());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FireShot(){
        Instantiate(EggPrefab, new Vector3(transform.position.x - spawnDist, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(EggPrefab, new Vector3(transform.position.x + spawnDist, transform.position.y, transform.position.z), Quaternion.identity);
    }

    IEnumerator timeDelay(){
        while(true){
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            FireShot();
        }
    }
}

