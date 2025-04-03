// namespace sps.Domain.Model.ValueObjects
// {
//     public class TimeRange
//     {
//         public DateTime Start { get; private set; }
//         public DateTime End { get; private set; }

//         public TimeRange(DateTime start, DateTime end)
//         {
//             if (end <= start)
//             {
//                 throw new ArgumentException("End time must be after start time");
//             }

//             Start = start;
//             End = end;
//         }

//         public bool OverlapsWith(TimeRange other)
//         {
//             return Start < other.End && other.Start < End;
//         }

//         public TimeSpan Duration => End - Start;
//     }
// }