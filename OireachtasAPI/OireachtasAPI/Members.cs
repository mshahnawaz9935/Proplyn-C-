using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OireachtasAPI
{
    public class Counts
    {
        public int memberCount { get; set; }
        public int resultCount { get; set; }
    }

    public class DateRange
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }

    public class Head
    {
        public Counts counts { get; set; }
        public DateRange dateRange { get; set; }
        public string lang { get; set; }
    }

    public class Represent2
    {
        public string representCode { get; set; }
        public string showAs { get; set; }
        public string uri { get; set; }
        public string representType { get; set; }
    }

    public class Represent
    {
        public Represent represent { get; set; }
    }

    public class OfficeName
    {
        public string showAs { get; set; }
        public object uri { get; set; }
    }

    public class Office2
    {
        public OfficeName officeName { get; set; }
        public DateRange dateRange { get; set; }
    }

    public class Office
    {
        public Office office { get; set; }
    }

    public class Party2
    {
        public string showAs { get; set; }
        public DateRange dateRange { get; set; }
        public string partyCode { get; set; }
        public string uri { get; set; }
    }

    public class Party
    {
        public Party party { get; set; }
    }

    public class House2
    {
        public string showAs { get; set; }
        public string chamberType { get; set; }
        public string houseCode { get; set; }
        public string houseNo { get; set; }
        public string uri { get; set; }
    }

    public class Membership2
    {
        public List<Represent> represents { get; set; }
        public DateRange dateRange { get; set; }
        public List<Office> offices { get; set; }
        public List<Party> parties { get; set; }
        public string uri { get; set; }
        public House2 house { get; set; }
    }

    public class Membership
    {
        public Membership membership { get; set; }
    }

    public class Member
    {
        public string memberCode { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string gender { get; set; }
        public object dateOfDeath { get; set; }
        public string fullName { get; set; }
        public string pId { get; set; }
        public object wikiTitle { get; set; }
        public string uri { get; set; }
        public List<Membership> memberships { get; set; }
    }

    public class Result2
    {
        public Member member { get; set; }
    }

    public class Members
    {
        public Head head { get; set; }
        public List<Result2> results { get; set; }
    }


}
