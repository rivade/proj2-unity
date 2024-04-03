using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    public void StartShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 previousPos = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.position.z);

            Vector3 shakePosition = previousPos + (Vector3)Random.insideUnitCircle * magnitude;

            transform.position = shakePosition;

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.position.z);
    }
}
