using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToScene : MonoBehaviour
{
    public Button b;
    // Start is called before the first frame update
    void Start()
    {
        b = gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    [SerializeField]
    public void GoToSceneNow(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
