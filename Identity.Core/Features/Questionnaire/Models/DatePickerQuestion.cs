namespace Identity.Core.Features.Questionnaire.Models
{
    public class DatePickerQuestion : QuestionBase
    {
        public DatePickerQuestion()
        {
            ControlType = (int)ControlTypeEnum.DatePicker;
        }
    }
}
