using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _optionsPanel;

    public void OnPlayClick()
    {
		SceneManager.LoadScene(1);
    }

	public void OnOptionsClick()
	{
		_optionsPanel.SetActive(true);
	}

	public void OnExitClick()
	{
		Application.Quit();
	}
}
