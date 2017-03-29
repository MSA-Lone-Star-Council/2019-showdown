using System;
using UIKit;

namespace Common.iOS
{
	public class UneditableTextField : UITextFieldDelegate
	{
		public override bool ShouldChangeCharacters(UITextField textField, Foundation.NSRange range, string replacementString)
		{
			return false;
		}

		public override bool ShouldReturn(UITextField textField)
		{
			return textField.ResignFirstResponder();
		}
	}
}
