using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public Transform gridCellPrefab;
    public Transform cube;
    public Transform onMousePrefab;
    public Vector3 smoothMousePos;

    [SerializeField] private int height;
    [SerializeField] private int width;
    private Node[,] nodes;
    private Plane plane;

    Vector3 mousePosition;

    void Start()
    {
        CreateGrid();
        plane = new Plane(Vector3.up, transform.position);
    }

    void Update()
    {
        GetMousePositionOnGrid();
    }

    void GetMousePositionOnGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out var enter))
        {
            mousePosition = ray.GetPoint(enter);
            smoothMousePos = mousePosition;
            mousePosition.y = 0;
            mousePosition = Vector3Int.RoundToInt(mousePosition);
            foreach(var node in nodes)
            {
                if(node.cellPosition==mousePosition&&node.isPlacable)
                {
                    if(Input.GetMouseButtonUp(0)&& onMousePrefab != null)
                    {
                        node.isPlacable = false;
                        onMousePrefab.GetComponent<ObjFollowMouse>().isOnGrid = true;
                        onMousePrefab.position = node.cellPosition + new Vector3(0, 0.5f, 0);
                        onMousePrefab = null;
                    }
                }
            }
        }
    }

    public void OnMouseClickOnUI()
    {
        if(onMousePrefab==null)
        {
            onMousePrefab = Instantiate(cube, mousePosition, Quaternion.identity);
        }
    }

    private void CreateGrid()
    {
        nodes = new Node[width, height];
        var name = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 worldPosition = new Vector3(i, 0, j);
                Transform obj = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity);
                obj.name = "Cell " + name;
                nodes[i, j] = new Node(true, worldPosition, obj);
                name++;
            }
        }
    }
}

public class Node
{
    public bool isPlacable;
    public Vector3 cellPosition;
    public Transform obj;

    public Node(bool isPlacable, Vector3 cellPosition, Transform obj)
    {
        this.isPlacable = isPlacable;
        this.cellPosition = cellPosition;
        this.obj = obj;
    }
}
