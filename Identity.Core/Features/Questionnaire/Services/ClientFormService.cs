using Common.Core.Models;
using Identity.Core.Features.Questionnaire.Attributes;
using Identity.Core.Features.Questionnaire.Models;
using Identity.Core.Features.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Identity.Core.Features.Questionnaire.Services
{
    public class FormService
    {
        public IEnumerable<QuestionBase> GetForm(Type modelType)
        {
            return modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(it => it.Name != nameof(BaseModel.Id))
                .Select(GetQuestion)
                .Where(it => it != null);
        }

        private QuestionBase GetQuestion(PropertyInfo propertyTypeInfo)
        {
            var attr = propertyTypeInfo.GetCustomAttribute<QuestionAttribute>();

            if (attr == null)
            {
                return null;
            }

            switch (attr.ControlType)
            {
                case ControlTypeEnum.Textbox:
                    var textBoxAttr = (TextBoxQuestionAttribute)attr;
                    return new TextboxQuestion
                    {
                        Label = textBoxAttr.Label,
                        Required = textBoxAttr.Required,
                        Key = GetFormattedPropertyName(propertyTypeInfo.Name),
                        Pattern = textBoxAttr.Pattern,
                        Value = textBoxAttr.DefaultValue
                    };
                case ControlTypeEnum.Dropdown:
                    var dropDownAttr = (DropDownQuestionAttribute)attr;
                    return new DropdownQuestion
                    {
                        Label = dropDownAttr.Label,
                        Required = dropDownAttr.Required,
                        Key = GetFormattedPropertyName(propertyTypeInfo.Name),
                        OptionsUrl = dropDownAttr.OptionsUrl,
                        Value = dropDownAttr.DefaultValue
                    };
                case ControlTypeEnum.DatePicker:
                    var datePickerAttr = (DatePickerQuestionAttribute)attr;
                    return new DatePickerQuestion
                    {
                        Label = datePickerAttr.Label,
                        Required = datePickerAttr.Required,
                        Key = GetFormattedPropertyName(propertyTypeInfo.Name),
                        Value = datePickerAttr.DefaultValue
                    };
                case ControlTypeEnum.Checkbox:
                    var checkBoxAttr = (CheckBoxQuestionAttribute)attr;
                    return new CheckBoxQuestion
                    {
                        Label = checkBoxAttr.Label,
                        Required = checkBoxAttr.Required,
                        Key = GetFormattedPropertyName(propertyTypeInfo.Name),
                        Value = checkBoxAttr.DefaultValue
                    };
                default:
                    return null;
            }
        }

        private string GetFormattedPropertyName(string propertyName)
        {
            return string.Concat(propertyName.ToLower()[0], propertyName.Substring(1, propertyName.Length - 1));
        }
    }
}
