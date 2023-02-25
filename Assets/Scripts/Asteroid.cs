using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField, Range(-250, 250)] private float _minMovementSpeed;
    [SerializeField, Range(-250, 250)] private float _maxMovementSpeed;
    [Header("Rotation")]
    [SerializeField, Range(-180, 180)] private float _minRotationSpeed;
    [SerializeField, Range(-180, 180)] private float _maxRotationSpeed;

    private Transform _planet;
    private float _distanceToPlanet;
    private float _movementSpeed;
    private float _rotationSpeed;
    private bool _isInOrbit;

	private void Start()
	{
		_movementSpeed = Random.Range(_minMovementSpeed, _maxMovementSpeed);
        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
	}

	private void Update()
	{
        if (!_isInOrbit)
            return;

        float actualSpeed = (_movementSpeed / _distanceToPlanet) * Time.deltaTime;
        transform.RotateAround(_planet.position, Vector3.forward, actualSpeed);
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
	}

    public void SetPlanet(Transform planet)
    {
        _planet = planet;
        _distanceToPlanet = Vector2.Distance(_planet.position, transform.position);
        _isInOrbit = true;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform == ShipController.Instance.transform)
            _isInOrbit = false;
	}
}
