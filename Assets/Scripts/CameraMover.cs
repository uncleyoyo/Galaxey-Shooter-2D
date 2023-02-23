using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public IEnumerator Mover(float durtation)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
        while (elapsed < durtation)
        {
            float x = Random.Range(-1f, 1f);

            float y = Random.Range(-1f, 1f);

            transform.localPosition += new Vector3(x, y, originalPos.z);
            elapsed += Time.time;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
