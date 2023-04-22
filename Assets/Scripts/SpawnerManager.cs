using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject intObjGroup;
    public AudioSource gunAudioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObj", 0f, 10f);
    }

    void SpawnObj()
    {
        Instantiate(intObjGroup, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
    }
}
