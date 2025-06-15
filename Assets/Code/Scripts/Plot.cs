using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        //Debug.Log("Mouse Entered");
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        //Debug.Log("Mouse Exited");
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        //Debug.Log("Mouse Clicked");
        if (tower != null) return;
            Tower towerPrefab = BuildManager.main.GetSelectedTower();
            Vector3 spawnPosition = transform.position + new Vector3(-4.6f, 0.5f, 0f); // misalnya
            
        if(towerPrefab.cost > LevelManager.main.currency)
        {
            Debug.Log("Not enough currency");
            return;
        }
        LevelManager.main.SpendCurrency(towerPrefab.cost);
        tower = Instantiate(towerPrefab.prefab, spawnPosition, Quaternion.identity);
        
    }
}
