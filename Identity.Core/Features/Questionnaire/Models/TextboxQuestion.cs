namespace Identity.Core.Features.Questionnaire.Models
{
    public class TextboxQuestion : QuestionBase
    {
        public string Pattern { get; set; }

        public TextboxQuestion()
        {
            ControlType = (int)ControlTypeEnum.Textbox;
        }
    }
}
