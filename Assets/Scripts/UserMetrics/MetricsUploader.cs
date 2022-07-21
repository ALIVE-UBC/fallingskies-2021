using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[JsonConverter(typeof(StringEnumConverter))]
public enum MetricEventType
{
    // ReSharper disable InconsistentNaming
    UNKNOWN,

    GAME_START,
    GAME_END,

    INSPECT,
    TALK,

    BACKPACK_ADD,
    BACKPACK_DISCARD,

    ZONE_ENTER,
    ZONE_EXIT,

    LAB_ENTER,
    LAB_TEST,
    LAB_EXIT,

    SCRATCHPAD_OPEN,
    SCRATCHPAD_UPDATE,
    SCRATCHPAD_CLOSE,

    KIOSK_OPEN,
    KIOSK_VIEW,
    KIOSK_CLOSE,

    ASSESSMENT_START,
    ASSESSMENT_UPDATE,
    ASSESSMENT_END,
    // ReSharper restore InconsistentNaming
}

public class MetricEvent
{
    public int UserId;
    public long Timestamp;
    public MetricEventType Type;
    public Dictionary<string, object> Params;

    public MetricEvent(int userId, MetricEventType metricEventType, Dictionary<string, object> metricEventParams = null)
    {
        UserId = userId;
        Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        Type = metricEventType;
        Params = metricEventParams ?? new Dictionary<string, object>();
    }
}

public class MetricsUploader : MonoBehaviour
{
    private const string EventApiUrl = "https://alive.educ.ubc.ca/fsd/metrics/event/";
    private static readonly HttpClient Client = new HttpClient();

    private static int GetUserId()
    {
        UserManager userManager = FindObjectOfType<UserManager>();
        int userId = int.Parse((string) userManager.Read("UserId", "0"));
        return userId;
    }

    // Log a metric event and send it to the server.
    public static void LogEvent(MetricEventType metricEventType, Dictionary<string, object> metricEventParams = null)
    {
        int userId = GetUserId();
        var metricEvent = new MetricEvent(userId, metricEventType, metricEventParams);
        SerializeAndSendEvent(metricEvent);
    }

    public static void LogEvent(MetricEventType metricEventType, string key, object value)
    { 
        var metricEventParams = new Dictionary<string, object>();
        metricEventParams.Add(key, value);
        LogEvent(metricEventType, metricEventParams);
    }

    public static void LogEvent(MetricEventType metricEventType, string key1, object value1, string key2, object value2)
    { 
        var metricEventParams = new Dictionary<string, object>();
        metricEventParams.Add(key1, value1);
        metricEventParams.Add(key2, value2);
        LogEvent(metricEventType, metricEventParams);
    }

    private static async void SerializeAndSendEvent(MetricEvent metricEvent)
    {
        // Serialize MetricEvent into JSON.
        string jsonText = JsonConvert.SerializeObject(metricEvent);
        print(jsonText);

        // Send JSON over HTTP.
        var resp = await Client.PutAsync(EventApiUrl, new StringContent(jsonText));

        // Verify if the return code is HTTP 201.
        if (resp.StatusCode != HttpStatusCode.Created)
        {
            string errorText = await resp.Content.ReadAsStringAsync();
            Debug.LogError($"HTTP Error {resp.StatusCode}: {errorText}");
        }
    }
}
