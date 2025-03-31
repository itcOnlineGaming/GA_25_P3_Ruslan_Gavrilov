using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInformationManager : MonoBehaviour
{
    private TextMeshProUGUI buildingName;
    private Slider healthSlider;
    private Image towerImage;
    private GameObject costContainer;
    private GameObject upgradeContainer;
    private GameObject coinImageInUpgradeContainer;
    private int rebuildCostValue;
    private int upgradeCostValue;

    private bool canAfford = false;

    private void Awake()
    {
        buildingName = transform.Find("TowerImage").GetComponentInChildren<TextMeshProUGUI>();
        costContainer = transform.Find("CostButton/CostContainer").gameObject;
        upgradeContainer = transform.Find("CostButton (1)/CostContainer").gameObject;
        coinImageInUpgradeContainer = transform.Find("CostButton (1)/CostContainer/CoinIcon").gameObject;
        healthSlider = GetComponentInChildren<Slider>();
        towerImage = transform.Find("TowerImage/Background/TowerImage").GetComponentInChildren<Image>();
    }


    public void SelectNewTower(string TowerType, int rebuildCost, int upgradeVal, bool afford)
    {
        // Update UI Elements (Static Information)
        buildingName.text = TowerType;
        rebuildCostValue = rebuildCost;
        upgradeCostValue = upgradeVal;
        costContainer.GetComponentInChildren<TextMeshProUGUI>().text = upgradeCostValue.ToString();
        upgradeContainer.GetComponentInChildren<TextMeshProUGUI>().text = rebuildCostValue.ToString();
        canAfford = afford;

        UpdateUI();
    }

    private void UpdateTowerRepairCost()
    {
        if (!canAfford)
        {
            costContainer.GetComponent<Image>().color = Color.red;

        }
        else
        {
            costContainer.GetComponent<Image>().color = Color.green;
        }
    }

    private void UpdateTowerUpgradeCost()
    {
        if (!canAfford)
        {
            upgradeContainer.GetComponent<Image>().color = Color.red;

        }
        else
        {
            upgradeContainer.GetComponent<Image>().color = Color.green;
        }
    }

    public void UpdateUI()
    {
        //int currentHealth = currentTower.GetCurrentHealth();
        //int maxHealth = currentTower.GetTowerData().MaxHealth;
        //healthSlider.value = (float)currentHealth / maxHealth;
        //healthSlider.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = $"{currentHealth}/{maxHealth}";

        UpdateTowerRepairCost();
        UpdateTowerUpgradeCost();
    }
}
