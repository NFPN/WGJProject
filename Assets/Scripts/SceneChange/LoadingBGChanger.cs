using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadingBGChanger : MonoBehaviour
{
    private Object[] bgImgs;
    public GameObject thisGO;

    private void Awake()
    {
        // Load all images on folder "Assets/Resources/Images/LoadingImgs"
        bgImgs = Resources.LoadAll("Images/LoadingImgs", typeof(Sprite));
    }

    //Changes the background of the Loading Scene, useful for Game Tips or Game Art
    void Start()
    {
        thisGO = gameObject;      
        Sprite bgImageToLoad = (Sprite)bgImgs[Random.Range(0, bgImgs.Length)];
        thisGO.GetComponent<Image>().sprite = bgImageToLoad;
    }
}