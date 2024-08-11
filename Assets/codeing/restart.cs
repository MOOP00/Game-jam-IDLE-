using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    public string _newGamelevel;
    public string _newGamelevel2;

    public void NewGameDialogYes() {
        SceneManager.LoadScene(_newGamelevel2);
    }
    public void NewGameDialogNO() {
        SceneManager.LoadScene(_newGamelevel);
    }
}
