using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity 
{
 [Serializable] public class BackgroundBRuleAssignment:BaseEntity
    {

      public BackgroundBRuleAssignment(string pstrActivityId, string pSysteMUserGuId, string  pActivityType)
      {
          this.ActivityId = pstrActivityId;
          this.SysteMUserGuId = pSysteMUserGuId;
          this.ActivityType = pActivityType;
      }

      private string _ActivityId;

      public string ActivityId
      {
          get { return _ActivityId; }
          set { _ActivityId = value; }
      }
      private string _SysteMUserGuId;

      public string SysteMUserGuId
      {
          get { return _SysteMUserGuId; }
          set { _SysteMUserGuId = value; }
      }
      private string _ActivityType;

      public string ActivityType
      {
          get { return _ActivityType; }
          set { _ActivityType = value; }
      }

    }
}
