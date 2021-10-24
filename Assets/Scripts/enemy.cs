using UnityEngine;
using System.Collections;
public class enemy : MonoBehaviour
{
 private Vector3 startPosition;

 [SerializeField]
 private float frequency = 1.5f;

 [SerializeField]
private float magnitude = 5f;

 [SerializeField]
 private float offset = 0f;

 void Start()
 {
     startPosition = transform.position;
 }

 void Update()
 {
     transform.position = startPosition + transform.right * Mathf.Sin(Time.time * frequency +offset) * magnitude;
 }

}
