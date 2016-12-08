using System.Collections.Generic;

namespace Identity.Core.Features.Questionnaire.Models
{
    public class DropdownQuestion: QuestionBase
    {
        public string OptionsUrl { get; set; }

        public DropdownQuestion()
        {
            ControlType = (int)ControlTypeEnum.Dropdown;
        }
    }
}
