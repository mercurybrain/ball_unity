using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placement : MonoBehaviour
{

    public GameObject[] tilePrefabs;
    public GameObject currentTile;

    private static Placement instance;
    private Stack<GameObject> leftTiles = new Stack<GameObject>();
    private Stack<GameObject> topTiles = new Stack<GameObject>();
    public Stack<GameObject> LeftTiles { get => leftTiles; set => leftTiles = value; }
    public Stack<GameObject> TopTiles { get => topTiles; set => topTiles = value; }
    public static Placement Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<Placement>();       
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateTiles(100);
        for (int i = 0; i < 50; i++) {
            SpawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTiles(int amnt) {
        for (int i = 0; i < amnt; i++) {
            leftTiles.Push(Instantiate(tilePrefabs[0]));
            topTiles.Push(Instantiate(tilePrefabs[1]));
            // Неактивны, пока не заспаунятся
            leftTiles.Peek().name = "LeftSide";
            leftTiles.Peek().SetActive(false);
            topTiles.Peek().name = "TopSide";
            topTiles.Peek().SetActive(false);
        }
    }

    public void SpawnTile() {

        int indx = Random.Range(0, 2); // 0 или 1 => Лево или верх
        if (indx == 0)
        {
            // Взять тайл и активировать
            GameObject tmp = leftTiles.Pop();
            tmp.SetActive(true);
            // Установить позицию относительно текущего тайла и сделать установленный текущим
            tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(indx).position;
            currentTile = tmp;
        }
        else if (indx == 1) {
            // То же самое, только для тайлов предназначенных для плейсмента выше, а не левее
            GameObject tmp = topTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(indx).position;
            currentTile = tmp;
        }
        // currentTile = (GameObject)Instantiate(tilePrefabs[indx], currentTile.transform.GetChild(0).transform.GetChild(indx).position, Quaternion.identity);

        int spawnPoint = Random.Range(0, 5); // 1 k 5 = 20%
        if (spawnPoint == 0) {
            currentTile.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
