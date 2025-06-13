using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    MeshRenderer MyMeshRenderer;
    [SerializeField]
    Material hightlightMat;
    Material originalMat;

    int value = 10;
    public void Collect(PlayerBehaviour player)
    {
        player.ModifyScore(value);
        Destroy(gameObject);
    }
    void Start() 
    {
        MyMeshRenderer = GetComponent<MeshRenderer>(); 
        originalMat = MyMeshRenderer.material;
    }

    public void Highlight() 
    {
        MyMeshRenderer.material = hightlightMat; 
    }

    public void UnHighlight() 
    {
        MyMeshRenderer.material = originalMat; 
    }
}
