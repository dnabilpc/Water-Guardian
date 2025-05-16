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
        if (tower == null)
        {
            GameObject towerPrefab = BuildManager.main.GetSelectedTower();
            Vector3 spawnPosition = transform.position + new Vector3(-4.6f, 0.5f, 0f); // misalnya
            tower = Instantiate(towerPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("Can't build there!");
        }
    }
}
