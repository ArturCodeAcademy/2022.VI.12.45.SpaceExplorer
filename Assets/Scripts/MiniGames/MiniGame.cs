using UnityEngine;
using UnityEngine.Events;

public abstract class MiniGame : MonoBehaviour
{
    public UnityEvent OnPassed;
    public UnityEvent OnDenied;

    public abstract void ResetMiniGame();
}
