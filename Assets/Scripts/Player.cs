using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    //Unity üzerinde de?i?ken tan?mlama 3 ?ekilde olur.
    //1- private olu?turma(varsay?lan). sadece bu s?n?ftan ula??l?r
    //2- public olu?turma, her yerden ula??l?r(unity'den).
    //3- [SerializeField] koyarak tan?mlan?rsa sadece bu s?n?ftan ula??l?r ama unity üzerinde de ula??l?r.
    //animation penceresi animasyonu olu?turmak için kullan?l?r
    //animator penceresi aniamsyonu kontrol etmek için kullan?l?r (ne zaman ba?las?n, ne yaps?n ve kodla ba?lama)

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Animator anim;

    [SerializeField]
    float speed = 5f; //global de?i?ken
    bool doesRun = false;
    int score = 0;

    [SerializeField]
    Text puanText, puanPanelText;

    [SerializeField]
    GameObject yenidenOynaPanel, mainMenuPanel;
    public static bool gameStarded = false;
    [SerializeField]
    AudioSource coinAudio;


    // Start is called before the first frame update
    void Start()
    {
        if (TekrarOyna.isRestart)
        {
            mainMenuPanel.SetActive(false);
            gameStarded = true;
        }

        if (PlayerPrefs.HasKey("Score"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                PlayerPrefs.DeleteKey("Score");
            }
            else
            {
                score = PlayerPrefs.GetInt("Score");

            }
        }
        puanText.text = score.ToString();
    }





    private void FixedUpdate()
    {
        if (!gameStarded)
        {
            return;
        }
        //Hareket i?lemleri: Input Eksenleridir. Horizontal ve Vertical olarak ikiye ayr?l?r.
        //Horizontal : -1 ~ 1 aras? çal???r. 0 durmak demek. 0 ~ -1 aras? sol / 0 ~ 1 aras? sa? hareket.
        float mySpeedX = Input.GetAxis("Horizontal"); //hareketi alg?l?yor, lokal de?i?kendir.
        move(mySpeedX); //hareket i?lemi
        myAnimation(mySpeedX);
        charDirection(mySpeedX);


    }

    public void playGame()
    {
        gameStarded = true;
        mainMenuPanel.SetActive(false);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //alt?n?n içinden ilk geçildi?inde çal??acak (alt?n?n trigger'? aç?k olmal?)
        //collision parametresi çarpt???m?z nesneyi verir
        //if (collision.CompareTag("Coin"))
        //{
        //    altinSonuc(collision, 1);

        //}

        //else if (collision.CompareTag("BigCoin"))
        //{
        //    altinSonuc(collision, 5);
        //}
        //else if (collision.CompareTag("Enemy"))
        //{
        //    death();
        //}
        //else if (collision.CompareTag("Death"))
        //{
        //    death();
        //}
        //else if (collision.CompareTag("Door"))
        //{
        //    if (SceneManager.GetActiveScene().buildIndex < 1)
        //    {
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //    }
        //    else
        //    {
        //        SceneManager.LoadScene("Level1");
        //    }

        //}

        implementTag(collision);

    }

    void implementTag(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            altinSonuc(collision, 1);

        }

        else if (collision.CompareTag("BigCoin"))
        {
            altinSonuc(collision, 5);
        }
        else if (collision.CompareTag("Enemy"))
        {
            death();
        }
        else if (collision.CompareTag("Death"))
        {
            death();
        }
        else if (collision.CompareTag("Door"))
        {
            if (SceneManager.GetActiveScene().buildIndex < 4)
            {
                saveScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene("Level1");
            }

        }
    }

    void altinSonuc(Collider2D collision, int kazanc)
    {
        score+= kazanc;
        puanText.text = score.ToString();
        Destroy(collision.gameObject);
        coinAudio.Play();
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Death"))
    //    {
    //        death();
    //    }
    //}


    void saveScore()
    {
        PlayerPrefs.SetInt("Score", score);
    }
    void death()
    {
        Destroy(this.gameObject);
        yenidenOynaPanel.SetActive(true);
        puanPanelText.text = "Skor: " + score.ToString();
    }

    void move(float h)
    {

        rb.velocity = new Vector2(h * speed, rb.velocity.y);
    }

    void myAnimation(float h)
    {
        if (h != 0)//Animasyon ??lemleri: E?er mySpeedX = 0 ise idle, de?ilse run
        {
            doesRun = true;

        }
        else
        {
            doesRun = false;
        }
        anim.SetBool("isRunning", doesRun);
    }
    void charDirection(float h) //dönme i?lemleri
    {
        if (h > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (h < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }







}
