using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IGroupRuleAdaptor<T>
    {
        GroupRule GetRuleByName(GroupRule pEntGroupRule);
        GroupRule GetRuleIdByName(GroupRule pEntGroupRule);
        GroupRule EditRule(GroupRule pEntGroupRule);
        GroupRule GetRuleIdByParentId(GroupRule pEntGroupRule);
        GroupRule AddSingleRule(GroupRule pEntGroupRule);
        GroupRule UpdateSingleRule(GroupRule pEntGroupRule);
        List<GroupRule> GetGroupRuleList(GroupRule pEntGroupRule);
        List<GroupRule> GetGroupRuleList_IPerform(GroupRule pEntGroupRule);
        List<GroupRule> DeactivateGroupRule(List<GroupRule> pEntListGroupRule);
        List<GroupRule> ActivateGroupRule(List<GroupRule> pEntListGroupRule);
        List<GroupRule> DeleteGroupRule(List<GroupRule> pEntListGroupRule);
        List<GroupRule> GetGroupRuleListByUserId(GroupRule pEntGroupRule);
        List<GroupRule> GetGroupRuleListForDistribution(GroupRule pEntGroupRule);
        GroupRule GetRuleByID(GroupRule pEntGroupRule);
        string CheckBusinessRuleNameExists(string NewName, int NoOfCopy, string originalName, GroupRule groupRule);
    }
}
