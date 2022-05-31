using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
[ExecuteInEditMode]
public class CustomeCharacterMenu : MonoBehaviour
{
    public GameObject character;

    /// <summary>
    /// Dient zum Szenen wechsel von der UIAnimated Szene, zur "Patrick" (Erstes Dorf) Szene, wenn man den save
    /// Button dr�ckt.
    /// </summary>
    public void Submit()
    {
        GetComponent<CheckTheUpdate>().CheckAll();
        SceneManager.LoadScene("Patrick", LoadSceneMode.Single);
    }
    /// <summary>
	/// Wechselt auf das MainMenu zur�ck, wenn der cancel button bet�tigt wurde.
	/// </summary>
    public void CancleButton()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
