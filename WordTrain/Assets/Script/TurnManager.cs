using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    #region Singleton
    protected static TurnManager _instance;
    //Used only once to ensure when one thread have access to create the instance
    protected static readonly object _Lock = new object();

    public static TurnManager Instance
    {
        get
        {
            //thread safe!
            lock (_Lock)
            {
                if (_instance != null)
                    return _instance;
                TurnManager[] instances = FindObjectsOfType<TurnManager>();
                //see if there are any already more instance of this
                if (instances.Length > 0)
                {
                    //yay only 1 instance so give it back
                    if (instances.Length == 1)
                        return _instance = instances[0];

                    //remove all other instance of it other than the 1st one
                    for (int i = 1; i < instances.Length; i++)
                        Destroy(instances[i]);
                    return _instance = instances[0];
                }

                GameObject manage = new GameObject("TurnManager");
                manage.AddComponent<TurnManager>();

                return _instance = manage.GetComponent<TurnManager>();
            }
        }
    }
    #endregion

    [SerializeField]
    private Player playerOne;
    [SerializeField]
    private Player playerTwo;
    [SerializeField]
    private GameObject canvas; //this is to rotate the canvas than the camera. Camera make it look funky.
    
    public Player CurrentPlayer { get; private set; }
    public float CurrentRotationDegree { get; private set; }
    private void Awake()
    {
        CurrentPlayer = playerOne;
    }

    public void NextTurn()
    {
        //RotateView();
        SwitchPlayer();
    }

    private void SwitchPlayer()
    {
        if(CurrentPlayer.Equals(playerOne))
        {
            CurrentPlayer.LockHand();
            CurrentPlayer = playerTwo;
            CurrentPlayer.UnlockHand();
        }
        else
        {
            CurrentPlayer.LockHand();
            CurrentPlayer = playerOne;
            CurrentPlayer.UnlockHand();
        }
    }

    private void RotateView()
    {
        if(canvas != null)
        {
            CurrentRotationDegree += 180;
            canvas.transform.Rotate(Vector3.forward, CurrentRotationDegree);
        }
            
    }



        
}
