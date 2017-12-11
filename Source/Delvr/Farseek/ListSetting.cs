using HugsLib.Settings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Delvr.Farseek
{
    public abstract class ListSetting<T>
    {
        private SettingHandle<string> handler;
        private Dictionary<string, T> keyedValues;
        private List<T> sortedValues;

        protected ListSetting(ModSettingsPack settings, string name, string title, string description, string defaultValueKey, List<T> validValues)
        {
            keyedValues = validValues.ToDictionary(value => Key(value), value => value);
            sortedValues = validValues.OrderBy(value => Label(value)).ToList();
            handler = settings.GetHandle(name, title, description, defaultValueKey, keyedValues.ContainsKey);
            handler.CustomDrawer = DrawDropDown;
        }

        protected abstract string Key(T value);
        protected abstract string Label(T value);

        public T Value() => keyedValues[handler.Value];

        private bool valueChanged = false;

        private bool DrawDropDown(Rect controlRect)
        {
            if (Widgets.ButtonText(controlRect, Label(Value())))
            {
                var floatOptions = sortedValues.Select(value => new FloatMenuOption(Label(value), () => {
                    handler.StringValue = Key(value);
                    valueChanged = true;
                }));
                Find.WindowStack.Add(new FloatMenu(floatOptions.ToList()));
            }
            if (valueChanged)
            {
                valueChanged = false;
                return true;
            }
            return false;
        }
    }

    public class DefListSetting<T> : ListSetting<T> where T : Def, new()
    {
        public DefListSetting(ModSettingsPack settings, string name, string title, string description, string defaultValueKey, List<T> values) : 
            base(settings, name, title, description, defaultValueKey, values) { }

        public DefListSetting(ModSettingsPack settings, string name, string title, string description, string defaultValueKey) : 
            this(settings, name, title, description, defaultValueKey, DefDatabase<T>.AllDefsListForReading) { }

        protected override string Key(T def) => def.defName;

        protected override string Label(T def) => def.LabelCap;
    }
}
