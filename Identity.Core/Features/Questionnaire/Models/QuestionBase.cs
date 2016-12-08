namespace Identity.Core.Features.Questionnaire.Models
{
    public abstract class QuestionBase
    {
        public object Value { get; set; }
        public string Key { get; set; }
        public string Label { get; set; }
        public bool Required { get; set; }
        public int ControlType { get; set; }
    }
}
