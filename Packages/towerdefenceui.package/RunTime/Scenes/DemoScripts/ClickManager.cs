using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    private BuildingInformationManager buildingInformationManager;

    private void Awake()
    {
        buildingInformationManager = FindObjectOfType<BuildingInformationManager>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.CompareTag("Tower"))
                {
                    ExampleTower tower = clickedObject.GetComponent<ExampleTower>();
                    buildingInformationManager.SelectNewTower(tower.name, tower.rebuildcost, tower.upgradeValue, true);
                    UIManager.Instance.OpenDetailsMenu();
                }
            }
        }
    }
}
