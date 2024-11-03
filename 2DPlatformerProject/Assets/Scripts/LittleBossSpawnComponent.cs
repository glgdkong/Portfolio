using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBossSpawnComponent : MonoBehaviour
{
    [SerializeField] private GameObject[] littleBoss;
    int randomNum;
    private Quaternion quaternion = Quaternion.identity;
    [SerializeField] private Transform spawnPosition;
    
    private void Start()
    {
        randomNum = Random.Range(0, littleBoss.Length);
    }
    private void OnDisable()
    {
        quaternion.y = (randomNum == 0 ? 180 : 0);
        Instantiate(littleBoss[0], spawnPosition.position, quaternion);
        quaternion.y = (randomNum == 1 ? 180 : 0);
        Instantiate(littleBoss[1], transform.position, quaternion);
    }
}
