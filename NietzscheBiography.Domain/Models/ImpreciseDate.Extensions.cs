using NietzscheBiography.Domain.Resources;
using System;

namespace NietzscheBiography.Domain.Models
{
    public partial class ImpreciseDate
    {
        public enum DatePartToOmit
        {
            None = 0,
            OmitCentury,
            OmitYear,
            OmitMonth,
            OmitDay
        }

        public override string ToString()
        {
            return this.Date.HasValue && this.Precision.HasValue
                ? GetDateString(this.Date.Value, this.Precision.Value)
                : string.Empty;
        }

        internal static string GetDateString(DateTime date, DateTimePrecision precision, DatePartToOmit omit = DatePartToOmit.None)
        {
            switch (precision)
            {
                case DateTimePrecision.Day:
                    switch (omit)
                    {
                        case DatePartToOmit.OmitMonth:
                            return date.ToString("%d");

                        case DatePartToOmit.OmitYear:
                            return date.ToString("d MMMM");

                        default:
                            return date.ToString("d MMMM yyyy");
                    }

                case DateTimePrecision.Month:
                    switch (omit)
                    {
                        case DatePartToOmit.OmitYear:
                            return date.ToString("MMMM");

                        default:
                            return date.ToString("MMMM yyyy");
                    }

                case DateTimePrecision.Season:
                    switch (omit)
                    {
                        case DatePartToOmit.OmitYear:
                            return Texts.ResourceManager.GetString(date.GetSeason().ToString());

                        default:
                            return Texts.ResourceManager.GetString(date.GetSeason().ToString()) + " " + date.Year;
                    }

                case DateTimePrecision.Year:
                    return date.Year.ToString();

                default:
                    throw new UnknownDateTimePrecisionException(precision.ToString());
            }
        }
    }
}