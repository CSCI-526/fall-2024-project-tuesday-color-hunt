using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWithEnemy : MonoBehaviour
{
    [SerializeField] private Transform initialGrabParent;
    [SerializeField] private float x_pos;
    [SerializeField] private float y_pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (initialGrabParent && initialGrabParent.gameObject.activeInHierarchy)
        {
            // Adjust the offset values to move the object up and to the right
            Vector3 offset = new Vector3(x_pos, y_pos, 0.0f); // Change these values to fine-tune the movement
            transform.position = initialGrabParent.position + offset;
        }
    }
}
