#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
using UnityEngine;
#endif
// Combines a quest, a quest's objective and Status
[System.Serializable]
public class ObjectiveTrigger
{
    // The quest that we're referring to
    public Quest quest;
    // The status we want to apply to the objective
    public Quest.Status statusToApply;
    // The location of this objective in the quest's objective list
    public int objectiveNumber;
    public void Invoke()
    {
        // Find the quest manager
        var manager = Object.FindObjectOfType<QuestManager>();
        // Tell it to update our objective
        manager.UpdateObjectiveStatus(quest, objectiveNumber, statusToApply);
    }
}
#if UNITY_EDITOR
// Custom property drawers override how a type of property appears in
// the Inspector.
[CustomPropertyDrawer(typeof(ObjectiveTrigger))]
public class ObjectiveTriggerDrawer : PropertyDrawer
{
    // Called when Unity needs to draw an ObjectiveTrigger property
    // in the Inspector.
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Wrap this in Begin/EndProperty to ensure that undo works
        // on the entire ObjectiveTrigger property
        EditorGUI.BeginProperty(position, label, property);
        // Get a reference to the three properties in the ObjectiveTrigger.
        var questProperty = property.FindPropertyRelative("quest");
        var statusProperty = property.FindPropertyRelative("statusToApply");
        var objectiveNumberProperty = property.FindPropertyRelative("objectiveNumber");
        // We want to display three controls:
        // - An Object field for dropping a Quest object into
        // - A Popup field for selecting a Quest.Status from
        // - A Popup field for selecting the specific objective from;
        //     it should show the name of the objective.
        // If no Quest has been specified, or if the Quest has no
        // objectives, the objective pop up should be empty and disabled.
        // Calculate the rectangles in which we're displaying.
        var lineSpacing = 2;
        // Calculate the rectangle for the first line
        var firstLinePos = position;
        firstLinePos.height = base.GetPropertyHeight(questProperty, label);
        // And for the second line (same as the first line, but shifted down one line)
        var secondLinePos = position;
        secondLinePos.y = firstLinePos.y + firstLinePos.height + lineSpacing;
        secondLinePos.height = base.GetPropertyHeight(statusProperty, label);
        // Repeat for the third line (same as the second line, but shifted down)
        var thirdLinePos = position;
        thirdLinePos.y = secondLinePos.y + secondLinePos.height + lineSpacing;
        thirdLinePos.height = base.GetPropertyHeight(objectiveNumberProperty, label);
        // Draw the quest and status properties, using the automatic property fields
        EditorGUI.PropertyField(firstLinePos, questProperty, new GUIContent("Quest"));
        EditorGUI.PropertyField(secondLinePos, statusProperty, new GUIContent("Status"));
        // Now we draw our custom property for the object. Draw a label on the 
        //   left-hand side, and get a new rectangle to draw the pop up in
        thirdLinePos = EditorGUI.PrefixLabel(thirdLinePos, new GUIContent("Objective"));
        // Draw the UI for choosing a property
        var quest = questProperty.objectReferenceValue as Quest;
        // Only draw this if we have a quest, and it has objectives
        if (quest != null && quest.objectives.Count > 0)
        {
            // Get the name of every objective, as an array
            var objectiveNames = quest.objectives.Select(o => o.name).ToArray();
            // Get the index of the currently selected objective
            var selectedObjective = objectiveNumberProperty.intValue;
            // If we somehow are referring to an object that's not
            // present in the list, reset it to the first objective
            if (selectedObjective >= quest.objectives.Count)
            {
                selectedObjective = 0;
            }
            // Draw the pop up, and get back the new selection
            var newSelectedObjective = EditorGUI.Popup(thirdLinePos, selectedObjective,
                                        objectiveNames);
            // If it was different, store it in the property
            if (newSelectedObjective != selectedObjective)
            {
                objectiveNumberProperty.intValue = newSelectedObjective;
            }
        }
        else
        {
            // Draw a disabled pop up as a visual placeholder
            using (new EditorGUI.DisabledGroupScope(true))
            {
                // Show a pop up with a single entry: the string "-".
                // Ignore its return value, since it's not interactive anyway.
                EditorGUI.Popup(thirdLinePos, 0, new[] { "-" });
            }
        }
        EditorGUI.EndProperty();
    } //End OnGUI
      // Called by Unity to figure out the height of this property.
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // The number of lines in this property
        var lineCount = 3;
        // The number of pixels in between each line
        var lineSpacing = 2;
        // The height of each line
        var lineHeight = base.GetPropertyHeight(property, label);
        // The height of this property is the number of lines times the
        // height of each line, plus the spacing in between each line
        return (lineHeight * lineCount) + (lineSpacing * (lineCount - 1));
    }
} //End of ObjectiveTriggerDrawer
#endif