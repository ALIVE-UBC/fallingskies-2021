using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct PlayerPortrait
{
    public int CharacterId;
    public Sprite Potrait;
}

public class MainHud : MonoBehaviour
{
    [SerializeField] private Image _playerPortrait;
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _zoneName;
    [SerializeField] private GameObject _tutorial;

    public PlayerPortrait[] PlayerPortraits;

    private UserManager _userManager;
    private GameObject _player;
    private int _zoningLayer = 8;

    void Start()
    {
        _userManager = FindObjectOfType<UserManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        UpdatePlayerInfo();
    }

    void Update()
    {
        UpdateZoneInfo();
    }

    public void ShowTutorial()
    {
        _tutorial.SetActive(true);
    }

    void UpdatePlayerInfo()
    {
        // Update player portrait. Default to potrait 3.
        int cId = (int) _userManager.Read("CharacterId", 3);
        foreach (PlayerPortrait pp in PlayerPortraits)
        {
            if (pp.CharacterId == cId)
            {
                _playerPortrait.sprite = pp.Potrait;
                break;
            }
        }

        // Update player name.
        _playerName.text = (string) _userManager.Read("AvatarName", "Player Name");
    }

    void UpdateZoneInfo() {
        Ray ray = new Ray(_player.transform.position, Vector3.down);
        int layerMask = 1 << _zoningLayer;  // hit only the Layer 8 (Zoning)
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask);

        // Update zone only if it differs from origZone
        string origZone = _zoneName.text;
        string currentZone = hit.collider ? hit.collider.name : "";
        if (origZone != currentZone)
        {
            _zoneName.text = currentZone;
            // from "" to "some zone"
            if (origZone == "")
            {
                MetricsUploader.LogEvent(MetricEventType.ZONE_ENTER, "Name", currentZone);
            }
            // from "some zone" to ""
            else if (currentZone == "")
            {
                MetricsUploader.LogEvent(MetricEventType.ZONE_EXIT, "Name", origZone);
            }
            // from "some zone" to "another zone"
            else
            {
                MetricsUploader.LogEvent(MetricEventType.ZONE_EXIT, "Name", origZone);
                MetricsUploader.LogEvent(MetricEventType.ZONE_ENTER, "Name", currentZone);
            }
        }
    }
}
