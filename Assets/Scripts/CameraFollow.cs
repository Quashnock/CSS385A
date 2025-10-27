using UnityEngine;

public class CameraFollow : MonoBehaviour, IDataPersistence
{
    [SerializeField] Transform target;

    public Vector2 bufferSize = new Vector2(2f, 1.5f);
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        Vector2 cameraPos = transform.position;
        Vector2 targetPos = target.position;

        Vector2 offset = targetPos - cameraPos;

        if (Mathf.Abs(offset.x) > bufferSize.x || Mathf.Abs(offset.y) > bufferSize.y)
        {
            Vector2 newPos = new Vector2(targetPos.x, targetPos.y);
            transform.position = Vector2.Lerp(cameraPos, newPos, smoothSpeed * Time.deltaTime);
        }
    }

    public void LoadData(GameData data)
    {
        transform.position = data.cameraPosition;
    }


    public void SaveData(ref GameData data)
    {
        data.cameraPosition = transform.position;
    }
}
