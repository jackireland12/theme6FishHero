using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private bool isDragging = false;
    private fishManager fishManager; // Reference to fishManager

    void Start()
    {
        cam = Camera.main;
        fishManager = fishManager.Instance; // Get singleton instance
    }

    void OnMouseDown()
    {
        if (fishManager.IsAttackMode()) return; // Disable dragging in Attack Mode
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mouseWorld.x, mouseWorld.y, 0);
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging && !fishManager.IsAttackMode())
        {
            Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mouseWorld.x, mouseWorld.y, 0) + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}
