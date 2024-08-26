﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5GenralML
{
    public class WorkFlowData
    {
        public int WorkFlowDataId { get; set; }
        public int WorkFlowId { get; set; }
        public string Channel { get; set; }
        public string ChannelName { get; set; }
        public int ConfigId { get; set; }
        public string Segment { get; set; }
        public string SegmentId { get; set; }
        public string Rules { get; set; }
        public int RulesId { get; set; }
        public bool IsRuleSatisfy { get; set; }
        public string PreChannel { get; set; }
        public int PreConfigId { get; set; }
        public string Condition { get; set; }
        public int Time { get; set; }
        public string Date { get; set; }
        public DateTime? DateValue { get; set; }
        public DateTime? DateValueTo { get; set; }
        public int DateCondition { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsGroupOrIndividual { get; set; }
        public bool IsBelongToGroup { get; set; }
        public string ConfigName { get; set; }
        public string SegmentName { get; set; }
        public string RulesName { get; set; }
        public string TimeType { get; set; }
        public bool ChannelStatus { get; set; }
        public int TitleId { get; set; }
        public string DaysOfWeek { get; set; }
        public Int16 DaysOfMonth { get; set; }
        public bool IsDynamicGroup { get; set; }
        public string SlotTime { get; set; }
        public string SlotTime1 { get; set; }
    }
}
