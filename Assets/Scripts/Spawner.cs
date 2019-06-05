using UnityEngine;

public class Spawner : MonoBehaviour {
    public int counter;
    public int totalStars;
    public int maxSections = 0; // Value 0 means render all
    
    private int m_OldCounter;
    private string m_Path = "Assets/Data/";
    private float m_Test;
    private GameObject m_CurrentSection;
    
    [SerializeField] private GameObject m_GenerateSection;
    
    private void Load(TextAsset[] files) {
        int check = 0;
        // Looping through each file
        foreach (TextAsset file in files) {
            if (check > maxSections && maxSections != 0) return;
            
            m_CurrentSection = Instantiate(m_GenerateSection, new Vector3(0, 0, 0), Quaternion.identity);
            m_CurrentSection.GetComponent<GenerateStars>().starsData = file;
            m_CurrentSection.transform.SetParent(gameObject.transform);

            check++;
        }
    }
   
    // Start is called before the first frame update
    private void Start() {
        Load(Resources.LoadAll<TextAsset>("Data"));
    }

    private void Update() {
        if (counter == m_OldCounter) return;

        Debug.Log("No. of Stars: " + counter + " / " + totalStars);
        m_OldCounter = counter;
    }
}
