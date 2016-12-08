namespace Identity.Core.Features.Questionnaire.Models
{
    public class CheckBoxQuestion : QuestionBase
    {
        public CheckBoxQuestion()
        {
            ControlType = (int)ControlTypeEnum.Checkbox;
        }
    }
}
