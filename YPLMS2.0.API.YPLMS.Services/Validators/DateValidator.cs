using System;
using System.Globalization;
using System.Text.RegularExpressions;
using YPLMS2._0.API.YPLMS.Services.Messages;


namespace YPLMS2._0.API.YPLMS.Services.Validators
{
	public class DateValidator : IValidator 
	{
		private DateTime _minDate = DateTime.MinValue;
		private DateTime _maxDate = DateTime.MaxValue;
		private DateTime _dateToValidate = DateTime.MinValue;
		private String _valueToValidate = String.Empty;
		private String _dateFormat = String.Empty;
		private String _dateSeparator = "/";
		private Boolean _isValid = false;
		private Boolean _dateInitalized = false;
		
		public string DateFormat
		{
			get { return _dateFormat; }
			set 
			{
				_dateFormat = value;
				InitializeDateFromString(_valueToValidate); // make sure the date string is still valid;
			}
		}

		public String ValueToValidate
		{
			get { return _valueToValidate; }
			set
			{
				// reset the DateTime variable to keep String and Date in Sync.
				_dateToValidate = DateTime.MinValue;
				_dateInitalized = false;

				InitializeDateFromString(value);
			}
		}

		private void InitializeDateFromString(string dateString)
		{
			if (String.IsNullOrEmpty(_dateFormat))
			{
				// save the value but don't initialize  we'll check it later when we get the date format
				_valueToValidate = dateString;
			}
			else if (!String.IsNullOrEmpty(dateString))
			{
				SetDateSeparator(dateString);

				if (dateString.Length < 10)
				{
					dateString = AddLeadingDigits(dateString);
				}

				// validate format
				if (ValidatePreferredFormat(dateString))
				{
					// validate date
					if (DateTime.TryParseExact(dateString, _dateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out _dateToValidate))
					//if (DateTime.TryParse(value, out _dateToValidate))
					{
						_valueToValidate = dateString;
						_dateInitalized = true;
					}
				}
			}
		}


		public DateTime DateToValidate
		{
			get { return _dateToValidate; }
			set
			{
				_dateToValidate = value;
				_dateInitalized = true;

				// reset the String variable to keep String and Date in Sync.
				String formatString = DateFormat.Replace("MM", "{0}");
				formatString = formatString.Replace("dd", "{1}");
				formatString = formatString.Replace("yyyy", "{2}");

				_valueToValidate = String.Format(formatString, value.Month, value.Day, value.Year);
			}
		}

		public Boolean IsValid
		{
			get { return _isValid; }
		}

		public DateTime MinDate
		{
			get { return _minDate; }
			set { _minDate = value; }
		}

		public DateTime MaxDate
		{
			get { return _maxDate; }
			set { _maxDate = value; }
		}

		public DateValidator(String dateToValidate)
		{
			ValueToValidate = dateToValidate; // use field so that date value is also set
			_isValid = Validate();
		}

		public Boolean Validate()
		{

			if (_dateInitalized) { 
				_isValid = (DateTime.Compare(_minDate, _dateToValidate) <= 0 && DateTime.Compare(_maxDate, _dateToValidate) >= 0);
			}
			return _isValid;

		}

		private Boolean ValidatePreferredFormat(String formattedDate)
		{
			Boolean validFormat = false;

			String pattern = GetRegExValidationPatternForDateFormat(_dateFormat);
			if (!String.IsNullOrEmpty(pattern) && !String.IsNullOrEmpty(formattedDate))
			{
				if (Regex.IsMatch(formattedDate, pattern))
					validFormat = true;
			}

			return validFormat;
		}

		public string GetRegExValidationPatternForDateFormat(string preferredDateFormat)
		{
			string regExString = "";

			switch (preferredDateFormat)
			{
				// Regex matches a date in yyyy-mm-dd format from between 1900-01-01 and 2099-12-31, with a choice of three separators
				// year  (19|20)?\d\d  century is optional, asssume 20th if omitted
				// month (0{0,1}[1-9]|1[012])  leading zero is optional
				// day  (0{0,1}[1-9]?|[12][0-9]|3[01]) leading zero is optional 
				// separator  ([-/.]) \2 requires the second separator to match the first seperator
				case "dd/MM/yyyy":
				case "dd.MM.yyyy":
				case "dd-MM-yyyy":
//					regExString = @"^(0{0,1}[1-9]?|[12][0-9]|3[01])([-./])(0{0,1}[1-9]|1[012])\2(19|20)?\d\d$";
					regExString = @"^(0[1-9]?|[12][0-9]|3[01])([-./])(0[1-9]|1[012])\2(19|20)?\d\d$";
					break;
				case "MM/dd/yyyy":
				case "MM-dd-yyyy":
				case "MM.dd.yyyy":
//					regExString = @"^(0{0,1}[1-9]|1[012])([-./])(0{0,1}[1-9]?|[12][0-9]|3[01])\2(19|20)?\d\d$";
					regExString = @"^(0[1-9]|1[012])([-./])(0[1-9]?|[12][0-9]|3[01])\2(19|20)?\d\d$";
					break;
				case "yyyy-MM-dd":
				case "yyyy/MM/dd":
				case "yyyy.MM.dd":
					regExString = @"^(19|20)?\d\d([-./])(0[1-9]|1[012])\2(0[1-9]?|[12][0-9]|3[01])$";
					break;
				default:
					break;
			}

			// remove extra separators from regExString, does it really matter which separator is used?
			regExString = regExString.Replace("[-./]", _dateSeparator);
			return regExString;
		}


		private string AddLeadingDigits(string value)
		{
			String[] dateparts = value.Split(Convert.ToChar(_dateSeparator));
			String[] dateFormatParts = _dateFormat.Split(Convert.ToChar(_dateSeparator));

			if (dateFormatParts.Length == dateparts.Length)
			{
				for (int i = 0; i < dateparts.Length; i++)
				{
					int padLength = dateFormatParts[i].Length - dateparts[i].Length;
					if (padLength > 0)
					{
						if (dateFormatParts[i].Equals("yyyy", StringComparison.CurrentCultureIgnoreCase))
						{
							dateparts[i] = "20" + dateparts[i];
						}
						else
						{
							dateparts[i] = dateparts[i].PadLeft(padLength+1, '0');
						}
					}
				}
			}
			return String.Join(_dateSeparator, dateparts);
		}

		private void SetDateSeparator(string value)
		{
			if (value.Contains("/"))
			{
				_dateSeparator = "/";
			}

			else if (value.Contains("."))
			{
				_dateSeparator = ".";
			}

			else if (value.Contains("-"))
			{
				_dateSeparator = "-";
			}
		}

		public string ValidatorName
		{
			get {return  "DateValidator";} 
		}

		//public ImportDefinition.ValueType ValidatorDataType
		//{
		//	get { return ImportDefination.ValueType.Date; }
		//}

	}
}