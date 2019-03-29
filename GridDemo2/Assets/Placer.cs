using UnityEngine;

public class Placer : MonoBehaviour
{
    private Grid grid;
    public GameObject rawPrefab;
    private Collider[] colliders;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Update()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceCubeNear(hitInfo.point);
            }
        }else if (Input.GetKeyDown("d"))
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                clearTile(hitInfo.point);
            }
        }

    }

    private void clearTile(Vector3 clickPoint)
    {

        print("d pressed");

        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);

        if (isCollidingWithOutherGO(finalPosition)) { 

            foreach (var collider in colliders)
            {
                var go = collider.gameObject;
                print(go.tag);
                if (go == gameObject) continue;
                if(go.tag == "Tile")
                {
                    Destroy(go.transform.root.gameObject);
                }
            }
        }

    }


    private bool isCollidingWithOutherGO(Vector3 finalPosition)
    {
        return ((colliders = Physics.OverlapSphere(finalPosition, 0.5f)).Length > 1);
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {

        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);

        if ((colliders = Physics.OverlapSphere(finalPosition, 0.5f)).Length > 1)
        {
            
            foreach (var collider in colliders)
            {
                print(collider);
                var go = collider.gameObject;
                print("in");
                if (go == gameObject) continue;
                return;
                                                
            }
        }

        finalPosition.y = 0;

        if(rawPrefab!= null)
        {
            Instantiate(rawPrefab).transform.position = finalPosition;
           
        }
    }
}