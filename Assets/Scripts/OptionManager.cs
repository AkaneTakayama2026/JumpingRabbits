using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public GameObject optionPanel;
    void Start()
    {
        optionPanel.SetActive(false);
    }
    public void OpenOption()
    {
        optionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        optionPanel.SetActive(false);
    }
}
