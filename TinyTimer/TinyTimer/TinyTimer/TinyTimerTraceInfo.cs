using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNet.TinyTimer
{
    public class TinyTimerTraceInfo
    {
        public TinyTimerTraceInfo()
        {
            this.Id = Guid.NewGuid();
            this.StartedAt = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public TimeSpan? Elapsed { get; set; }

        public string Label { get; set; }

        public object[] Values { get; set; }

        public override string ToString()
        {
            return $"[Id: {this.Id}]: {this.Label} completed in {this.Elapsed} ({this.Elapsed?.TotalMilliseconds} ms). [{this.StartedAt} To {this.EndedAt}] Values: {ToJson(this.Values)}";
        }

        private static string ToJson(object[] values)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(values);
        }
    }
}
