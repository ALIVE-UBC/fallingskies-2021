using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] _players;

    private UserManager _umgr;

    private void Awake()
    {
        _umgr = FindObjectOfType<UserManager>();
    }

    private void Start()
    {
        int cId = (int) _umgr.Read("CharacterId", 4);

        GameObject player = _players[cId - 1];
        foreach (GameObject p in _players)
        {
            if (p == player)
            {
                p.SetActive(true);
            }
            else
            {
                p.SetActive(false);
            }
        }

        // metrics
        MetricsUploader.LogEvent(MetricEventType.GAME_START, "CharacterId", cId, "UserGroup", _umgr.Read("UserGroup", "-1"));
    }
}
