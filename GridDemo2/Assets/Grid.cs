using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private int size = 1;

    public int fieldSize = 40;

    public GameObject[] wallPrefabs;
    public GameObject[] cornerWallPrefabs;
    int wallLenght = 0;
    public GameObject hitBoxPrefab;


    private void Awake()
    {
        wallLenght = fieldSize - size;
        float backgroundImageSize = wallLenght / 5;
        createWalls();
        drawHitCubes();
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < fieldSize; x += size)
        {           
            for (float z = 0; z < fieldSize; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));                
                    Gizmos.DrawSphere(point, 0.1f);                             
            }
        }      
    }

    private void drawHitCubes()
    {
        for (float x = 0; x < fieldSize; x += size)
        {
            for (float z = 0; z < fieldSize; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                GameObject hitCube = Instantiate(hitBoxPrefab);
                Vector3 pointToPlaceHitcube = new Vector3(point.x, point.y-0.5f*size,point.z);
                hitCube.transform.position = pointToPlaceHitcube;
                hitCube.gameObject.transform.localScale = new Vector3(size, size, size);
            }
        }
    }

        private void createWalls()
    {

        int wallLenght = fieldSize - size;

        for (float x = 0; x < fieldSize; x += size)
        {
            for (float z = 0; z < fieldSize; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));

                if(z == 0)
                {
                    if (x != 0 && x != wallLenght)
                    {
                        Instantiate(getRandomWallAsset(), new Vector3(x, 0f, z - 0.5f * size), Quaternion.Euler(0, -90, 0));
                    }
                    else
                    {
                        if (x == wallLenght)
                        {
                            Instantiate(getRamdomCornerAsset(), new Vector3(x + 0.5f * size, 0f, z), Quaternion.Euler(0, -180, 0));
                        }
                        else
                        {
                            Instantiate(getRamdomCornerAsset(), new Vector3(x, 0f, z - 0.5f * size), Quaternion.Euler(0, -90, 0));
                        }

                    }
                }else if(z == wallLenght)
                {
                    if (x != 0 && x != wallLenght)
                    {
                        Instantiate(getRandomWallAsset(), new Vector3(x, 0f, z + 0.5f * size), Quaternion.Euler(0, -270, 0));
                    }
                    else
                    {
                        if (x == wallLenght)
                        {
                            Instantiate(getRamdomCornerAsset(), new Vector3(x, 0f, z + 0.5f * size), Quaternion.Euler(0, -270, 0));
                        }
                        else
                        {
                            Instantiate(getRamdomCornerAsset(), new Vector3(x - 0.5f * size, 0f, z), Quaternion.Euler(0, 0, 0));
                        }

                    }
                }
                  
                if(x == 0)
                {
                    if (z != 0 && z != wallLenght)
                    {
                        Instantiate(getRandomWallAsset(), new Vector3(x - 0.5f * size, 0f, z), Quaternion.Euler(0, 0, 0));
                    }

                }
                else if (x == wallLenght)
                {
                    if (z != 0 && z != wallLenght)
                    {
                        Instantiate(getRandomWallAsset(), new Vector3(x + 0.5f * size, 0f, z), Quaternion.Euler(0, -180, 0));
                    }

                }
            }

        }
    }

    private GameObject getRandomWallAsset ()
    {
        if(wallPrefabs.Length < 1)
        {
            throw new MissingComponentException("Array für Wandteile ist leer");
        }

        return wallPrefabs[Random.Range(0, wallPrefabs.Length)];
    }

    private GameObject getRamdomCornerAsset()
    {
        if (cornerWallPrefabs.Length < 1)
        {
            throw new MissingComponentException("Array für Wandteile ist leer");
        }

        return cornerWallPrefabs[Random.Range(0, cornerWallPrefabs.Length)];
    }
}