using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WiresMiniGame : MiniGame
{
    [SerializeField, Range(1, 10)] private int _wiresCount;

    [SerializeField] private Wire _leftWirePrefab;
    [SerializeField] private Wire _rightWirePrefab;

    private List<Wire> _leftWires;
    private List<Wire> _rightWires;
	private int _leftCount;

	private void Start()
	{
		_leftWires = new List<Wire>();
		_rightWires = new List<Wire>();

		ResetMiniGame();
		gameObject.SetActive(false);
	}

	public override void ResetMiniGame()
	{
		_leftCount = _wiresCount;
		foreach (var wire in _leftWires)
			Destroy(wire.gameObject);
		foreach (var wire in _rightWires)
			Destroy(wire.gameObject);
		_leftWires.Clear();
		_rightWires.Clear();

		for (int i = 0; i < _wiresCount; i++)
		{
			var leftWire = Instantiate(_leftWirePrefab, _leftWirePrefab.transform.parent);
			var rightWire = Instantiate(_rightWirePrefab);

			leftWire.gameObject.SetActive(true);
			rightWire.gameObject.SetActive(true);
			leftWire.OnWireConected += OnWireConected;
			rightWire.OnWireConected += OnWireConected;
			leftWire.Image.color = rightWire.Image.color = Random.ColorHSV();
			leftWire.Number = rightWire.Number = i;
			leftWire.gameObject.name = rightWire.gameObject.name = i.ToString();

			_leftWires.Add(leftWire);
			_rightWires.Add(rightWire);
		}
		_rightWires = _rightWires.OrderBy(x => Random.Range(0, 100)).ToList();
		foreach (var wire in _rightWires)
		{
			wire.transform.SetParent(_rightWirePrefab.transform.parent);
			wire.transform.localScale = Vector3.one;
		}

		OnPassed.RemoveAllListeners();
		OnDenied.RemoveAllListeners();
	}

	private void OnWireConected()
	{
		if (--_leftCount == 0)
			OnPassed?.Invoke();
	}
}
