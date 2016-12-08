using Identity.Core.Features.Questionnaire.Models;
using System;

namespace Identity.Core.Features.Questionnaire.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class QuestionAttribute: Attribute
    {
        public string Label { get; set; }
        public ControlTypeEnum ControlType { get; protected set; }
        public bool Required { get; set; }
        public object DefaultValue { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DropDownQuestionAttribute : QuestionAttribute
    {
        public string OptionsUrl { get; set; }

        public DropDownQuestionAttribute()
        {
            ControlType = ControlTypeEnum.Dropdown;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class TextBoxQuestionAttribute : QuestionAttribute
    {
        public string Pattern { get; set; }

        public TextBoxQuestionAttribute()
        {
            ControlType = ControlTypeEnum.Textbox;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CheckBoxQuestionAttribute : QuestionAttribute
    {
        public CheckBoxQuestionAttribute()
        {
            ControlType = ControlTypeEnum.Checkbox;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DatePickerQuestionAttribute : QuestionAttribute
    {
        public DatePickerQuestionAttribute()
        {
            ControlType = ControlTypeEnum.DatePicker;
        }
    }
}
