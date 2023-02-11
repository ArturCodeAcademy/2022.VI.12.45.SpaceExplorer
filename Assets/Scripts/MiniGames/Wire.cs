using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wire : MonoBehaviour, IPointerDownHandler
{
	public event Action OnWireConected;

	public Image Image { get => _image ??= GetComponent<Image>(); private set => _image = value; }
	public int Number { get; set; }

	private Outline _outline;
	private Image _image;

	private static Wire _selectedWire = null;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (_selectedWire == null)
		{
			_selectedWire = this;
			_outline = gameObject.AddComponent<Outline>();
			_outline.effectDistance = new Vector2(5, 5);
			return;
		}

		if (_selectedWire.gameObject != gameObject && _selectedWire.Number == Number)
		{
			gameObject.SetActive(false);
			_selectedWire.gameObject.SetActive(false);
			OnWireConected?.Invoke();
		}
		else
		{
			Destroy(_selectedWire._outline);
		}
		_selectedWire = null;
	}
}
