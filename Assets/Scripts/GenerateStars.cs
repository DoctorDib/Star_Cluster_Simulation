using System.Text.RegularExpressions;
using Random = System.Random;
using UnityEngine;

public class GenerateStars : MonoBehaviour {

    public TextAsset starsData;

    private bool m_Initial = false;
    private bool m_Setup = true;
    private int m_Tick;
    private int m_Spawner;
    private string[] m_Lines;
    private const string m_SplitRe = @"\n";
    private Spawner m_Parent;
    
    [SerializeField] private GameObject m_StarPrefab;

    private void Start() {
        m_Parent = gameObject.transform.parent.GetComponent<Spawner>();
    }
    
    private void GenerateStar(string[] props) {
        /**
         * 0 = x
         * 1 = y
         * 2 = z
         * 3 = vx (velocity x)
         * 4 = vy
         * 5 = vz
         * 6 = m
         * 7 = id
         */
        
        GameObject star = Instantiate(m_StarPrefab, new Vector3(float.Parse(props[0]), float.Parse(props[2]), float.Parse(props[3])), Quaternion.identity);
        star.transform.SetParent(transform);
        star.name = props[7]; // Changing the name
        
        Random rnd = new Random();
       
        float randomSize = rnd.Next(35, 150);
        randomSize = randomSize / 100000;
        
        // improve random size
        star.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
    }

    private void LateUpdate() {
        if (starsData == null) return;

        if (m_Setup) {
            m_Lines = Regex.Split(starsData.text, m_SplitRe);

            // Take away the titles e.g x, y and z
            m_Parent.totalStars += m_Lines.Length - 1; 
        }
        m_Setup = false;

        if (m_Lines == null) return;
        
        if (m_Tick+1 >= m_Lines.Length) return;
        m_Tick++;
        
        GenerateStar(Regex.Split(m_Lines[m_Tick], @","));

        m_Parent.counter++;
    }
}
