using Pathfinding;
using UnityEngine;


public enum ClickMode
{
    Mode2D,
    Mode3D
}
public class ClickToMoveAgent : MonoBehaviour
{
    [SerializeField] private ClickMode clickMode;
    [SerializeField] private LayerMask raycastLayers;
    [SerializeField] private Seeker seekerAI;

    public void Awake()
    {
        //safe is to deliberately throw an error about the seeker being errored 
        if (seekerAI == null) seekerAI = GetComponent<Seeker>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            switch (clickMode)
            {
                case ClickMode.Mode2D:
                    Raycast2D(ray);
                    break;
                case ClickMode.Mode3D:
                    Raycast3D(ray);
                    break;
            }
        }
    }

    private void Raycast2D(Ray ray)
    {
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(ray, Mathf.Infinity, raycastLayers);
        if (hitInfo.collider != null)
        {
            seekerAI.StartPath(transform.position, hitInfo.point, OnPathComplete);
            //callback?
        }
    }
    private void Raycast3D(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, raycastLayers))
        {
            seekerAI.StartPath(transform.position, hitInfo.point, OnPathComplete);
        }
    }

    private void OnPathComplete(Path path)
    {
        Debug.Log($"Path calculation compete. Any errors? - {path.error}");
    }
}

