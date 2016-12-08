import { QuestionBase } from './questionBase.model';

export class TextboxQuestion extends QuestionBase<string> {
  controlType = 'textbox';

  constructor(options: {} = {}) {
    super(options);
  }
}