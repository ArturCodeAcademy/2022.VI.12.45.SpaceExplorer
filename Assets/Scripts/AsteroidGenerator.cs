using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] private int _beltAsteroidCount;
    [SerializeField] private float _beltDistanceFromPlanet;
    [SerializeField] private float _spawnOffset;
    [SerializeField] private int _beltCount;
    [SerializeField] private Transform _planetOrbit;
    [SerializeField] private GameObject[] _asteroidPrefabs;
    [SerializeField] private Transform _asteroidPoolParent;

	private void Start()
	{
        GenerateAsteroidBelt();
	}

	private void GenerateAsteroidBelt()
	{
		for (int beltIndex = 0; beltIndex < _beltCount; beltIndex++)
		{
			List<Vector2> positions = GetAsteroidBeltPositions();
			for (int i = 0; i < _beltAsteroidCount; i++)
			{
				GameObject prefab = _asteroidPrefabs[Random.Range(0, _asteroidPrefabs.Length)];
				Transform asteroid = Instantiate(prefab, _asteroidPoolParent).transform;
				float size = Random.Range(0.5f, 2f);
				asteroid.localScale = Vector2.one * size;
				asteroid.position = positions[i];
			}
			_beltDistanceFromPlanet *= 2;
		}
	}

	private List<Vector2> GetAsteroidBeltPositions()
	{
		List<Vector2> positions = new List<Vector2>();

		for (int i = 0; i < _beltAsteroidCount; i++)
		{
			float angle = i * 360f / _beltAsteroidCount;
			Vector2 direction = Quaternion.Euler(Vector3.forward * angle) * Vector2.right;
			Vector2 offset = Random.insideUnitCircle * _spawnOffset;
			Vector2 position = (Vector2)_planetOrbit.position + direction * _beltDistanceFromPlanet + offset;
			positions.Add(position);
		}

		return positions;
	}
}
