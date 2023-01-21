using DigiD.Enums;

namespace DigiD.Models
{
    internal class EditInfoModel
    {
        public EditInfoModel(char changedChar, EditTypeEnum editType, int cursorPosition)
        {
            ChangedChar = changedChar;
            EditType = editType;
            CursorPosition = cursorPosition;
        }
        public char ChangedChar { get; set; }
        public EditTypeEnum EditType { get; set; }
        public int CursorPosition { get; set; }

        public override string ToString()
        {
            return $"\n====> EditInfoModel." +
                $"\n\tChangedChar:    {ChangedChar}" +
                $"\n\tEditType:       {EditType}" +
                $"\n\tCursorPostion:  {CursorPosition}";
        }
    }
}
