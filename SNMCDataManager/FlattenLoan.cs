using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SNMCDataManager
{
    public class FlattenLoan
    {
        public string LoanNumber {get; set;}
        public string OriginatorName { get; set; }
        public string OriginatorNMLSID { get;set;}
        public string RetailOriginator { get; set; }
        public string RetailOriginatorNMLSID {get; set;}
        public string LoanProgramName {get; set;}
        public string BorrowerLastName {get;set;}
        public string AdjustedNoteAmount { get;set;}
        public string NoteRate {get; set; }
        public string LoanPurpose {get; set; }
        public string CurrentStatus { get; set; }
        public string DecisionStatus {get; set;}
        public string ReceiveDate {get; set; }
        public string LockExpireDate { get; set; }
        public string EstClosingDate {get;set;}
        public string UnderwriterName {get; set;}
        public string ChannelType { get; set; }
    }
}
