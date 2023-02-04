using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CodeMiniGameUI))]
public class CodeMiniGame : MiniGame
{
    [SerializeField, Range(1, MAX_CODE_LENGTH)] private int _codeLength = 7;

    private CodeMiniGameUI _codeMiniGameUI;
    private string _code = string.Empty;
    private string _inputCode = string.Empty;

    private const int MAX_CODE_LENGTH = 10;

    private void Start()
    {
        _codeMiniGameUI = GetComponent<CodeMiniGameUI>();
        GenerateNewCode();
        SetUpButtons();
        gameObject.SetActive(false);
    }

    public override void ResetMiniGame()
    {
        GenerateNewCode();
        _inputCode = string.Empty;
        _codeMiniGameUI.CodeInput.text = string.Empty;
        OnPassed.RemoveAllListeners();
        OnDenied.RemoveAllListeners();
    }

    public void GenerateNewCode()
    {
        _code = string.Empty;
        for (int i = 0; i < _codeLength; i++)
            _code += Random.Range(0, 10);

        _codeMiniGameUI.CodeText.text = _code;
    }

    private void SetUpButtons()
    {
        for (int i = 0; i < _codeMiniGameUI.Numbers.Length; i++)
        {
            string number = $"{i}";
            _codeMiniGameUI.Numbers[i].onClick.AddListener
            (
                delegate ()
                {
                    if (_inputCode.Length < _codeLength)
                    {
                        _inputCode += number;
                        _codeMiniGameUI.CodeInput.text = _inputCode;
                    }
                }
            );
        }

        _codeMiniGameUI.Ok.onClick.AddListener
        (
            delegate ()
            {
                if (_code == _inputCode)
                    OnPassed?.Invoke();
                else
                    OnDenied?.Invoke();

                _inputCode = string.Empty;
                _codeMiniGameUI.CodeInput.text = string.Empty;
            }
        );

        _codeMiniGameUI.Cancel.onClick.AddListener
        (
            delegate ()
            {
                _inputCode = string.Empty;
                _codeMiniGameUI.CodeInput.text = string.Empty;
            }
        );
    }
}
