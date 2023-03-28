using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;
using WitchCompany.API;
using WitchCompany.GameSystem;

public class TestAPIWindow : EditorWindow
{
    [SerializeField] private int testInt;
    [SerializeField] private string eMail;
    [SerializeField] private string password;
    [SerializeField] private string acToken;
    [SerializeField] private string rfToken;

    private GUILayoutOption[] labelOption = new[]
    {
        GUILayout.Width(200),
        GUILayout.Height(50)
    };

    private GUIStyle style = new GUIStyle();
    
    [MenuItem("SBTest/API Test/Show Window1")]
    public static void ShowWindow1()
    {
        Debug.Log("ShowWindow1");
        EditorWindow.GetWindow(typeof(TestAPIWindow));
    }
    
    [MenuItem("SBTest/API Test/Show Window2")]
    public static void ShowWindow2()
    {
        Debug.Log("ShowWindow2");
        EditorWindow.GetWindow<TestAPIWindow>();
    }

    async void OnGUI()
    {
        ChangeGUIStyle(24, Color.blue);
        GUILayout.Label("Just Test", style, labelOption);
        testInt = EditorGUILayout.IntField("tInt", testInt);
        GUILayout.Space(10);
        
        if (GUILayout.Button("UniTask Test Button"))
        {
            Debug.Log("button click!!!");
            await TestUniTask();
            return;
        }
        GUILayout.Space(10);
        
        ChangeGUIStyle();
        GUILayout.Label("Login Test",EditorStyles.whiteBoldLabel, labelOption);
        eMail = EditorGUILayout.TextField("E-Mail", eMail);
        password = EditorGUILayout.TextField("Password", password);
        GUILayout.Space(5);
        acToken = EditorGUILayout.TextField("Access Token", acToken);
        rfToken = EditorGUILayout.TextField("Refresh Token", rfToken);
        GUILayout.Space(10);
        
        if (GUILayout.Button("Login"))
        {
            Debug.Log("login click!!!");
            //var auth = await AuthAPI.EmailLogin(eMail,password);
            var auth = await AuthAPI.EmailLogin(Game.Setting.email,Game.Setting.password);
            eMail = Game.Setting.email;
            password = Game.Setting.password;
            acToken = auth.accessToken;
            rfToken = auth.refreshToken;
            
            Debug.Log(JsonConvert.SerializeObject(auth));
            return;
        }
    }

    private static async UniTask TestUniTask()
    {
        await UniTask.Delay(1000);
        Debug.Log("대기 1초");
        await UniTask.Delay(1000);
        Debug.Log("대기 2초");
        await UniTask.Delay(1000);
        Debug.Log("대기 3초");
    }

    private void ChangeGUIStyle(int fontSize = 12, Color fontColor = default)
    {
        if(fontColor == default) fontColor = Color.white;
        
        style.fontSize = fontSize;
        style.normal.textColor = fontColor;
    }

    private void InitGUIStyle()
    {
        //ChangeGUIStyle();
    }
}

// public class TestAPI : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         TestAPIWindow t = (TestAPIWindow)target;
//         EditorGUILayout.LabelField("tInt", t.testInt.ToString());
//     }
// }