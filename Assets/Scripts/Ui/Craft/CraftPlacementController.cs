using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPlacementController : MonoBehaviour
{
    [SerializeField] private GameObject placementPrefab;
    private GameObject placementObject;
    private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private SurvivalWorldData survivalWorldData;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void PreviewCraft(GameObject prefab)
    {
        if(prefab == null)
            return;
        
        placementPrefab = prefab;
        placementObject = Instantiate(placementPrefab);
    }

    public void EndPreview()
    {
        Destroy(placementObject);
        placementObject = null;
    }

    void CraftBuilding(Vector3 position)
    {
        Instantiate(placementPrefab, position, Quaternion.identity);
        survivalWorldData.AddObject(new SurvivalWorldData.ObjectData(position, placementPrefab));
        EndPreview();
    }
    
    private void Update()
    {
        if (placementObject == null)
            return;
        
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 20f, groundLayer, QueryTriggerInteraction.Ignore))
        {
            placementObject.transform.position = hit.point;
        }

        if (Input.GetMouseButtonDown(0))
        {
            CraftBuilding(hit.point);
        }
    }
}
