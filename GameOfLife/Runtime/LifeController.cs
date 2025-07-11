using UnityEngine;

public class LifeController : MonoBehaviour
{
    #region Public
    public GameObject prefab;
    public Vector2Int m_size = new Vector2Int(5, 5);
    private LifeState[] m_gameState;
    public GameObject[] m_cubes;

    public Color m_deadColor = Color.blue;
    public Color m_aliveColor = Color.red;
    public Color m_undeadColor = Color.green;

    public float m_delay = 1;

    #endregion

    #region API

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_gameState = new LifeState[m_size.x * m_size.y];
        if (m_gameState != null)
        {
            m_cubes = new GameObject[m_gameState.Length];
            Vector3 pos = Vector3.zero;
            for (int x = 0; x < m_gameState.Length; x++)
            {
                Vector2 coords = Get2DCoordinates(x, m_size);
                pos.x = coords.x;
                pos.z = coords.y;
                m_cubes[x] = Instantiate(prefab, pos, Quaternion.identity);
            }
        }
        UpdateCubeColors();
    }

    // Update is called once per frame
    private void Update()
    {
        m_delayDelta -= Time.deltaTime;
        if (m_delayDelta <= 0)
        {
            NextGameFrame();
            UpdateCubeColors();
            m_delayDelta = m_delay;
        }
    }

    private void UpdateCubeColors()
    {
        for (int i = 0; i < m_cubes.Length; i++)
        {
            m_cubes[i].GetComponent<Renderer>().material.color = GetLifeColor(m_gameState[i]);
        }
    }

    #endregion

    #region Utils

    public enum LifeState : byte
    {
        None,
        Dead,
        Alive
    }

    private static byte GetNeighborScore(LifeState state)
    {
        byte score = 0;
        switch (state)
        {
            case LifeState.Alive:
                score = 1;
                break;
            case LifeState.Dead:
                score = 0;
                break;
        }
        return score;
    }

    private static LifeState NextState(LifeState state, int neighbours)
    {
        LifeState nextState;
        switch (state)
        {
            case LifeState.Alive:
                if (neighbours == 2 || neighbours == 3)
                {
                    nextState = LifeState.Alive;
                }
                else
                {
                    nextState = LifeState.Dead;
                }
                break;
            case LifeState.Dead:
                if (neighbours == 3)
                {
                    nextState = LifeState.Alive;
                }
                else
                {
                    nextState = LifeState.Dead;
                }
                break;
            default:
                Debug.Log("Invalid state : " + state);
                nextState = LifeState.None;
                break;
        }
        return nextState;
    }

    public byte GetNeighboursCount(int i, Vector2Int size)
    {
        Vector2Int pos = Get2DCoordinates(i, size);
        bool isY0 = pos.y == 0;
        bool isYMax = pos.y == size.y - 1;
        bool isX0 = pos.x == 0;
        bool isXMax = pos.x == size.x - 1;
        byte neighbours = 0;

        if (!isY0)
        {
            neighbours += GetNeighborScore(m_gameState[Get2DCoordinatesToInt(new Vector2Int(pos.x, pos.y - 1), size)]);
            if (!isX0)
            {
                neighbours +=
                    GetNeighborScore(m_gameState[Get2DCoordinatesToInt(new Vector2Int(pos.x - 1, pos.y - 1), size)]);
            }
            if (!isXMax)
            {
                neighbours +=
                    GetNeighborScore(m_gameState[Get2DCoordinatesToInt(new Vector2Int(pos.x + 1, pos.y - 1), size)]);
            }
        }

        if (!isYMax)
        {
            neighbours += GetNeighborScore(m_gameState[Get2DCoordinatesToInt(new Vector2Int(pos.x, pos.y + 1), size)]);
            if (!isX0)
            {
                neighbours +=
                    GetNeighborScore(m_gameState[Get2DCoordinatesToInt(new Vector2Int(pos.x - 1, pos.y + 1), size)]);
            }
            if (!isXMax)
            {
                neighbours +=
                    GetNeighborScore(m_gameState[Get2DCoordinatesToInt(new Vector2Int(pos.x + 1, pos.y + 1), size)]);
            }
        }

        if (!isX0)
        {
            neighbours += GetNeighborScore(m_gameState[Get2DCoordinatesToInt(new Vector2Int(pos.x - 1, pos.y), size)]);
        }
        if (!isXMax)
        {
            neighbours += GetNeighborScore(m_gameState[Get2DCoordinatesToInt(new Vector2Int(pos.x + 1, pos.y), size)]);
        }
        Debug.Log("position : " + pos.x + ", " + pos.y + " " + neighbours);
        return neighbours;
    }

    public static byte Get2DCoordinatesToInt(Vector2Int coordinates, Vector2Int dimensions)
    {
        return (byte)(coordinates.x * dimensions.x + coordinates.y);
    }

    public static Vector2Int Get2DCoordinates(int i, Vector2Int dimensions)
    {
        return new Vector2Int(i / dimensions.x, i % dimensions.x);
    }
    #endregion

    #region Private

    private Color GetLifeColor(LifeState state)
    {
        Color color = Color.white;
        switch (state)
        {
            case LifeState.Alive:
                color = m_aliveColor;
                break;
            case LifeState.Dead:
                color = m_deadColor;
                break;
            default:
                Debug.Log("Invalid state : " + state);
                break;
        }
        return color;
    }

    private void NextGameFrame()
    {
        LifeState[] nextGameState = new LifeState[m_gameState.Length];
        for (int i = 0; i < m_gameState.Length; i++)
        {
            byte neighbourCount = GetNeighboursCount(i, m_size);
            nextGameState[i] = NextState(m_gameState[i], neighbourCount);
        }
        m_gameState = nextGameState;
    }

    public float m_delayDelta = 1;

    #endregion
}
