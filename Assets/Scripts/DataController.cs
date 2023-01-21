using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class DataController : MonoBehaviour
{
    private SatelliteInfo satelliteInfo;

    private void Start()
    {
        StartCoroutine(CollectData());
    }

    private IEnumerator CollectData()
    {
        SatelliteInfo satelliteInfo = default;
        WaitUntil wait = new WaitUntil(() => DataRequester.Instance.TryGetData(out satelliteInfo));
        yield return wait;
        SatelliteGeneration.instance.GenerateSatellites(satelliteInfo);
    }
}
