using System.Net.Http;
using UnityEngine;

public class DataRequester : MonoBehaviour
{
    public static DataRequester Instance { get; private set; }

    private const string API_KEY = "QJTFU5-FF9RVN-2SJJ42-4YUA";
    private const string BASE_API_URI = "https://api.n2yo.com/rest/v1/satellite/above/{0}/{1}/{2}/{3}/{4}/&apiKey={5}";
    private string _requestURL = string.Empty; 
    private string _response = string.Empty;
    private HttpClient _client = new HttpClient();

    private const float LAT = 54.6872f;
    private const float LONG = 25.2797f;
    private const float ALT = 0;
    private const float RADIUS = 60;
    private const int CAT = 30;

    private SatelliteInfo _satelliteInfo;

    private void Awake()
    {
        Instance = this;
    }

    private async void Start()
    {
        _requestURL = string.Format(BASE_API_URI, LAT, LONG, ALT, RADIUS, CAT, API_KEY);
        string json = await _client.GetStringAsync(_requestURL);
        _satelliteInfo = JsonUtility.FromJson<SatelliteInfo>(json);
    }

    public bool TryGetData(out SatelliteInfo satelliteInfo)
    {
        satelliteInfo = _satelliteInfo;
        if (_satelliteInfo == null)
            return false;     
        return true;
    }
}
