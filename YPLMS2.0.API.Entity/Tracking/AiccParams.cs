using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace YPLMS2._0.API.Entity
{
   [Serializable] public class AiccParams
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string LessonLocation { get; set; }
        public string Credit { get; set; }
        public string LessonStatus { get; set; }
        public string Score
        {
            get
            {
                if (!RawScore.HasValue) return String.Empty;
                if (!MaxScore.HasValue) return RawScore.Value.ToString(CultureInfo.InvariantCulture.NumberFormat);
                if (!MinScore.HasValue) return String.Format(CultureInfo.InvariantCulture.NumberFormat, "{0},{1}", RawScore, MaxScore);
                return String.Format(CultureInfo.InvariantCulture.NumberFormat, "{0},{1},{2}", RawScore, MaxScore, MinScore);
            } 
        }
        public AiccTime Time { get; set; }
        public string LessonMode { get; set; }
        public string SuspendData { get; set; }
        public string LaunchData { get; set; }
        public int? MasteryScore { get; set; }
        public string AudioPreference { get; set; }
        public AiccTime MaxTimeAllowed { get; set; }
        public string TimeLimitAction { get; set; }
        public string Identifier { get; set; }
        public decimal? RawScore { get; set; }
        public decimal? MinScore { get; set; }
        public decimal? MaxScore { get; set; }

        public AiccParams()
        {
        }

        public AiccParams(string hacpData)
        {
            //Regexes to match fields delimited by square brackets - watch those escapes
            Regex coreMatcher = new Regex("\\[Core\\]([^\\[]*)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex lessonMatcher = new Regex("\\[Core_Lesson\\]([^\\[]*)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            //Regexes for mandatory core components
            Regex locationMatcher = new Regex("^\\s*Lesson_Location\\s*=[ \t]*(.*?)\\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex statusMatcher = new Regex("^\\s*Lesson_Status\\s*=[ \t]*(.*?)\\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex timeMatcher = new Regex("^\\s*Time\\s*=\\s*(.*?)\\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex scoreMatcher = new Regex("^\\s*Score\\s*=[ \t]*(.*?)\\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            //Strip out comments (any line whose first non-space character is a semicolon)
            hacpData = Regex.Replace(hacpData, "\n\\s*;.*?\n", "\n", RegexOptions.Multiline | RegexOptions.Compiled);
            if (lessonMatcher.IsMatch(hacpData))
            {
                SuspendData = lessonMatcher.Match(hacpData).Groups[1].Value.Trim();
            }

            if (!coreMatcher.IsMatch(hacpData))
            {
                throw new ArgumentException("[Core] not found.");
            }
            string core = coreMatcher.Match(hacpData).Groups[1].Value;
            if (!statusMatcher.IsMatch(core)) 
            {
                throw new ArgumentException("[Core] does not contain lesson status.");
            }
            LessonStatus = statusMatcher.Match(core).Groups[1].Value;
            if (timeMatcher.IsMatch(core))
            {
                Time = new AiccTime(timeMatcher.Match(core).Groups[1].Value);
            }
            else
            {
                Time = new AiccTime();
            }
            if (scoreMatcher.IsMatch(core))
            {
                ParseScore(scoreMatcher.Match(core).Groups[1].Value);
            }
            if (locationMatcher.IsMatch(core))
            {
                LessonLocation = locationMatcher.Match(core).Groups[1].Value;
            }
        }

        private void ParseScore(string score)
        {
            if (String.IsNullOrEmpty(score)) return;
            var scoreParts = score.Split(new[] { ',' });
            decimal rawScore, minScore, maxScore;

            if (GetDecimal(scoreParts[0], out rawScore))
            {
                RawScore = rawScore;
            }
            else
            {
                throw new ArgumentException("Raw score must be a decimal number.");
            }
            if (scoreParts.Length == 1) return;
            if (GetDecimal(scoreParts[1], out maxScore))
            {
                MaxScore = maxScore;
            }
            else
            {
                throw new ArgumentException("Maximum score must be a decimal number.");
            }
            if (scoreParts.Length == 2) return;
            if (GetDecimal(scoreParts[2], out minScore))
            {
                MinScore = minScore;
            }
            else
            {
                throw new ArgumentException("Minimum score must be a decimal number.");
            }
        }

        private bool GetDecimal(string decimalString, out decimal decimalNumber)
        {
            return Decimal.TryParse(decimalString, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat,
                                    out decimalNumber);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[CORE]");
            sb.AppendLine("Student_ID=" + StudentId);
            sb.AppendLine("Student_Name=" + StudentName);
            sb.AppendLine("Lesson_Location=" + LessonLocation);
            sb.AppendLine("Credit=" + Credit);
            sb.AppendLine("Lesson_Status=" + LessonStatus);
            sb.AppendLine("Score=" + Score);
            sb.AppendLine("Time=" + Time);
            sb.AppendLine("Lesson_Mode=" + LessonMode);
            sb.AppendLine("[CORE_LESSON]");
            sb.AppendLine(SuspendData);
            sb.AppendLine("[CORE_VENDOR]");
            sb.AppendLine(LaunchData);
            sb.AppendLine("[Student_Data]");
            sb.AppendLine("Mastery_Score=" + MasteryScore);
            if (MaxTimeAllowed != null)
            {
                sb.AppendLine("Max_Time_Allowed=" + MaxTimeAllowed);
                sb.AppendLine("Time_Limit_Action=" + TimeLimitAction);
            }
            sb.AppendLine("[Student_Preferences]");
            sb.AppendLine("Audio=" + AudioPreference);
            return sb.ToString();
        }
    }
}