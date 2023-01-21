using UnityEngine;

public class ShipController : MonoBehaviour
{
    public static ShipController Instance { get; private set; }

    public bool Moving => _input != Vector2.zero;
    public float Magnitude => _force.magnitude;
 
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _boostSpeed;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private Vector2 _input;
    private Vector2 _force;
    private bool _canMove;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ThrustForward()
    {
        _force = transform.up * _input.y * _speedMultiplier;
        if (Input.GetKey(KeyCode.LeftShift))
            _rigidbody.drag = _speedMultiplier / _boostSpeed;
        else
            _rigidbody.drag = _speedMultiplier / _maxSpeed;
        _rigidbody.AddForce(_force, ForceMode2D.Force);
    }

    private void RotateShip()
    {
        transform.Rotate(Vector3.forward * _input.x * _speedMultiplier);
    }

    private void Update()
    {
        _input = new Vector2
            (
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            );
    }

    private void FixedUpdate()
    {
        ThrustForward();
        RotateShip();
    }
}
