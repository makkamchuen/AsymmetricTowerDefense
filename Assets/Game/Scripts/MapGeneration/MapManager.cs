using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
public class MapManager : MonoBehaviour 
{

    public int width; // n
    public int height; // m
    public float unit;
    public int smoothness; // k
    public int pathRadius; // r
    public int minSeaSize;
    public int minIslandSize;

    public string seed;
    public bool useRandomSeed;

    public MapGenerator mapGenerator;

    [HideInInspector] public int[,] map;
    private MeshGenerator meshGen;
    private RandomSprite _randomSprite;
    private GameObject _filler;
    private List<GameObject> _fillers;
    [SerializeField] private float _wallHeight;
    private NavMeshSurface _navMeshSurface;
    
    void Start()
    {
        meshGen = GetComponent<MeshGenerator>();
        _randomSprite = GetComponent<RandomSprite>();
        InitFiller();
        _fillers = new List<GameObject>();
        _navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void InitFiller()
    {
        _filler = new GameObject {layer = 8};
        _filler.AddComponent<BoxCollider>();
        _filler.GetComponent<BoxCollider>().size = new Vector3(unit, _wallHeight, unit);
    }

    void Update() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            GenerateMap();
        }
    }

    public void GenerateMap()
    {
        if (useRandomSeed) 
        {
            seed = Time.time.ToString();
        }
        DestroyColliders();
        mapGenerator.GenerateMap(width, height, new System.Random(seed.GetHashCode()));
        for (int i = 0; i < smoothness; i += 1)
        {
            SmoothMap();
        }
        // _environmentGenerator.BuildEnvironment(map, unit, centerX, centerZ);
        meshGen.GenerateMesh(GetInversedMap(), unit);
        SetUpColliders();
        _randomSprite.placeSprite();
        if (_navMeshSurface.navMeshData == null)
        {
            _navMeshSurface.BuildNavMesh();
        }
        else
        {
            _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
        }
    }

    private int[,] GetInversedMap()
    {
        int[,] inversedMap = new int[width, height];
        for (int x = 0; x < width; x ++) 
        {
            for (int y = 0; y < height; y ++) 
            {
                inversedMap[x, y] = map[x, y] == 1? 0: 1;
            }
        }
        return inversedMap;
    }

    public void RebakeNavMesh()
    {
        
    }

    public void DestroyColliders()
    {
        foreach (GameObject filler in _fillers)
        {
            Destroy(filler);
        }
    }

    private void SetUpColliders()
    {
        if (map != null) 
        {
            for (int x = 0; x < width; x ++) 
            {
                for (int y = 0; y < height; y ++) 
                {
                    if (map[x, y] == 1)
                    {
                        Vector3 pos = new Vector3((-width / 2 + x) * unit, 0, (-height / 2 + y) * unit);
                        GameObject filler = Instantiate(_filler, transform.position + pos, Quaternion.identity);
                        filler.transform.SetParent(transform);
                        _fillers.Add(filler);
                        GameObjectUtility.SetNavMeshArea(filler, 1);
                    }
                }
            }
        }
    }

    void SmoothMap() // O(nm)
    {
        for (int x = 0; x < width; x += 1) 
        {
            for (int y = 0; y < height; y += 1) 
            {
                int neighbourWallTiles = GetSurroundingWallCount(x,y);
                if (neighbourWallTiles > 4)
                {
                    map[x, y] = 1;
                }
                else if (neighbourWallTiles < 4)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY) // O(1)
    { 
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX += 1) 
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY += 1) 
            {
                if (IsInMapRange(neighbourX, neighbourY)) 
                {
                    if (neighbourX != gridX || neighbourY != gridY) 
                    {
                        wallCount += map[neighbourX,neighbourY];
                    }
                }
                else 
                {
                    wallCount ++;
                }
            }
        }
        return wallCount;
    }
    
    bool IsInMapRange(int x, int y) 
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
    
    public void CreatePassage(Coordinate tileA, Coordinate tileB, int val = 0) 
    {
        List<Coordinate> line = GetLine (tileA, tileB);
        foreach (Coordinate c in line) 
        {
            FillPoint(c, val);
        }
    }
    
    private List<Coordinate> GetLine(Coordinate from, Coordinate to) 
    {
        List<Coordinate> line = new List<Coordinate> ();
        line.Add(to);
        int x = from.tileX;
        int y = from.tileY;
        int dx = to.tileX - from.tileX;
        int dy = to.tileY - from.tileY;
        bool inverted = false;
        int step = Math.Sign (dx);
        int gradientStep = Math.Sign (dy);
        int longest = Mathf.Abs (dx);
        int shortest = Mathf.Abs (dy);
        if (longest < shortest) {
            inverted = true;
            longest = Mathf.Abs(dy);
            shortest = Mathf.Abs(dx);
            step = Math.Sign (dy);
            gradientStep = Math.Sign (dx);
        }
        int gradientAccumulation = longest / 2;
        for (int i = 0; i < longest; i += 1) 
        {
            line.Add(new Coordinate(x,y));
            if (inverted) 
            {
                y += step;
            }
            else 
            {
                x += step;
            }
            gradientAccumulation += shortest;
            if (gradientAccumulation >= longest) 
            {
                if (inverted) 
                {
                    x += gradientStep;
                }
                else 
                {
                    y += gradientStep;
                }
                gradientAccumulation -= longest;
            }
        }
        return line;
    }

    public void FillPoint(Coordinate c, int val = 0)
    {
        DrawCircle(c, val);
    }

    private void DrawCircle(Coordinate c, int val) 
    {
        for (int x = -pathRadius; x <= pathRadius; x += 1) 
        {
            for (int y = -pathRadius; y <= pathRadius; y += 1) 
            {
                if (x*x + y*y <= pathRadius*pathRadius) 
                {
                    int drawX = c.tileX + x;
                    int drawY = c.tileY + y;
                    if (IsInMapRange(drawX, drawY)) 
                    {
                        map[drawX,drawY] = val;
                    }
                }
            }
        }
    }

    void OnDrawGizmos() 
    {
        if (map != null) 
        {
            for (int x = 0; x < width; x ++) 
            {
                for (int y = 0; y < height; y ++) 
                {
                    Gizmos.color = (map[x,y] == 1)? Color.blue:Color.green;
                    Vector3 pos = new Vector3((-width/2 + x) * unit + transform.position.x,0, (-height/2 + y) * unit + transform.position.z);
                    Gizmos.DrawCube(pos,new Vector3(unit, 0, unit));
                }
            }
        }
    }

    public void FillMap(int val)
    {
        for (int x = 0; x < width; x += 1) 
        {
            for (int y = 0; y < height; y += 1) 
            {
                map[x, y] = val;
            }
        }
    }

    public void GatherRegion()
    {
        
    }
}