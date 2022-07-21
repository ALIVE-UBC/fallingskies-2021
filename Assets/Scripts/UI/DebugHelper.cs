using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fungus;

public class DebugHelper : MonoBehaviour
{
    [SerializeField] TMP_Text _infoPanel = default;
    [SerializeField] GameObject[] _gameObjectsToHide = default;

    [Header("Teleport")]
    [SerializeField] Transform _waypoints;
    [SerializeField] TMP_Dropdown _waypointsDropdown;

    [Header("Dialogue")]
    [SerializeField] private TMP_Dropdown _flowchartsDropdown;

    [Header("User Group")]
    [SerializeField] private TMP_Dropdown _userGroupDropdown;

    private GameObject _player;
    private UserManager _umgr;
    private Dictionary<string, Vector3> _waypointConfig = new Dictionary<string, Vector3>();
    private List<string> _flowchartNames = new List<string>();

    private void Awake()
    {
        _umgr = FindObjectOfType<UserManager>();
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        HideGameObjects();
        InitWaypoints();
        InitFlowcharts();
    }

    private void Update()
    {
        UpdateInfoPanel();
    }

    private void HideGameObjects()
    {
        foreach (GameObject g in _gameObjectsToHide)
        {
            g.SetActive(false);
        }
    }

    private void UpdateInfoPanel()
    {
        string info = "";
        foreach (var de in _umgr.Data)
        {
            info += $"{de.Key}={de.Value} ({de.Value.GetType()})\n";
        }

        _infoPanel.text = info;
    }

    private void InitWaypoints()
    {
        foreach (Transform wp in _waypoints)
        {
            _waypointConfig.Add(wp.name, wp.position);
        }

        _waypointsDropdown.ClearOptions();
        _waypointsDropdown.AddOptions(new List<string>(_waypointConfig.Keys));
    }

    private void InitFlowcharts()
    {
        var flowcharts = FindObjectsOfType<Flowchart>();
        foreach (Flowchart flowchart in flowcharts)
        {
            // only add flowcharts with names starting with a digit
            if (char.IsDigit(flowchart.name[0]))
            {
                _flowchartNames.Add(flowchart.name);
            }
        }

        _flowchartNames.Sort();

        _flowchartsDropdown.ClearOptions();
        _flowchartsDropdown.AddOptions(_flowchartNames);
    }

    public void Teleport(Vector3 pos)
    {
        _player.transform.position = pos;
    }

    public void Teleport()
    {
        Teleport(Vector3.zero);
    }

    public void TeleportDropdown()
    {
        string wpName = _waypointsDropdown.options[_waypointsDropdown.value].text;
        Vector3 pos = _waypointConfig[wpName];
        Teleport(pos);
    }

    public void TriggerDialogue()
    {
        string dName = _flowchartsDropdown.options[_flowchartsDropdown.value].text;
        //Flowchart f = _flowchartConfig[dName];
        //f.ExecuteBlock("Start");
        Flowchart.BroadcastFungusMessage(dName);
    }

    public void SetUserGroup()
    {
        // user group = 1, 2, 3
        // dropdown index = 0, 1, 2
        string userGroup = (_userGroupDropdown.value + 1).ToString();
        _umgr.Write("UserGroup", userGroup);
    }
}
