using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeMiniGameUI : MonoBehaviour
{
    [field: SerializeField] public Button Ok { get; set; } 
    [field: SerializeField] public Button Cancel { get; set; } 
    [field: SerializeField] public Button[] Numbers { get; set; } 
    [field: SerializeField] public TMP_InputField CodeInput { get; set; } 
    [field: SerializeField] public TMP_Text CodeText { get; set; } 

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (Numbers != null)
            for (int i = 0; i < Numbers.Length; i++)
                Numbers[i].GetComponentInChildren<TMP_Text>().text = i.ToString();
    }

#endif
}
