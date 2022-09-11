using UnityEngine;
using System.Collections;
using UnityEditor;

namespace unity4dv
{

    [CustomEditor(typeof(Plugin4DS))]
    public class Plugin4DSEditor : Editor
    {
        bool showFold = false;

        public override void OnInspectorGUI()
        {
            Plugin4DS myTarget = (Plugin4DS)target;

            Undo.RecordObject(myTarget, "Inspector");

            BuildFilesInspector(myTarget);

            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }



        private void BuildFilesInspector(Plugin4DS myTarget)
        {
            GUILayout.Space(10);

            myTarget.SourceType = (SOURCE_TYPE)EditorGUILayout.EnumPopup(new GUIContent(" Data Source", "Choose the source of the data between local file and over http network") , myTarget.SourceType);

            Rect rect = EditorGUILayout.BeginVertical(); ;
            if (myTarget.SourceType == SOURCE_TYPE.Local)
            {
                //rect to allow drag'n'drop
                myTarget.SequenceName = EditorGUILayout.TextField(new GUIContent("Sequence Name","4ds file name. Drag'n'drop the file in the field for an automatic configuration"), myTarget.SequenceName);
                EditorGUILayout.EndVertical();

                myTarget._dataInStreamingAssets = EditorGUILayout.Toggle(new GUIContent("In Streaming Assets","checked if the 4ds file is located in the StreamingAssets folder (necessary to deploy it with the app)"), myTarget._dataInStreamingAssets);
                if (!myTarget._dataInStreamingAssets)
                    myTarget.SequenceDataPath = EditorGUILayout.TextField("Data Path", myTarget.SequenceDataPath);
            }
            else //NETWORK
            {
                myTarget.SequenceName = EditorGUILayout.TextField(new GUIContent("URL", "Set the entire URL of the 4ds file (like http://myURL.com/myfile.4ds)"), myTarget.SequenceName);
                EditorGUILayout.EndVertical();
                myTarget.HTTPKeepInCache = EditorGUILayout.Toggle(new GUIContent("Keep Data In Cache", "Keep the download data in cache memory, to avoid to download again when looping"), myTarget.HTTPKeepInCache);

                showFold =  EditorGUILayout.BeginFoldoutHeaderGroup(showFold, "Advanced");
                if (showFold)
                {
                    myTarget.HTTPDownloadSize = EditorGUILayout.IntField(new GUIContent("Request Download Size (in MB)", "Set the size of payload for each download request to the server.Bigger size takes more time to download but does less requests"), myTarget.HTTPDownloadSize/1000000)*1000000;
                    myTarget.HTTPCacheSize = EditorGUILayout.LongField(new GUIContent("Cache Max Size (in MB)", "Maximum size the memory cache can be. Warning, this cache is common for All sequences, so changing the cache size for a sequence will change it for all of them"), myTarget.HTTPCacheSize/1000000)*1000000;
                }
                EditorGUILayout.EndFoldoutHeaderGroup();

                if (Event.current.keyCode == KeyCode.Return)
                {
                    myTarget.Close();
                    myTarget.Preview();
                    myTarget.last_preview_time = System.DateTime.Now;
                }
            }

            GUILayout.Space(10);

            myTarget.AutoPlay = EditorGUILayout.Toggle(new GUIContent("Play On Start", "Automatically plays the sequence when the app starts"), myTarget.AutoPlay);
            myTarget.Loop = EditorGUILayout.Toggle(new GUIContent("Playback Loop","Restart the sequence playback when it reaches the end"), myTarget.Loop);


            GUILayout.Space(10);

            GUIContent previewframe = new GUIContent("Preview Frame");
            Color color = GUI.color;
            if ((myTarget.LastActiveFrame != -1) && (myTarget.PreviewFrame < (int)myTarget.FirstActiveFrame || myTarget.PreviewFrame > (int)myTarget.LastActiveFrame))
                GUI.color = new Color(1, 0.6f, 0.6f);

            int frameVal = EditorGUILayout.IntSlider(previewframe, myTarget.PreviewFrame, 0, myTarget.SequenceNbOfFrames - 1);
            if (frameVal != myTarget.PreviewFrame)
            {
                myTarget.PreviewFrame = (int)frameVal;
                myTarget.Preview();
                myTarget.last_preview_time = System.DateTime.Now;
            }
            else
                myTarget.ConvertPreviewTexture();
            GUI.color = color;

            GUIContent activerange = new GUIContent(new GUIContent("Active Range", "Set the frame range that will be played"));
            float rangeMax = myTarget.LastActiveFrame == -1 ? myTarget.SequenceNbOfFrames - 1 : myTarget.LastActiveFrame;
            if (myTarget.LastActiveFrame == -1)
                GUI.color = new Color(0.5f, 0.7f, 2.0f);
            float firstActiveFrame = myTarget.FirstActiveFrame;
            EditorGUILayout.MinMaxSlider(activerange, ref firstActiveFrame, ref rangeMax, 0.0f, myTarget.SequenceNbOfFrames - 1);
            myTarget.FirstActiveFrame = (int)firstActiveFrame;
            if (rangeMax == myTarget.SequenceNbOfFrames - 1 && myTarget.FirstActiveFrame == 0)
                myTarget.LastActiveFrame = -1;
            else
                myTarget.LastActiveFrame = (int)rangeMax;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();

            if (myTarget.LastActiveFrame == -1)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Full Range", GUILayout.Width(80));
                GUI.color = color;
                EditorGUILayout.Space();
                myTarget.LastActiveFrame = -1;
            }
            else
            {
                myTarget.FirstActiveFrame = EditorGUILayout.IntField((int)myTarget.FirstActiveFrame, GUILayout.Width(50));
                EditorGUILayout.Space();
                myTarget.LastActiveFrame = EditorGUILayout.IntField((int)myTarget.LastActiveFrame, GUILayout.Width(50));
            }


            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
            //myTarget.OutOfRangeMode = (OUT_RANGE_MODE)EditorGUILayout.EnumPopup("Out of Range Mode", myTarget.OutOfRangeMode);

            myTarget.SpeedRatio = EditorGUILayout.FloatField(new GUIContent("Speed Ratio","Change the playback speed. A value of 1 means normals speed, 2 is twice the speed, 0.5 is half speed, etc."), myTarget.SpeedRatio);

            myTarget._debugInfo = EditorGUILayout.Toggle(new GUIContent("Debug Info", "Display some information useful for debugging") , myTarget._debugInfo);


            Event evt = Event.current;
            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (rect.Contains(evt.mousePosition))
                        DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                    else
                    {
                        //EditorGUILayout.EndVertical();
                        return;
                    }

                    if (evt.type == EventType.DragPerform)
                    {
                        foreach (string path in DragAndDrop.paths)
                        {
                            string seqName = path.Substring(path.LastIndexOf("/") + 1);
                            string dataPath = path.Substring(0, path.LastIndexOf("/") + 1);

                            if (dataPath.Contains("StreamingAssets"))
                            {
                                myTarget._dataInStreamingAssets = true;
                                dataPath = dataPath.Substring(dataPath.LastIndexOf("StreamingAssets") + 16);
                                myTarget.SequenceDataPath = dataPath;
                            }
                            else
                            {
                                if (dataPath.Contains("Assets"))
                                {
                                    string message = "The sequence should be in \"Streaming Assets\" for a good application deployment";
                                    EditorUtility.DisplayDialog("Warning", message, "Close");
                                }
                                myTarget._dataInStreamingAssets = false;
                                myTarget.SequenceDataPath = dataPath;
                            }

                            myTarget.SequenceName = seqName;
                            myTarget.SourceType = SOURCE_TYPE.Local;
                            myTarget.PreviewFrame = 0;
                            myTarget.FirstActiveFrame = 0;
                            myTarget.LastActiveFrame = -1;

                            EditorUtility.SetDirty(target);

                            myTarget.Close();
                            myTarget.Preview();
                        }
                    }
                    break;
            }

        }

    }

}