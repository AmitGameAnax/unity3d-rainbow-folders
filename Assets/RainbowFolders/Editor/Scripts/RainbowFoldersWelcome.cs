﻿/*
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */

using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Borodar.RainbowFolders.Editor
{
    public class RainbowFoldersWelcome : DraggablePopupWindow
    {
        public const string PREF_KEY = "RainbowFolders.IsWelcomeShown";

        private const float WINDOW_WIDTH = 325f;
        private const float WINDOW_HEIGHT = 100f;

        private static readonly Vector2 WINDOW_SIZE = new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);
        private static readonly Rect WINDOW_RECT = new Rect(Vector2.zero, WINDOW_SIZE);
        private static readonly Rect BACKGROUND_RECT = new Rect(Vector2.one, WINDOW_SIZE - new Vector2(2f, 2f));

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public static void ShowWindow()
        {
            var position = new Rect(CalcWindowPosition(), WINDOW_SIZE);
            var window = GetDraggableWindow<RainbowFoldersWelcome>();
            window.Show<RainbowFoldersWelcome>(position);
        }

        //---------------------------------------------------------------------
        // Messages
        //---------------------------------------------------------------------

        public override void OnGUI()
        {
            base.OnGUI();

            // Background

            var borderColor = EditorGUIUtility.isProSkin ? new Color(0.13f, 0.13f, 0.13f) : new Color(0.51f, 0.51f, 0.51f);
            EditorGUI.DrawRect(WINDOW_RECT, borderColor);

            var backgroundColor = EditorGUIUtility.isProSkin ? new Color(0.18f, 0.18f, 0.18f) : new Color(0.83f, 0.83f, 0.83f);
            EditorGUI.DrawRect(BACKGROUND_RECT, backgroundColor);

            // Content

            GUILayout.BeginHorizontal();
            {
                GUI.skin.label.wordWrap = true;
                GUILayout.Label(new GUIContent(RainbowFoldersEditorUtility.GetAssetLogo()));

                GUILayout.BeginVertical();
                //GUILayout.Label("Let's Make it colorful!");
                GUILayout.Label("Welcome!", EditorStyles.boldLabel);
                GUILayout.Label("With \"Rainbow Folders\" you can set custom icon for any folder in Unity project browser.");
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Close")) Close();
        }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private static Vector2 CalcWindowPosition()
        {
            return GetProjectWindow().position.position + new Vector2(10f, 30f);
        }

        private static EditorWindow GetProjectWindow()
        {
            return GetWindowByName("UnityEditor.ProjectWindow")
                ?? GetWindowByName("UnityEditor.ObjectBrowser")
                ?? GetWindowByName("UnityEditor.ProjectBrowser");
        }

        private static EditorWindow GetWindowByName(string pName)
        {
            var objectList = Resources.FindObjectsOfTypeAll(typeof(EditorWindow));
            return (from obj in objectList where obj.GetType().ToString() == pName select ((EditorWindow) obj)).FirstOrDefault();
        }
    }
}