
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float range;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float lowestPoint, highestPoint;
    public float min, max;

    private void Start()
    {
    }

    public void Spawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(prefab, player.transform.position + GetRandomPos(), Quaternion.identity);

        }
    }
    private Vector3 GetRandomPos()
    {
        return new Vector3(RandomRange(range), Mathf.Lerp(lowestPoint, highestPoint, Random.value), RandomRange(range));
    }

    private float RandomRange(float value)
    {

        return Mathf.Lerp(-value, value, Random.value);
    }
}
