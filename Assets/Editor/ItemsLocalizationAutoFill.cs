//using UnityEngine;
//using UnityEditor;
//using UnityEditor.Localization;
//using UnityEngine.Localization.Tables;
//using UnityEngine.Localization;
//using System.Collections.Generic;
//using System.Linq;

//public class ItemLocalizationEditor : EditorWindow
//{
//    private StringTableCollection tableCollection;
//    private List<Item> items = new List<Item>();
//    private Vector2 scrollPosition;
//    private bool addNameSuffix = true;
//    private bool addDescriptionSuffix = true;
//    private string nameSuffix = "_name";
//    private string descriptionSuffix = "_desc";
//    private bool autoAssignLocalizedStrings = true;

//    [MenuItem("Tools/Item Localization Editor")]
//    public static void ShowWindow()
//    {
//        GetWindow<ItemLocalizationEditor>("Item Localization");
//    }

//    private void OnGUI()
//    {
//        GUILayout.Label("Item Localization Editor", EditorStyles.boldLabel);
//        EditorGUILayout.Space();

//        // Localization table selection
//        EditorGUILayout.LabelField("Localization Table", EditorStyles.boldLabel);
//        tableCollection = (StringTableCollection)EditorGUILayout.ObjectField(
//            "String Table Collection",
//            tableCollection,
//            typeof(StringTableCollection),
//            false
//        );

//        EditorGUILayout.Space();

//        // Suffix settings
//        EditorGUILayout.LabelField("Key Settings", EditorStyles.boldLabel);
//        addNameSuffix = EditorGUILayout.Toggle("Add Name Suffix", addNameSuffix);
//        if (addNameSuffix)
//        {
//            nameSuffix = EditorGUILayout.TextField("Name Suffix", nameSuffix);
//        }

//        addDescriptionSuffix = EditorGUILayout.Toggle("Add Description Suffix", addDescriptionSuffix);
//        if (addDescriptionSuffix)
//        {
//            descriptionSuffix = EditorGUILayout.TextField("Description Suffix", descriptionSuffix);
//        }

//        EditorGUILayout.Space();
//        autoAssignLocalizedStrings = EditorGUILayout.Toggle("Auto-assign LocalizedStrings", autoAssignLocalizedStrings);

//        EditorGUILayout.Space();

//        // Items list
//        EditorGUILayout.LabelField("Items to Add", EditorStyles.boldLabel);

//        EditorGUILayout.BeginHorizontal();
//        if (GUILayout.Button("Add Selected Items"))
//        {
//            AddSelectedItems();
//        }
//        if (GUILayout.Button("Clear List"))
//        {
//            items.Clear();
//        }
//        EditorGUILayout.EndHorizontal();

//        EditorGUILayout.Space();
//        EditorGUILayout.LabelField("Items to Add", EditorStyles.boldLabel);

//        // Scroll view with items
//        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));

//        // Drag and drop area
//        Rect dropArea = GUILayoutUtility.GetRect(0f, 50f, GUILayout.ExpandWidth(true));
//        GUI.Box(dropArea, "Drag items here", EditorStyles.helpBox);

//        HandleDragAndDrop(dropArea);

//        EditorGUILayout.Space(10);

//        for (int i = 0; i < items.Count; i++)
//        {
//            EditorGUILayout.BeginHorizontal();
//            items[i] = (Item)EditorGUILayout.ObjectField(items[i], typeof(Item), false);
//            if (GUILayout.Button("X", GUILayout.Width(25)))
//            {
//                items.RemoveAt(i);
//                i--;
//            }
//            EditorGUILayout.EndHorizontal();
//        }

//        EditorGUILayout.EndScrollView();

//        EditorGUILayout.Space();

//        // Add to table button
//        GUI.enabled = tableCollection != null && items.Count > 0;

//        if (GUILayout.Button("Add to Localization Table", GUILayout.Height(30)))
//        {
//            AddItemsToLocalizationTable();
//        }

//        GUI.enabled = true;

//        EditorGUILayout.Space();
//        EditorGUILayout.HelpBox(
//            "1. Select a localization table (String Table Collection)\n" +
//            "2. Add items using 'Add Selected Items' or drag them manually\n" +
//            "3. Enable 'Auto-assign LocalizedStrings' to automatically link items to localization\n" +
//            "4. Click 'Add to Localization Table' to add them",
//            MessageType.Info
//        );
//    }

//    private void AddSelectedItems()
//    {
//        Object[] selection = Selection.objects;
//        foreach (Object obj in selection)
//        {
//            Item item = obj as Item;

//            if (item == null && obj is GameObject go)
//            {
//                item = go.GetComponent<Item>();
//            }

//            if (item != null && !items.Contains(item))
//            {
//                items.Add(item);
//                Debug.Log($"Added item: {item.nameItem}");
//            }
//        }

//        if (items.Count == 0)
//        {
//            Debug.LogWarning("No Items found among selected objects");
//        }
//    }

//    private void HandleDragAndDrop(Rect dropArea)
//    {
//        Event evt = Event.current;

//        switch (evt.type)
//        {
//            case EventType.DragUpdated:
//            case EventType.DragPerform:
//                if (!dropArea.Contains(evt.mousePosition))
//                    return;

//                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

//                if (evt.type == EventType.DragPerform)
//                {
//                    DragAndDrop.AcceptDrag();

//                    foreach (Object draggedObject in DragAndDrop.objectReferences)
//                    {
//                        // Try to get Item from GameObject or directly
//                        Item item = draggedObject as Item;

//                        if (item == null && draggedObject is GameObject go)
//                        {
//                            item = go.GetComponent<Item>();
//                        }

//                        if (item != null && !items.Contains(item))
//                        {
//                            items.Add(item);
//                            Debug.Log($"Added item: {item.nameItem}");
//                        }
//                        else
//                        {
//                            Debug.LogWarning($"Failed to add object: {draggedObject.name} ({draggedObject.GetType()})");
//                        }
//                    }
//                }
//                evt.Use();
//                break;
//        }
//    }

//    private void AddItemsToLocalizationTable()
//    {
//        if (tableCollection == null)
//        {
//            EditorUtility.DisplayDialog("Error", "Please select a localization table!", "OK");
//            return;
//        }

//        if (items.Count == 0)
//        {
//            EditorUtility.DisplayDialog("Error", "Please add items to the list!", "OK");
//            return;
//        }

//        int addedCount = 0;
//        int updatedCount = 0;
//        int assignedCount = 0;

//        foreach (Item item in items)
//        {
//            if (item == null) continue;

//            string itemName = item.name;
//            string nameKey = itemName + (addNameSuffix ? nameSuffix : "");
//            string descKey = itemName + (addDescriptionSuffix ? descriptionSuffix : "");

//            // Add or update item name
//            var nameEntry = tableCollection.SharedData.GetEntry(nameKey);
//            if (nameEntry == null)
//            {
//                tableCollection.SharedData.AddKey(nameKey);
//                addedCount++;
//            }
//            else
//            {
//                updatedCount++;
//            }

//            // Add or update item description
//            var descEntry = tableCollection.SharedData.GetEntry(descKey);
//            if (descEntry == null)
//            {
//                tableCollection.SharedData.AddKey(descKey);
//                addedCount++;
//            }
//            else
//            {
//                updatedCount++;
//            }

//            // Set values for each language in the collection
//            foreach (var table in tableCollection.StringTables)
//            {
//                // Add name
//                var nameTableEntry = table.GetEntry(nameKey);
//                if (nameTableEntry == null)
//                {
//                    table.AddEntry(nameKey, item.nameItem);
//                }
//                else if (string.IsNullOrEmpty(nameTableEntry.Value))
//                {
//                    nameTableEntry.Value = item.nameItem;
//                }

//                // Add description
//                var descTableEntry = table.GetEntry(descKey);
//                if (descTableEntry == null)
//                {
//                    table.AddEntry(descKey, item.description);
//                }
//                else if (string.IsNullOrEmpty(descTableEntry.Value))
//                {
//                    descTableEntry.Value = item.description;
//                }

//                EditorUtility.SetDirty(table);
//            }

//            // Auto-assign LocalizedStrings to Item
//            if (autoAssignLocalizedStrings)
//            {
//                SerializedObject serializedItem = new SerializedObject(item);

//                // Assign Name LocalizedString
//                SerializedProperty nameProp = serializedItem.FindProperty("NameLocalized");
//                if (nameProp != null)
//                {
//                    LocalizedString nameLocalizedString = new LocalizedString
//                    {
//                        TableReference = tableCollection.TableCollectionName,
//                        TableEntryReference = nameKey
//                    };
//                    nameProp.boxedValue = nameLocalizedString;
//                    assignedCount++;
//                }

//                // Assign Description LocalizedString
//                SerializedProperty descProp = serializedItem.FindProperty("DescriptionLocalized");
//                if (descProp != null)
//                {
//                    LocalizedString descLocalizedString = new LocalizedString
//                    {
//                        TableReference = tableCollection.TableCollectionName,
//                        TableEntryReference = descKey
//                    };
//                    descProp.boxedValue = descLocalizedString;
//                    assignedCount++;
//                }

//                serializedItem.ApplyModifiedProperties();
//                EditorUtility.SetDirty(item);
//            }
//        }

//        EditorUtility.SetDirty(tableCollection.SharedData);
//        AssetDatabase.SaveAssets();

//        string message = $"Keys added: {addedCount}\nExisting updated: {updatedCount}\nItems processed: {items.Count}";
//        if (autoAssignLocalizedStrings)
//        {
//            message += $"\nLocalizedStrings assigned: {assignedCount}";
//        }

//        EditorUtility.DisplayDialog("Success", message, "OK");

//        Debug.Log($"[ItemLocalizationEditor] Added {addedCount} new keys, updated {updatedCount} existing, assigned {assignedCount} LocalizedStrings");
//    }
//}