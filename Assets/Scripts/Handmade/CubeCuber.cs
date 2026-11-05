using UnityEngine;

public class CubeCuber : MonoBehaviour
{
    public GameObject toBeCubed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (toBeCubed == null)
            Debug.Log("Nothing to cube");
        else
        {
            for (int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    for(int  k = 0; k < 10; k++)
                    {
                        var go = Instantiate(toBeCubed, new Vector3(i * 1.5f, j * 1.5f, k * 1.5f), Quaternion.identity);
                        go.AddComponent<Rigidbody>();
                        float temp = Random.Range(3f, 7f);
                        Destroy(go, temp);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
