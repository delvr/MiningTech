using System.Collections.Generic;
using System.Linq;
using HugsLib.Settings;
using UnityEngine;
using Verse;

namespace MiningTech
{
    public abstract class ListSetting<T> {
        private readonly SettingHandle<string> _handler;
        private readonly Dictionary<string, T> _keyedValues;
        private readonly List<T> _sortedValues;

        protected ListSetting(ModSettingsPack settings, string name, string title, string description, string defaultKey,
                              IReadOnlyCollection<T> validValues) {
            _keyedValues  = validValues.ToDictionary(Key, value => value);
            _sortedValues = validValues.OrderBy(Label).ToList();
            _handler = settings.GetHandle(name, title, description, defaultKey, _keyedValues.ContainsKey);
            _handler.CustomDrawer = DrawDropDown;
        }

        protected abstract string Key(T value);
        protected abstract string Label(T value);

        public T Value() => _keyedValues[_handler.Value];

        private bool _valueChanged;

        private bool DrawDropDown(Rect controlRect) {
            if(Widgets.ButtonText(controlRect, Label(Value()))) {
                var floatOptions = _sortedValues.Select(value => new FloatMenuOption(Label(value), () => {
                    _handler.StringValue = Key(value);
                    _valueChanged = true;
                }));
                Find.WindowStack.Add(new FloatMenu(floatOptions.ToList()));
            }
            if(!_valueChanged)
                return false;
            _valueChanged = false;
            return true;
        }
    }

    public class DefListSetting<T>: ListSetting<T> where T: Def, new() {
        public DefListSetting(ModSettingsPack settings, string name, string title, string description, string defaultKey):
            base(settings, name, title, description, defaultKey, DefDatabase<T>.AllDefsListForReading) {}
        protected override string Key  (T def) => def.defName;
        protected override string Label(T def) => def.LabelCap;
    }
}
