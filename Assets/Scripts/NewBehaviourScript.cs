using UnityEngine;
using System.Runtime.InteropServices;
public class NewBehaviourScript : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void Hello();
    // Start is called before the first frame update
    void Start()
    {
        //Hello();
    }
}
