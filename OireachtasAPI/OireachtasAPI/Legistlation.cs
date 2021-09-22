using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OireachtasAPI
{
    public class Legistlation
    {
        public object head { get; set; }

        public List<Result> results { get; set; }
    }

    public class Result
    {
        public Bill bill { get; set; }

        public object billSort { get; set; }

        public string contextDate { get; set; }
  
    }

    public class Chamber
    {
        public string showAs { get; set; }
        public string uri { get; set; }
        public string chamberCode { get; set; }
    }

    public class Debate
    {
        public Chamber chamber { get; set; }
        public string date { get; set; }
        public string debateSectionId { get; set; }
        public string showAs { get; set; }
        public string uri { get; set; }
    }

    public class Date
    {
        public string date { get; set; }
    }

    public class House
    {
        public string chamberCode { get; set; }
        public string chamberType { get; set; }
        public string houseCode { get; set; }
        public string houseNo { get; set; }
        public string showAs { get; set; }
        public string uri { get; set; }
    }

    public class Event2
    {
        public Chamber chamber { get; set; }
        public List<Date> dates { get; set; }
        public House house { get; set; }
        public int progressStage { get; set; }
        public string showAs { get; set; }
        public bool stageCompleted { get; set; }
        public object stageOutcome { get; set; }
        public string stageURI { get; set; }
        public string uri { get; set; }
    }

    public class Event
    {
        public Event @event { get; set; }
    }

    public class MostRecentStage
    {
        public Event @event { get; set; }
    }

    public class OriginHouse
    {
        public string showAs { get; set; }
        public string uri { get; set; }
    }

    public class As
    {
        public object showAs { get; set; }
        public object uri { get; set; }
    }

    public class By
    {
        public string showAs { get; set; }
        public string uri { get; set; }
    }

    public class Sponsor2
    {
        public As @as { get; set; }
        public By by { get; set; }
    }

    public class Sponsor
    {
        public Sponsor2 sponsor { get; set; }
    }

    public class Stage
    {
        public Event @event { get; set; }
    }

    public class Pdf
    {
        public string uri { get; set; }
    }

    public class Formats
    {
        public Pdf pdf { get; set; }
        public object xml { get; set; }
    }

    public class Version2
    {
        public string date { get; set; }
        public string docType { get; set; }
        public Formats formats { get; set; }
        public string lang { get; set; }
        public string showAs { get; set; }
        public string uri { get; set; }
    }

    public class Version
    {
        public Version version { get; set; }
    }

    public class Bill
    {
        public object act { get; set; }
        public List<object> amendmentLists { get; set; }
        public string billNo { get; set; }
        public string billType { get; set; }
        public string billTypeURI { get; set; }
        public string billYear { get; set; }
        public List<Debate> debates { get; set; }
        public List<Event> events { get; set; }
        public DateTime lastUpdated { get; set; }
        public string longTitleEn { get; set; }
        public string longTitleGa { get; set; }
        public string method { get; set; }
        public string methodURI { get; set; }
        public MostRecentStage mostRecentStage { get; set; }
        public OriginHouse originHouse { get; set; }
        public string originHouseURI { get; set; }
        public List<object> relatedDocs { get; set; }
        public string shortTitleEn { get; set; }
        public string shortTitleGa { get; set; }
        public string source { get; set; }
        public string sourceURI { get; set; }
        public List<Sponsor> sponsors { get; set; }
        public List<Stage> stages { get; set; }
        public string status { get; set; }
        public string statusURI { get; set; }
        public string uri { get; set; }
        public List<Version> versions { get; set; }
    }




}
