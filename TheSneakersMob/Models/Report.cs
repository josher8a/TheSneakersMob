using System;
using System.Collections.Generic;
using TheSneakersMob.Models.Common;

namespace TheSneakersMob.Models
{
    public class Report : ValueObject
    {
        public const int BannedDays = 14;
        public Reason Reason { get; private set; }
        public Severity Severity { get; private set; }
        public DateTime Date { get; private set; }
        public Client Reporter { get; private set; }

        private Report()
        {
            
        }

        private Report(Reason reason, Severity severity, Client reporter)
        {
            Reason = reason;
            Severity = severity;
            Date = DateTime.Now;
            Reporter = reporter;
        }

        public static Report Create(Reason reason, Client reporter) =>
            reason switch
            {
                Reason.Bots => new Report(Reason.Bots, Severity.Low, reporter),
                Reason.MysteryBox => new Report(Reason.MysteryBox, Severity.Low, reporter),
                Reason.NotStreetWear => new Report(Reason.NotStreetWear, Severity.Low, reporter),
                Reason.Raffle => new Report(Reason.Raffle, Severity.Low, reporter),
                Reason.Others => new Report(Reason.Others, Severity.High, reporter),
                Reason.Fakes => new Report(Reason.Fakes, Severity.High, reporter),
                Reason.Spamming => new Report(Reason.Spamming, Severity.High, reporter),
                Reason.Offensive => new Report(Reason.Offensive, Severity.High, reporter),
                _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(reason))
            };

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Reason;
            yield return Severity;
            yield return Date;
            yield return Reporter;
        }
    }
    public enum Reason
    {
        Bots = 0,
        Fakes = 1,
        MysteryBox = 2,
        NotStreetWear = 3,
        Raffle = 4,
        Spamming = 5,
        Offensive = 6,
        Others = 7
    }

    public enum Severity
    {
        High = 0,
        Low = 1
    }
}