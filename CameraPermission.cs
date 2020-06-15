using UnityEngine;
using System.Collections;
using UnityEngine.Events;
<summary>
`// For compile Android devices
</summary>
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

// For compile iOS devices
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class PermissionStates
{
    public const int DEFAULT = 0;       // Never been asked
    public const int NOT_AUTHORIZED = 1;// Not authorized by the user
    public const int AUTHORIZED = 2;    // Authorized by user
}

public class CameraPermission : MonoBehaviour
{
    // Objects containing informative texts
    [SerializeField] private GameObject textNotAuthorizedIOS = default;
    [SerializeField] private GameObject textNotAuthorizedAndroid = default;

    //Events that can be used depending on authorization
    [SerializeField] private UnityEvent<bool> onAuthorized = default;
    [SerializeField] private UnityEvent<bool> onNotAuthorized = default;

    // Private variables
    private delegate void OnVariableChangeDelegate(int value);
    private event OnVariableChangeDelegate OnVariableChange;
    private int _isAuthorized;

    private int isAuthorized
    {
        get 
        { 
            return _isAuthorized; 
        }
        set 
        {
            if (_isAuthorized == value)
            {
                return;
            }

            _isAuthorized = value;
            if (OnVariableChange != null)
            {
                OnVariableChange(_isAuthorized);
            }
        }
    }

    void Awake() 
    {
        isAuthorized = PermissionStates.DEFAULT;

        if (textNotAuthorizedIOS != null)
        {
            textNotAuthorizedIOS.SetActive(false);
        }

        if (textNotAuthorizedAndroid != null)
        {
            textNotAuthorizedAndroid.SetActive(false);
        }

        OnVariableChange += OnVariableExecute;

        InvokeRepeating("ForceUpdate", 2f, 1f);
    }

    #if UNITY_ANDROID
        public void Start()
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Permission.RequestUserPermission(Permission.Camera);
            } 
        }

        void ForceUpdate()
        {
            if (Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                isAuthorized = PermissionStates.AUTHORIZED;
            }
            else
            {
                isAuthorized = PermissionStates.NOT_AUTHORIZED;
            }
        }
    #endif

    #if UNITY_IOS
        IEnumerator Start()
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                isAuthorized = PermissionStates.AUTHORIZED;
            }
            else
            {
                isAuthorized = PermissionStates.NOT_AUTHORIZED;
            }
        }
    #endif

    private void OnVariableExecute(int value)
    {
        if (value == PermissionStates.AUTHORIZED)
        {
            onAuthorized.Invoke(true);
        }
        else if (value == PermissionStates.NOT_AUTHORIZED)
        {
            onNotAuthorized.Invoke(false);
        }

        #if UNITY_ANDROID
            if (textNotAuthorizedAndroid != null)
            {
                textNotAuthorizedAndroid.SetActive(value == PermissionStates.NOT_AUTHORIZED);
            }
        #endif

        #if UNITY_IOS
            if (textNotAuthorizedIOS != null)
            {
                textNotAuthorizedIOS.SetActive(value == PermissionStates.NOT_AUTHORIZED);
            }
        #endif
    }
}
