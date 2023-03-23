using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUi : MonoBehaviour
{

    public Image levelSlider;
    public Image LevelImage;
    public Image NextLvlImage;

    private Material playerMaterial;
    // Start is called before the first frame update
    void Awake()
    {
        playerMaterial = FindAnyObjectByType<Player>().transform.GetChild(0).GetComponent<MeshRenderer>().material;

        levelSlider.transform.parent.GetComponent<Image>().color = playerMaterial.color + Color.magenta;
        levelSlider.color = playerMaterial.color;
        LevelImage.color = playerMaterial.color;
        NextLvlImage.color = playerMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelSliderF(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;

    }
}
