using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    public Image bgImage;

    public GameObject startMenu;
    
    public GameObject endMenu;

    public Button startButton;

    public Button endButton;

    private void Start()
    {
        OpenStartMenu();
    }

    public void OpenStartMenu()
    {
        ToggleMenu(startMenu, startButton, true);
    }
    
    public void OpenEndMenu()
    {
        ToggleMenu(endMenu, endButton, true);
    }
    
    public void CloseStartMenu()
    {
        ToggleMenu(startMenu, startButton, false);
    }
    
    public void CloseEndMenu()
    {
        ToggleMenu(endMenu, endButton, false);
    }
    
    public void ToggleMenu(GameObject menu, Button button, bool isOn)
    {
        bgImage.gameObject.SetActive(isOn);
        
        menu.gameObject.SetActive(isOn);
        
        button.gameObject.SetActive(isOn);
    }
}
