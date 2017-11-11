using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject music;

    private void Awake()
    {
        if (music == null)
        {
            Instantiate(music);
        }

    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DontDestroyOnLoad(music);

    }

}
