using System;

namespace YPLMS2._0.API.YPLMS.Services.Validators
{
	public interface IValidator
	{
		String ValueToValidate { get; set; }
		Boolean IsValid { get;  }
		Boolean Validate();
	}
}