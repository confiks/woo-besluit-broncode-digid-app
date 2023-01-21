﻿using System;
using System.Globalization;
using DigiD.Common.Controls;
using DigiD.Enums;
using DigiD.Models;
using Xamarin.Forms;

namespace DigiD.Behaviors
{
    public class DateEntryBehavior : Behavior<CustomEntryField>
    {
        protected override void OnAttachedTo(CustomEntryField customEntryField)
        {
            base.OnAttachedTo(customEntryField);
            customEntryField.Entry.TextChanged += Entry_TextChanged;
        }

        protected override void OnDetachingFrom(CustomEntryField customEntryField)
        {
            base.OnDetachingFrom(customEntryField);
            customEntryField.Entry.TextChanged -= Entry_TextChanged;
        }

        // uitgaande van nederlands of engels dateformat
        // nl: "dd-MM-yyyy" of
        // en: "MM/dd/yyyy"
        private int _resetCursorToPosition = -1;
        public void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry)
            {
                if (_resetCursorToPosition >= 0) // alleen herpositioneren van cursor
                {
                    entry.CursorPosition = _resetCursorToPosition;
                    _resetCursorToPosition = -1;
                    return;
                }

                var dateSeparator = CultureInfo.DefaultThreadCurrentCulture.DateTimeFormat.DateSeparator;
                var editInfo = GetEditInfo(entry, e.NewTextValue, e.OldTextValue);
                var cp = editInfo.CursorPosition;
                var length = e.NewTextValue.Length;

                //breakpoint printmessage: DateEntryBehavior.EntryTextChanged()-->  editInfoModel} -- newValue: '{e.NewTextValue}'-- oldValue: '{e.OldTextValue}'-- entry.cp: {entry.CursorPosition}
                if (editInfo.ChangedChar != dateSeparator[0] && !"0123456789".Contains(editInfo.ChangedChar))
                {
                    entry.Text = e.OldTextValue ?? "";
                    _resetCursorToPosition = cp;
                }
                else switch (editInfo.EditType)
                {
                    case EditTypeEnum.Added:
                        HandleAddedCharacter(e, entry, dateSeparator, cp, length);
                        break;
                    case EditTypeEnum.Deleted:
                        HandleDeletedCharacter(e, entry, dateSeparator, editInfo);
                        break;
                }
            }
        }

        private void HandleDeletedCharacter(TextChangedEventArgs e, InputView entry, string dateSeparator, EditInfoModel editInfoModel)
        {
            if (editInfoModel.ChangedChar == dateSeparator[0])
            {
                if (editInfoModel.CursorPosition == 0)
                {
                    entry.Text = "";
                }
                else
                {
                    entry.Text = e.OldTextValue.Substring(0, editInfoModel.CursorPosition - 1) +
                        (editInfoModel.CursorPosition < e.OldTextValue.Length - 1
                        ? e.OldTextValue.Substring(editInfoModel.CursorPosition + 1)
                        : "");
                }
                _resetCursorToPosition = editInfoModel.CursorPosition - 1;
            }
        }

        private void HandleAddedCharacter(TextChangedEventArgs e, InputView entry, string dateSeparator, int cp, int length)
        {
            if (cp == 1 || cp == 4)
            {
                if (length == cp + 1) //karakter wordt aan het eind toegevoegd
                    entry.Text = e.NewTextValue + dateSeparator;
                else if (length > cp + 1 && e.NewTextValue.Substring(cp + 1, 1) != dateSeparator)
                    entry.Text = e.NewTextValue[..(cp + 1)] + dateSeparator + e.NewTextValue[(cp + 1)..];
                _resetCursorToPosition = cp + 2;
            }
            else if ((cp == 2 || cp == 5) && length > cp + 1)
            {
                if (e.NewTextValue.Substring(cp + 1, 1) != dateSeparator)
                    entry.Text = e.NewTextValue[..cp] + dateSeparator + e.NewTextValue[cp..];
                _resetCursorToPosition = cp + 1;
            }
        }

        internal static EditInfoModel GetEditInfo(Entry entry, string newValue, string oldValue)
        {
            switch (oldValue)
            {
                case null when newValue.Length == 0:
                    return new EditInfoModel(' ', EditTypeEnum.NoChange, entry.CursorPosition);
                // begin situatie, newValue is 1 lang, oldValue is null, added first character
                case null:
                    return new EditInfoModel(newValue[0], EditTypeEnum.Added, entry.CursorPosition);
            }

            if (newValue == oldValue)
            {
                return new EditInfoModel(' ', EditTypeEnum.NoChange, newValue.Length);
            }

            if (newValue.Length == oldValue.Length)
            {
                var index = entry.CursorPosition >= newValue.Length ? newValue.Length - 1 : entry.CursorPosition;
                return new EditInfoModel(newValue[index], EditTypeEnum.Changed, entry.CursorPosition);
            }

            if (newValue.Length > oldValue.Length)
            {
                var index = entry.CursorPosition >= newValue.Length ? newValue.Length - 1 : entry.CursorPosition;
                return new EditInfoModel(newValue[index], EditTypeEnum.Added, entry.CursorPosition);
            }

            switch (newValue.Length < oldValue.Length)
            {
                case true:
                {
                    var subtract = entry.CursorPosition > 0 ? 1 : 0;
                    return new EditInfoModel(oldValue.ToCharArray()[entry.CursorPosition - subtract], EditTypeEnum.Deleted, entry.CursorPosition - subtract);
                }
                default:
                    return new EditInfoModel(' ', EditTypeEnum.NoChange, entry.CursorPosition);
            }
        }
    }
}
