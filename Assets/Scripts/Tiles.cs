using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{

    private float fallDelay = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            Placement.Instance.SpawnTile();
            StartCoroutine(FallDown());
        }
    }

    IEnumerator FallDown() { // Я упал
        yield return new WaitForSeconds(fallDelay);
        GetComponent<Rigidbody>().isKinematic = false; // Тайл получает чуть больше свободы и просто падает
        yield return new WaitForSeconds(2);
        // Возвращение тайла домой, придумано для переработки тайлов, а не их постоянного спавна через статик метод
        switch (gameObject.name) {
            case "LeftSide":
                Placement.Instance.LeftTiles.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;

            case "TopSide":
                Placement.Instance.TopTiles.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;
        }
    }
}
