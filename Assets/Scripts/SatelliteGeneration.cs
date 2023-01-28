using System.Collections.Generic;
using UnityEngine;

public class SatelliteGeneration : MonoBehaviour
{
    [SerializeField] private GameObject satellitePrefab;

    [SerializeField] private Transform orbitCenter;
    [SerializeField] private List<Transform> activeSatellites = new List<Transform>();
    [SerializeField, Min(0)] private float _maxSpawnDistance;

    public static SatelliteGeneration instance;

    private void Awake()
    {
        instance = this;
    }

    public void GenerateSatellites(SatelliteInfo satelliteInfo)
    {
        int satelliteCount = satelliteInfo.above.Length;

        for (int i = 0; i < satelliteCount; i++)
        {
            float distanceAroundCircleInRads = i * 2 * Mathf.PI / satelliteCount;
            float verticalDirection = Mathf.Sin(distanceAroundCircleInRads);
            float horizontalDirection = Mathf.Cos(distanceAroundCircleInRads);
            Vector2 direction = new Vector2(horizontalDirection, verticalDirection);
            Vector2 spawnPosition = satelliteInfo.above[i].satalt / 100 * direction;

            if (spawnPosition.magnitude > _maxSpawnDistance)
                continue;

            GameObject satellite = Instantiate(satellitePrefab, spawnPosition, Quaternion.identity);
            activeSatellites.Add(satellite.transform);
        }

        QuestController.Instance.SetSatellites(activeSatellites);
    }
}
