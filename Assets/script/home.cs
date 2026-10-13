using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class home : MonoBehaviour
{
    public AudioSource audios;
    public AudioClip audi1;
    public AudioClip audi2;

    public GameObject panel;
    public Transform image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audios.clip = audi2;
        //audios.loop = true;
        audios.Play();
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        image.Rotate(0, 0, 1 * 200f * Time.deltaTime);





    }

    public void Play()
    {




        StartCoroutine(Load());

        //  SceneManager.LoadScene(0);
        audios.PlayOneShot(audi1);
    }
    public void About()
    {
        SceneManager.LoadScene(2);
        audios.PlayOneShot(audi1);
    }
    IEnumerator Load()
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);

    }
    public void Quit()
    {
        Application.Quit();
    }

}
