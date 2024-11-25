using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light2DShades : MonoBehaviour
{
    private ColorControl cc;
    private List<GameObject> objectsCollided = new List<GameObject>();

    void Start()
    {
        cc = FindObjectOfType<ColorControl>();
    }

    void Update()
    {
        for (int i = 0; i < objectsCollided.Count; i++)
        {
            GameObject obj = objectsCollided[i];
            if (obj.GetComponent<SpriteRenderer>())
            {
                Color objectColor = obj.GetComponent<SpriteRenderer>().color;
                if (cc.CompareColors(objectColor, GetComponent<SpriteRenderer>().color))
                {
                    if (obj.CompareTag("Enemy"))
                    {
                        obj.SetActive(false);

                        int enemyKilledCount = PlayerPrefs.GetInt("EnemyKilledCount", 0);
                        enemyKilledCount++;
                        PlayerPrefs.SetInt("EnemyKilledCount", enemyKilledCount);
                        PlayerPrefs.Save();

                        if (CompareTag("LightShades"))
                        {
                            int enemyTrappedCount = PlayerPrefs.GetInt("EnemyTrappedCount", 0);
                            if (enemyTrappedCount != 0)
                            {
                                enemyTrappedCount--;
                            }
                            PlayerPrefs.SetInt("EnemyTrappedCount", enemyTrappedCount);
                            PlayerPrefs.Save();
                        }
                    }
                    if (obj.CompareTag("Obstacle"))
                    {
                        obj.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsCollided.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objectsCollided.Contains(collision.gameObject))
        {
            objectsCollided.Remove(collision.gameObject);
        }
    }
}
