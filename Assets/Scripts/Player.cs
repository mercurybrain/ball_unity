using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    private Vector3 dir;
    public GameObject particles;
    private bool isDead;
    public GameObject restartBtn;

    private int score = 0;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDead) {
            if (dir == Vector3.forward)
            {
                dir = Vector3.left;
            }
            else {
                dir = Vector3.forward;
            }
        }
        float moveTiles = speed * Time.deltaTime; // На разных кадрах плавное движение

        transform.Translate(dir * moveTiles); // Само движение, изменяя позицию шара через Transform
    }

    private void OnTriggerEnter(Collider other)
    {
        // Обработка попадания по кристаллу
        if (other.tag == "Point") {
            other.gameObject.SetActive(false);
            Instantiate(particles, transform.position, Quaternion.identity);
            score++;
            scoreText.text = score.ToString();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor") {
            RaycastHit hit; // Во что попал луч
            Ray downRay = new Ray(transform.position, -Vector3.up); // Луч вниз
            if (!Physics.Raycast(downRay, out hit)) {
                // Смерть 
                isDead = true;
                GetComponent<Collider>().enabled = false; // Чтобы после смерти если вдруг врежется в другой коллайдер ничего не работало, а то мало ли
                Button btn = restartBtn.GetComponent<Button>();
                restartBtn.SetActive(true);
                // Отвязка камеры
                transform.GetChild(0).transform.parent = null;
                btn.onClick.AddListener(ResetGame);
            }
        }
    }
    private void ResetGame()
    {
        // Перезагружаем сцену
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

}
