using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TekrarOyna : MonoBehaviour
{
    public static bool isRestart = false;




    public void restartGame()
    {
        //sahnelerin yeniden y�klenmesi veya bir sonraki sahneye ge�i? i?lemleri "Scene Management" s?n?f? ile olur.
        //aktif olan sahnenin tekrar y�klenmesi i�in:

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isRestart = true;

    }

    public void quitGame()
    {
        //oyunu kapatmak i�in kullan?l?r, unity ekran?nda �al??maz, apk'da �al???r.
        Application.Quit();
    }
}
