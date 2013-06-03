using System;

namespace NietzscheBiography.Domain.Models
{
    public partial class EventOccurrence
    {
        public override string ToString()
        {
            if (!this.Start.Date.HasValue || !this.Start.Precision.HasValue)
            {
                return string.Empty;
            }

            if (!this.End.Date.HasValue || !this.End.Precision.HasValue)
            {
                return this.Start.ToString();
            }

            DateTime startDate = this.Start.Date.Value;
            DateTime endDate = this.End.Date.Value;
            string startDateStringFull = this.Start.ToString();

            if (startDateStringFull == this.End.ToString())
            {
                return startDateStringFull;
            }

            ImpreciseDate.DatePartToOmit omit;

            // 15 May-June 2014
            // 2nd decade of 19th century-1832
            if (startDate.Year == endDate.Year)
            {
                if (startDate.Month == endDate.Month)
                {
                    if (startDate.Day == endDate.Day)
                    {
                        omit = ImpreciseDate.DatePartToOmit.OmitDay;
                    }
                    else
                    {
                        omit = ImpreciseDate.DatePartToOmit.OmitMonth;
                    }
                }
                else
                {
                    omit = ImpreciseDate.DatePartToOmit.OmitYear;
                }
            }
            else if (startDate.GetCentury() == endDate.GetCentury())
            {
                omit = ImpreciseDate.DatePartToOmit.OmitCentury;
            }
            else
            {
                omit = ImpreciseDate.DatePartToOmit.None;
            }

            string startDateString = ImpreciseDate.GetDateString(startDate, this.Start.Precision.Value, omit);

            return startDateString + "–" + this.End.ToString();
        }
    }
}