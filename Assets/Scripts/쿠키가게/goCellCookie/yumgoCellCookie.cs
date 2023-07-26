using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class yumCellCookie : MonoBehaviour
{
    public Button cellcookie;
    // Start is called before the first frame update
    void Start()
    {
        cellcookie.onClick.AddListener(cellcookiefunc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void cellcookiefunc()
    {
        SceneManager.LoadScene("yumcellcookieScene");
    }
}
