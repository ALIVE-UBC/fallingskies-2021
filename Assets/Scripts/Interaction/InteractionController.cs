using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class InteractionController : MonoBehaviour
{
    public InteractButtonController InteractButton;
    public InventoryWrapper PlayerInventory;
    public InventoryWrapper RecycleInventory;

    public GameObject GenericPopup = default;

    [Header("UI")]
    [SerializeField] GameObject _toast = default;
    [SerializeField] GameObject _computerScreen = default;
    [SerializeField] GameObject _microscopeScreen = default;
    [SerializeField] GameObject _submitScreen = default;

    private TMP_Text _toastText;

    private void Start()
    {
        _toastText = _toast.GetComponentInChildren<TMP_Text>();
    }

    public void ShowToast(string message)
    {
        StartCoroutine(ShowToastCoroutine(message));
    }

    public IEnumerator ShowToastCoroutine(string message)
    {
        _toastText.text = message;
        _toast.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        _toastText.text = "";
        _toast.gameObject.SetActive(false);
    }

    public void ShowComputerScreen()
    {
        RefreshAnalysisInventory();
        _computerScreen.SetActive(true);
        MetricsUploader.LogEvent(MetricEventType.LAB_ENTER, "Name", "Computer");
    }

    public void ShowMicroscopeScreen()
    {
        RefreshAnalysisInventory();
        _microscopeScreen.SetActive(true);
        MetricsUploader.LogEvent(MetricEventType.LAB_ENTER, "Name", "Microscope");
    }

    public void HideComputerScreen()
    {
        _computerScreen.SetActive(false);
        MetricsUploader.LogEvent(MetricEventType.LAB_EXIT, "Name", "Computer");
    }

    public void HideMicroscopeScreen()
    {
        _microscopeScreen.SetActive(false);
        MetricsUploader.LogEvent(MetricEventType.LAB_EXIT, "Name", "Microscope");
    }

    public void RefreshAnalysisInventory()
    {
        _computerScreen.GetComponentInChildren<InventoryUi>().PopulateCells(PlayerInventory.Inventory, InventoryType.Computer);
        _microscopeScreen.GetComponentInChildren<InventoryUi>().PopulateCells(PlayerInventory.Inventory, InventoryType.Microscope);
    }

    public void ShowSubmitScreen()
    {
        _submitScreen.SetActive(true);
        MetricsUploader.LogEvent(MetricEventType.ASSESSMENT_START);
    }
}
