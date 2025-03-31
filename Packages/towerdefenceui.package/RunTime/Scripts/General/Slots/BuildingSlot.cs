using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    public GameObject towerPrefab;
    private GameObject previewTower;

    private TextMeshProUGUI cost;
    private Image background;
    private Sprite TowerSprite;

    public int costForBuilding;
    private Camera mainCamera;

    public int rebuildCost;
    public int upgradeVal;

    public string buildingName = "";

    private bool canAfford = false;

    void Start()
    {
        mainCamera = Camera.main;
        cost = GetComponentInChildren<TextMeshProUGUI>();
        background = transform.Find("TowerBackground").GetComponent<Image>();

        cost.text = costForBuilding.ToString();

        //debug
        UpdateCostAmount(10000);
    }

    public void UpdateCostAmount(int _currentCoinAmount)
    {
        canAfford = _currentCoinAmount - costForBuilding > 0;
    }

    void Update()
    {
        if (!canAfford)
        {
            cost.color = Color.red;
            background.color = Color.red;
        }
        else
        {
            cost.color = Color.white;
            background.color = Color.yellow;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Instantiate a preview of the building
        if (canAfford)
        {
            previewTower = Instantiate(towerPrefab);

            previewTower.GetComponent<Collider>().enabled = false; // Disable collider while dragging

            //GamePlayUIController.Instance.BackButton();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (previewTower != null && canAfford)
        {
            // Convert screen position to world position
            Ray ray = mainCamera.ScreenPointToRay(eventData.position);

            // Perform a raycast but ignore the "Environment" layer
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                previewTower.transform.position = hit.point;

                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                previewTower.transform.rotation = rotation;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (previewTower != null && canAfford)
        {
            Ray ray = mainCamera.ScreenPointToRay(eventData.position);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                Instantiate(towerPrefab, hit.point, rotation);
            }

            Destroy(previewTower);
        }
    }

}
