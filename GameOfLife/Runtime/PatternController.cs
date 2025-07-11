using UnityEngine;
using UnityEngine.UI;

public class PatternController : MonoBehaviour
{
    #region Public
    public Vector2Int m_size = new Vector2Int(3, 3);
    public Toggle m_prefabToggle;
    #endregion

    #region API
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_toggles = new Toggle[m_size.x * m_size.y];
        for (int i = 0; i < m_size.x; i++)
        {
            for (int j = 0; j < m_size.y; j++)
            {
                m_toggles[i + j * m_size.x] = Instantiate(m_prefabToggle, new Vector3(i, j, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }
    #endregion

    #region Main Methods

    public bool[] GetPattern()
    {
        bool[] pattern = new bool[m_size.x * m_size.y];
        for (int i = 0; i < m_size.x; i++)
        {
            pattern[i] = m_toggles[i].isOn;
        }
        return pattern;
    }

    public Vector2Int GetSize()
    {
        return m_size;
    }

    #endregion

    #region Private
    private Toggle[] m_toggles;
    #endregion
}
