using UnityEditor;

/// <summary>
/// いちいち上の実行ボタンを押すのもめんどくさい自分用
/// Alt + Sで実行
/// </summary>
public class QuickStarter{
    [MenuItem("AdditionalItems/QuickStarter &s")]
    static void AudioNameCreate() {
        if(EditorApplication.isPlaying) {
            EditorApplication.isPlaying = false;
        }else {
            EditorApplication.isPlaying = true;
        }
    }
}
